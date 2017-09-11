Feature: Slipping request with location

Scenario: Authorised User Enters Location Correctly in Slipping Form
	Given I have successfully logged in to the SRS
	And I start a new Slip request
	And I have correctly entered a date and time for my absence
	And I click the ‘Continue’ icon
	Then I should see a page entitled ‘Where are you going?’
	And I should see a field with label ‘Location’
	And I should see a hint labelled ‘This can be a city, town or village’
	When I enter a location in text format inside the ‘Location’ field
	And this is within the character limit of 20 characters 
	Then I will be able to progress with completing the rest of the SRS form

Scenario: Authorised User Attempts to Enter Location Above Character Limit
	Given I have successfully logged in to the SRS
	And I start a new Slip request
	And I have correctly entered a date and time for my absence
	And I click the ‘Continue’ icon
	Then I should see a page entitled ‘Where are you going?’
	And I should see a field with label ‘Location’
	And I should see a hint labelled ‘This can be a city, town or village’
	When I attempt to enter a location in text format inside the ‘Location’ field
	And this is above the character limit of 20 characters 
	Then I will not be able to enter more text
	And I will  able to progress with completing the rest of the SRS form


Scenario: Authorised User Attempts to Enter Location In Numerical/Symbolic Format
	Given I have successfully logged in to the SRS
	And I start a new Slip request
	And I have correctly entered a date and time for my absence
	And I click the ‘Continue’ icon
	Then I should see a page entitled ‘Where are you going?’
	And I should see a field with label ‘Location’
	And I should see a hint labelled ‘This can be a city, town or village’
	When I attempt to enter a location in purely numeric format inside the ‘Location’ field
	And I attempt to enter a location in purely symbolic format inside the ‘Location’ field
	And this is either above or within the character limit of 20 characters 
	Then I will not be able to progress with completing the rest of the SRS form
