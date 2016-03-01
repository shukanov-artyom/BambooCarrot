using System;

namespace Carrot.BehaviorTests.Helpers
{
    public class ScenarioException : Exception
    {
        public ScenarioException(string message)
            : base (message)
        {
            
        }
    }
}
