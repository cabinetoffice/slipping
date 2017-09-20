Scenario: Authorised user enters data in wrong format
          Given that i have successfully logged into the SRS
          And I am at any point on the form
          When I attempt to enter information that does not adhere to the given constraints of that specified field
          And I attempt to click 'Continue' 
          Then I will see an error message above this field in bold red that states e.g: 'Please enter your location in a valid format' 
          And I should automatically be taken to the top of the page
          And I should see a text box that states in black bold text: 'There was a problem'
          And below this I should see non-bold black text that states: 'Please ammend the following details:'
          And below this I should see the instruction to complete the missing section as a clickable link in red bold text e.g: 'Please enter location'
          And I should be able to click this link to return to the incomplete field on the page
          And I should be able to update this field with the correct information 
          And I should be able to progress with completing the form
          
                    
Scenario: Authorised User Does Not Select An Option On The Page 
          Given that i have successfully logged into the SRS
          And I have begun completing the form
          When I leave all fields unselected
          And I attempt to click 'Continue' without selecting an option
          Then I will see an error message above this blank field in bold red that states e.g: 'Select an Answer' 
          And I should automatically be taken to the top of the page
          And I should see a text box that states in black bold text: 'Please fix the following:'
          And below this I should see text in non-bold black that states: 'Please ammend the following details:'
          And below this I should see the instruction to complete the missing section as a clickable link in red bold text e.g: 'Please Select an Answer'
          And this link should direct me back to the incomplete field on the page
          And i should be able to update this field with the correct information 
          And i should be able to progress with completing the form
