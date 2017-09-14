Feature: User Enters Reason

Scenario: Authorised User Views Selectable Reasons in SRS
          Given I have successfully logged in to the SRS
          And I start a new Slip request
          And I have correctly entered a date and time for my absence
          And I have correctly entered the location and length of travel  for my absence 
          When I click the ‘Continue’ icon
          Then I should see a page entitled ‘What is the reason for your slip?’
          And I should see a list of 4 radio buttons below this
          And I should see a radio button with the label “Government Work (Secretaries of State / Ministers of State only)”
          And I should see a radio button with the label “Constituency Engagement”
          And I should see a radio button with the label “Parliamentary Campaigning Activity”
          And I should see a radio button with the label “Personal/Other”
          And at the end of the page I should see a clickable button that says 'Continue'
          
Scenario: Authorised User Selects Reason Radio Button in Slipping Form- 'Government Work'
          Given have successfully logged into the SRS
          And I see a list of 4 radio buttons on the page entitled: ‘What is the reason for your slip’ 
          When I select the radio button that states: “Government Work (Secretaries of State / Ministers of State only)” 
          Then I should see a text field appear below this with a word limit of 200 words
          And I should see a label that states “Please provide a description for your reason”
          And I should see a hint appear below this that states “for example: Select Committee Trip, Delegation on behalf of a group, an All Party Parliamentary related trip”
          And I should see an instruction below this that states “What will be the repercussions of the slip being revoked at last minute? 
          And I should see an instruction below this stating "Please state if the trip has been approved by Number 10”
          
Scenario: Authorised User Enters Reason in Text Field- 'Government Work'
	Given I have successfully logged in to the SRS
       	And I see a list of 4 radio buttons on the page entitled: ‘What is the reason for your slip’ 
       	And I select the radio button that states: “Government Work (Secretaries of State / Ministers of State only)” 
      	And I see a text field with a word limit of 200 words
     	When I enter a reason in text format inside the given text field
	And this is within the word limit of 200 words 
	Then I will be able to click the 'Continue' button
       	And I will be able to progress with completing the rest of the SRS form
          

