Feature: MPPensionTests
	In order to use the MPPension site
	As a user
	I want to navigate between pages

@Chrome
Scenario: I can access the open pages on the MP Pension web site
	Given I open the MPPension web site
	Given I click the link with text selector 'Kontakt os'
	Then I am on the 'Kontakt os' page