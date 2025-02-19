using _01_Framework.Domain;
using DiscountManagement.Application.Contract.ColleagueDiscount;

namespace DiscountManagement.Domain.ColleagueDiscountAgg
{
    public interface IColleagueDiscountRepository : IRepositoryBase<long,ColleagueDiscount>
    {

        EditColleagueDiscount GetDetails(long id);
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    }
}
