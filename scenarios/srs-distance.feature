Feature: Slipping request with distance

Scenario: User Enters Time Take to Travel Back To Westminster Correctly
	Given I have successfully logged in to the SRS
	And I start a new Slip request
	And I have correctly entered a date and time for my absence
	And I click the ‘Continue’ icon
	Then I should see a page entitled ‘Where are you going?’
	And I have correctly entered the location for my absence request
	And I should see a field labeled ‘How long will it take from your location to get to Westminster?’
	And I should see a hint labelled ‘Please provide an estimated time in hours only’
	When I enter a time in numeric format in whole hours inside the ‘Hours’ field
	And this is within the character limit of ???? characters 
	Then I will be able to progress with completing the rest of the SRS form

Scenario: User Attempts to Enter Time Take to Travel Back To Westminster Above Character Limit
	Given I have successfully logged in to the SRS
	And I start a new Slip request
	And I have correctly entered a date and time for my absence
	And I click the ‘Continue’ icon
	Then I should see a page entitled ‘Where are you going?’
	And I have correctly entered the location for my absence request
	And I should see a field labeled ‘How long will it take from your location to get to Westminster?’
	And I should see a hint labelled ‘Please provide an estimated time in hours only’
	When I attempt to enter a time in numeric format in whole hours inside the ‘Hours’ field
	And this attempt is above the character limit of ???? characters 
	Then I will not be able to enter more text
	And I will be able to progress with completing the rest of the SRS form

Scenario: User Attempts to Enter Time Take to Travel Back To Westminster Time- In Text and/or Symbol format
	Given I have successfully logged in to the SRS
	And I start a new Slip request
	And I have correctly entered a date and time for my absence
	And I click the ‘Continue’ icon
	Then I should see a page entitled ‘Where are you going?’
	And I have correctly entered the location for my absence request
	And I should see a field labeled ‘How long will it take from your location to get to Westminster?’
	And I should see a hint labelled ‘Please provide an estimated time in hours only’
	When I enter a time in text format inside the ‘Hours’ field e.g. 'eight hours'
	And this is within the character limit of ???? characters 
	Then I will not be able to progress with completing the rest of the SRS form

