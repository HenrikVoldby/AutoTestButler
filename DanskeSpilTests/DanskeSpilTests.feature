Feature: DanskeSpilTests
	In order to use the DanskeSpil site
	As a user
	I want to navigate between pages

@InternetExplorer @Quick
Scenario: I can complete the quick guldbarren game in InternetExplorer
	Given I open the DanskeSpil web site
	Given I click the link with xpath selector '//a[@href="/quick/?intcmp=top_menu_quick_brand"]'
	Given I click the link with xpath selector '//a[@href='/quick/spil/guldbarren?demo=1']'
	 When I wait until the game has loaded
	 Then The 'Guldbarren' game container is visible
	 Then The page contains image 'tool_Coin.png'
	Given I click the link with xpath selector '//div[@class='stepper']//a[@class="next"]'
	 Then The page contains image 'tool_key.png'
	Given I click the link with xpath selector '//div[@class='stepper']//a[@class="next"]'
	 Then The page contains image 'tool_pencil.png'
	 When I click the button with class selector 'try-btn'
	 Then The page contains substring 'Du spiller i øjeblikket et demo-spil'
	 When I click the button with xpath selector '//button[text() = 'Auto skrab']'
	Given I wait for the game to finish

@Chrome @Quick
Scenario: I can complete the quick smil i sigte game in Chrome
	Given I open the DanskeSpil web site
	Given I click the link with xpath selector '//a[@href="/quick/?intcmp=top_menu_quick_brand"]'
	Given I click the link with xpath selector '//a[@href='/quick/spil/smil-i-sigte?demo=1']'
	 When I wait until the game has loaded
	 Then The 'SmilISigte' game container is visible
	 When I click the button with class selector 'try-btn'
	 Then The page contains substring 'Du spiller i øjeblikket et demo-spil'
	 When I click the button with xpath selector '//button[text() = 'Auto skrab']'
	Given I wait for the game to finish

#@InternetExplorer
#Scenario: I can navigate to the Edit page in Internet explorer
#	Given I open the EnumSample web site
#	 When I click the 'EditRowLinkId1' link
#	 Then I am on the 'Create' page
#
#@InternetExplorer
#Scenario: I can navigate to the Delete page in Internet explorer
#	Given I open the EnumSample web site
#	 When I click the 'DeleteRowLinkId1' link
#	 Then I am on the 'Delete' page
#
#@InternetExplorer
#Scenario: I can navigate to the Details page in Internet explorer
#	Given I open the EnumSample web site
#	 When I click the 'DetailsRowLinkId1' link
#	 Then I am on the 'Details' page
#
#@Chrome
#Scenario: I can navigate to the create page in Chrome
#	Given I open the EnumSample web site
#	 When I click the 'CreateNewRowLinkId' link
#	 Then I am on the 'Create' page
#
#@Chrome
#Scenario: I can navigate to the edit page in Chrome
#	Given I open the EnumSample web site
#	 When I click the 'EditRowLinkId1' link
#	 Then I am on the 'Edit' page
#
#@Chrome
#Scenario: I can navigate to the delete page in Chrome
#	Given I open the EnumSample web site
#	 When I click the 'DeleteRowLinkId1' link
#	 Then I am on the 'Delete' page
#
#@Chrome
#Scenario: I can navigate to the details page in Chrome
#	Given I open the EnumSample web site
#	 When I click the 'DetailsRowLinkId1' link
#	 Then I am on the 'Details' page
