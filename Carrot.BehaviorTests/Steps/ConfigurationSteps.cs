using System;
using TechTalk.SpecFlow;

namespace Carrot.BehaviorTests.Steps
{
    [Binding]
    public class ConfigurationSteps
    {
        [Given(@"The following command line is passed to Carrot executable '(.*)'")]
        public void GivenTheFollowingCommandLineIsPassedToCarrotExecutable(string cmdline)
        {
            throw new NotImplementedException();
        }

        [Then(@"Configuration file must contain the following keys and values")]
        public void ThenConfigurationFileMustContainTheFollowingKeysAndValues(Table table)
        {
            throw new NotImplementedException();
        }
    }
}
