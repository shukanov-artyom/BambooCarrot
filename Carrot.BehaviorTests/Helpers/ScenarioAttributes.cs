using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Carrot.BehaviorTests.Helpers
{
    public class ScenarioAttributes
    {
        private IDictionary<ScenarioAttribute, string> parameters = 
            new Dictionary<ScenarioAttribute, string>();

        public ScenarioAttributes(string[] tags)
        {
            string carrotMarker = tags.Single(t => t.Contains("CRT"));
            if (String.IsNullOrEmpty(carrotMarker))
            {
                throw new ScenarioException("Scenario must contain one scenario ID like CRT-123");
            }
            string splitn = carrotMarker.Split('-')[1];
            int scenarioId = Int32.Parse(splitn);
            this[ScenarioAttribute.CarrotScenarioId] = scenarioId.ToString();
        }

        public string this[ScenarioAttribute arg]
        {
            get
            {
                return parameters[arg];
            }
            private set
            {
                parameters[arg] = value;
            }
        }
    }
}
