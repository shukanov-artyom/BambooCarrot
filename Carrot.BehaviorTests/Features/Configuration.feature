Feature: Configuration

# todo : copy application binaries to a temp folder 
# copy all necessary data to a temp folder
# do operations against application binaries 
# delete application binaries from temp folder

#introduce universal identifier for a test to put test data and binaries into its own folder for later analysis
@CRT-1
Scenario: Pass configuration data to command line
    Given The following command line is passed to Carrot executable 'kk'
    Then Configuration file must contain the following keys and values
    | Key | Value |
    |     |       |
