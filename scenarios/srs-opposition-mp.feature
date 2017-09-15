Scenario: Authorised User Sees Opposition MP Selection Page
          Given that I have successfully logged into the SRS
          And I have selected and completed the reason for the slip request
          When I click continue at the end of the page
          Then I should see a page entitled ‘Are there any opposition MPs in attendance?’
          And I should see 1 radio dial button with ‘Yes’ text next to it
          And I should see another radio dial button with ‘No’ text next to it
          And I should only be able to select one of these options
          And at the end of the page I should see a clickable ‘Continue’ button


Scenario: Select Yes to Add Opposition MP - Textbox Appears
          Given that I have successfully logged into the SRS
          And I am on the page entitled ‘Are there any opposition MPs in attendance?’
          When i click the radio dial that states ‘Yes’
          Then I should see the words “MP1’ in bold
          And I should see the instructions ‘Full Name’ appear below this
          And I should see a text box appear under this 
          And I should see a clickable button that states ‘Add Another MP’ below this


Scenario: Select Yes to Add Opposition MP- Textbox Populated
          Given that I have successfully logged into the SRS
          And I am on the page entitled ‘Are there any opposition MPs in attendance?’
          And I click the radio dial that states ‘Yes’
          And I see a text box appear under this 
          When I enter a full name in text format into this textbox
          Then I should see a clickable button that states ‘Add Another MP’ 
          And I should be able to click the ‘Continue’ button
          And I should be able to proceed with the rest of the form


Scenario: Add Opposition MP- Textbox Populated- ‘Remove This’ Option
          Given that I have successfully logged into the SRS
          And I am on the page entitled ‘Are there any opposition MPs in attendance?’
          And I enter a full name in text format into this textbox
          When I click ‘Add Another MP’ 
          Then I should see a clickable button that states: ‘Remove This’ beside every ‘MP’ text box
          And I should be able to click the ‘Continue’ button
          And I should be able to proceed with the rest of the form
          And I should see a clickable button that states ‘Remove This’ next to this


Sad Path: 
          Scenario: Select Yes to Add Opposition MP- Textbox Empty
          Given that I have successfully logged into the SRS
          And I am on the page entitled ‘Are there any opposition MPs in attendance?’
          And I click the radio dial that states ‘Yes’
          And I see a text box appear under this 
          When I do not enter any text into this textbox
          Then I should not see a clickable button that states ‘Add Another MP’ 
          And I should not see a clickable button that states ‘Remove This’
          And I should not be able to click the ‘Continue’ button
          And I should not be able to proceed with the rest of the form


Scenario: Select Opposition MP - ‘Add Another MP’ 
          Given that I have successfully logged into the SRS
          And I am on the page entitled ‘Are there any opposition MPs in attendance?’
          And I click the radio dial that states ‘Yes’
          And I have entered a full name in text format into this textbox
          When I click the ‘Add Another MP’ button
          Then i should see another text box appear with the words MP2
          And I should see subsequent text boxes appear with each click of the ‘Add Another MP’ button 
          And The ‘MP’ number should correspond with the number of boxes produced e.g “MP3, MP4, MP5 etc”


Scenario:  Add Another MP- Textbox Populated
          Given that I have successfully logged into the SRS
          And I am on the page entitled ‘Are there any opposition MPs in attendance?’
          And I click the radio dial that states ‘Yes’
          And I select ‘Add Another MP’
          When I enter a full name in text format into this textbox
          Then I should be able to click the ‘Continue’ button
          And I should be able to proceed with the rest of the form
          

Sad Path
Scenario: Add Another Opposition MP - Textbox Empty
          Given that I have successfully logged into the SRS
          And I am on the page entitled ‘Are there any opposition MPs in attendance?’
          And I click the radio dial that states ‘Yes’
          And I see a text box appear under this 
          When I do not enter any text into this textbox
          Then i will not be able to proceed with the rest of the form
          

Scenario: Select no to adding Opposition MP 
          Given that I have successfully logged into the SRS
          And I am on the page entitled ‘Are there any opposition MPs in attendance?’
          When i click the radio dial that states ‘No’
          Then I should see no changes to the page
          And I should be able to click the ‘Continue’ button
          And I should be able to proceed with the rest of the form
