using _01_Framework.Domain;
using ShopManagement.Application.Contract.Slide;

namespace ShopManagement.Domain.SlideAgg
{
    public interface ISlideRepository : IRepositoryBase<long, Slide>
    {
        EditSlide GetDetails(long id);
        List<SlideViewModel> GetList();
    }
}
