using System;
using TechTalk.SpecFlow;

namespace Carrot.BehaviorTests.Helpers
{
    [Binding]
    public class ScenarioBorderActions
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            ScenarioAttributes attr = new ScenarioAttributes(
                ScenarioContext.Current.ScenarioInfo.Tags);
            string scenarioId = attr[ScenarioAttribute.CarrotScenarioId];

        }

        [AfterScenario]
        public void AfterScenario()
        {

        }
    }
}
