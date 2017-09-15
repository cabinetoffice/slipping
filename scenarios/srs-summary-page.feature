Feature: Summary Page

Scenario: Authorised User Views Summary Page Before Submission
          Given that I have successfully logged into the SRS
          And I have completed all sections of the form correctly
          When I click continue at the end of the page entitled 'Are there any opposition MPs in attendance?'
          Then I should see a page entitled ‘Check your answers before submitting your slipping request’
          And I should see a summary of my completed slip in a table format
          And I should see the entered From and To locations for my absence request with a clickable 'Change' button next to it
          And I should see the entered Location of my absence request  with a clickable 'Change' button next to it
          And I should see the entered Travel Time to Westminster  with a clickable 'Change' button next to it
          And I should see the entered reason and details of my absence request  with a clickable 'Change' button next to it
          And I should see the entered response from the Opposition MP page  with a clickable 'Change' button next to it
          And at the end of the page I should see a clickable button that states: 'Submit Slipping Request'
      
      
Scenario: Authorised User Wants to Edit Information Before Submission 
          Given that I have successfully logged into the SRS
          And I have completed all sections of the form correctly
          And I am on the final page entitled ‘Check your answers before submitting your slipping request’
          And I see a summary of my completed slip in a table format with a clickable 'Change' button next to each section
          When I click a 'Change' button
          Then I should be taken to the corresponding page of the form
          And I should be able to edit/change and update the corresponding/relevant field in that page
          And I should be able to complete the form with the given parameters in each section
          And I should be able to return to this Summary Page before submission
          
           
Scenario: Authorised User Wants Submit Form Without Editing 
          Given that I have successfully logged into the SRS
          And I have completed all sections of the form correctly
          And I am on the final page entitled ‘Check your answers before submitting your slipping request’
          And I see a summary of my completed slip in a table format with a clickable 'Change' button next to each section
          When I do not clicked on the 'Change' button
          Then I should be able to click the button that states: 'Submit Slipping Request'
          And I should be taken to a page with a bold heading that states: 'Slipping request submitted'
          And below this I should see the date and time of the request which I have just submitted
          And below this I should see a clickable button that states: 'New Slipping Request'
