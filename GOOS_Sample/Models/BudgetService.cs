using GOOS_Sample.Models.DataModels;
using GOOS_Sample.Models.ViewModels;

namespace GOOS_Sample.Models
{
    public class BudgetService : IBudgetService
    {
        private IRepository<Budget> _budgetRepositoryStub;

        public BudgetService(IRepository<Budget> budgetRepositoryStub)
        {
            _budgetRepositoryStub = budgetRepositoryStub;
        }

        public void Create(BudgetAddViewModel model)
        {
            using (var dbcontext = new GOOSEntities())
            {
                var budget = new Budget() { Amount = model.Amount, YearMonth = model.Month };
                dbcontext.Budgets.Add(budget);

                dbcontext.SaveChanges();
            }
        }
    }
}