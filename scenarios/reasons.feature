Feature: User Enters Reason

Scenario: Authorised User Views Selectable Reasons in SRS
          Given I have successfully logged in to the SRS
          And I start a new Slip request
          And I have correctly entered a date and time for my absence
          And I have correctly entered the location and length of travel  for my absence 
          When I click the ‘Continue’ icon
          Then I should see a page entitled ‘What is the reason for your slip?’
          And I should see a list of 4 radio buttons below this
          And I should see a radio button with the label “Government Work”
          And I should see a radio button with the label “Constituency Engagement”
          And I should see a radio button with the label “Parliamentary Campaigning Activity”
          And I should see a radio button with the label “Personal/Other”
          
Scenario: Authorised User Selects Reasons in SRS
