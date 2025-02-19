using System.Globalization;
using _01_Framework.Application;
using _01_Framework.Application.ZarinPal;
using _02_Query.Contracts;
using _02_Query.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contract.Order;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        private readonly ICartCalculatorService _cartCalculatorService;
        public const string CookieName = "cart-items";
        public Cart Cart;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IOrderApplication _orderApplication;
        private readonly IAuthHelper _authHelper;
        private readonly IZarinPalFactory _zarinPalFactory;


        public CheckoutModel(ICartCalculatorService cartCalculatorService, ICartService cartService,
            IProductQuery productQuery, IOrderApplication orderApplication, IAuthHelper authHelper,
            IZarinPalFactory zarinPalFactory)
        {
            _cartCalculatorService = cartCalculatorService;
            _cartService = cartService;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _authHelper = authHelper;
            _zarinPalFactory = zarinPalFactory;
            Cart = new Cart();
        }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var cartItem in cartItems)
                cartItem.CalculateTotalItemPrice();

            Cart = _cartCalculatorService.ComputeCart(cartItems);
            _cartService.Set(Cart);
        }

        public IActionResult OnPostPay(int paymentMethod)
        {
            var cart = _cartService.Get();
            cart.SetPaymentMethod(paymentMethod);

            var result = _productQuery.CheckInventoryStatus(cart.Items);
            if (result.Any(x => !x.IsInStock))
                return RedirectToPage("/Cart");

            var orderId = _orderApplication.PlaceOrder(cart);
            if (paymentMethod == PaymentMethods.Zarinpal)
            {
                var paymentResponse = _zarinPalFactory.CreatePaymentRequest(
                    cart.PayAmount.ToString(CultureInfo.InvariantCulture), "", "",
                    "خرید از درگاه لوازم خانگی و دکوری", orderId);

                return Redirect(
                    $"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
            }

            var paymentResult = new PaymentResult();
            return RedirectToPage("/PaymentResult",
                paymentResult.Succeeded(ApplicationMessages.PaymentByCash, "0"));
        }

        public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status,
            [FromQuery] long oId)
        {
            var orderAmount = _orderApplication.GetAmountBy(oId);
            var verificationResponse =
                _zarinPalFactory.CreateVerificationRequest(authority,
                    orderAmount.ToString(CultureInfo.InvariantCulture));
            var result = new PaymentResult();
            if (status == "OK" && verificationResponse.Status >= 100)
            {
                var issueTrackingNo = _orderApplication.PaymentSucceeded(oId, verificationResponse.RefID);
                Response.Cookies.Delete(CookieName);
                result = result.Succeeded(ApplicationMessages.PaymentSuccessful, issueTrackingNo);
                return RedirectToPage("/PaymentResult", result);
            }

            result = result.Failed(ApplicationMessages.PaymentFailed);
            return RedirectToPage("/PaymentResult", result);

        }
    }
}
