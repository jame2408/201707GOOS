using System;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using GOOS_Sample.Controllers;
using GOOS_Sample.Models;
using GOOS_Sample.Models.ViewModels;
using GOOS_SampleTests.DataModelsForIntegrationTest;
using GOOS_SampleTests.Steps.Comment;
using Microsoft.Practices.Unity;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace GOOS_SampleTests.Steps
{
    [Binding]
    public class BudgetControllerSteps
    {
        private BudgetController _budgetController;


        [BeforeScenario()]
        public void BeforeScenario()
        {
            //this._budgetController = new BudgetController(new BudgetService());
            this._budgetController = Hooks.UnityContainer.Resolve<BudgetController>();
        }

        [When(@"add a budget")]
        public void WhenAddABudget(Table table)
        {
            var model = table.CreateInstance<BudgetAddViewModel>();
            var result = this._budgetController.Add(model);

            ScenarioContext.Current.Set<ActionResult>(result);
        }
        
        [Then(@"ViewBag should have a message for adding successfully")]
        public void ThenViewBagShouldHaveAMessageForAddingSuccessfully()
        {

            var result = ScenarioContext.Current.Get<ActionResult>() as ViewResult;
            string message = result.ViewBag.Message;
            message.Should().Be(GetAddingSuccessfullyMessage());
        }

        private string GetAddingSuccessfullyMessage()
        {
            return "added successfully";
        }


        [Then(@"it should exist a budget record in budget table")]
        public void ThenItShouldExistABudgetRecordInBudgetTable(Table table)
        {
            using (var dbcontext = new GOOSEntitiesForTest())
            {
                var budget = dbcontext.Budgets
                    .FirstOrDefault();
                budget.Should().NotBeNull();
                table.CompareToInstance(budget);
            }
        }

        [Given(@"go to adding budget page")]
        public void GivenGoToAddingBudgetPage()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"Budget table existed budgets")]
        public void GivenBudgetTableExistedBudgets(Table table)
        {

            var budgets = table.CreateSet<Budget>();
            using (var dbcontext = new GOOSEntitiesForTest())
            {
                dbcontext.Budgets.AddRange(budgets);
                dbcontext.SaveChanges();
            }
        }

        [When(@"I add a buget (.*) for ""(.*)""")]
        public void WhenIAddABugetFor(int p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"it should display ""(.*)""")]
        public void ThenItShouldDisplay(string p0)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
