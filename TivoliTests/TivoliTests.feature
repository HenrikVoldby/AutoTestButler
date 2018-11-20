Feature: TivoliTests
	In order to use the Tivoli site
	As a user
	I want to navigate between pages

@Chrome
Scenario: I can access the contact us page in Chrome
	Given I open the Tivoli web site
	 Then The page contains header 'Glæd dig til'
	 When I click the link with text 'Kontakt os'
	 Then The page contains substring 'Åbningstider for Tivolis Callcenter i perioden'

@Chrome
Scenario: I can access the Tivoli Foodhall page under Spisesteder in Chrome
	Given I open the Tivoli web site
	 Then The page contains header 'Glæd dig til'
	 When I click the main menu item with text 'Mad og drikke'
	 When I click the submenu item with text 'Tivoli Food Hall'
	 Then The page contains header 'Tivoli Food Hall'

@InternetExplorer
Scenario: I can not enter an invalid email address when applying for news letter in IE
	Given I open the Tivoli web site
	 Then The page contains header 'Glæd dig til'
	Given I click the main menu item with text 'Mad og drikke'
	 When I enter 'not_a_valid_mailaddress' in the 'NewsletterFormEmailID' textbox
	 When I click the button with text 'Hold mig opdateret'
	 Then The 'NewsletterFormEmailID' textbox shows validation message 'Du skal angive en gyldig mailadresse'

@PhantomJS
Scenario: I can not enter an invalid email address when applying for news letter in headless browser
	Given I open the Tivoli web site
	 Then The page contains header 'Glæd dig til'
	Given I click the main menu item with text 'Praktisk information'
	Given I click the submenu item with text 'Priser'
	 Then The page contains header 'Priser'
