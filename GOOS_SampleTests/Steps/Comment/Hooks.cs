using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation;
using GOOS_SampleTests.DataModelsForIntegrationTest;
using TechTalk.SpecFlow;

namespace GOOS_SampleTests.Steps.Comment
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeScenario]
        [Scope(Tag = "web")]
        public void SetBrowser()
        {
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
        }


        [BeforeScenario()]
        public void BeforeScenarioCleanTable()
        {
            CleanTableByTags();
        }

        [AfterFeature()]
        public static void AfterFeatureCleanTable()
        {
            CleanTableByTags();
        }

        private static void CleanTableByTags()
        {
            var tags = ScenarioContext.Current.ScenarioInfo.Tags
                .Where(x => x.StartsWith("Clean"))
                .Select(x => x.Replace("Clean", ""));

            if (!tags.Any())
            {
                return;
            }

            using (var dbcontext = new GOOSEntitiesForTest())
            {
                foreach (var tag in tags)
                {
                    dbcontext.Database.ExecuteSqlCommand($"TRUNCATE TABLE [{tag}]");
                }

                dbcontext.SaveChangesAsync();
            }
        }
    }
}
