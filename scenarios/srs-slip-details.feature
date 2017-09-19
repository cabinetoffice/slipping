Feature: Slip Details

Scenario: Authorised User Opens Sent Slip to View Details - Slips with Approved & Refused Status
          Given I have successfully logged into the SRS
          And I see a list of up to 5 slip requests for this user
          When I click on the title of one of these slips e.g. ‘Slipping request 26/09/2017 - 10:00’
          Then I should see a new page with the name of the slip as the page heading e.g. ‘Slipping request 26/09/2017 - 10:00’
          And I should see a summary of the slip details below
          And I will not see an option to ‘Cancel my slipping request’
          And at the end of the slip review page i should see a clickable link to return to the summary page with the words ‘Take me back to my slipping requests’


Scenario: View Sent Slip Details Page- Date & Time
          Given I have successfully logged into the SRS
          When I click on the title of one of these sent slips e.g. ‘Slipping request 26/09/2017 - 10:00’
          Then I should see the requested From date and time of the slip e.g: ‘From: 26/09/2017 10:00’ 
          And below this i should see the requested To date and time e.g: ‘To: 26/09/2017 12:00’
          

Scenario: View Sent Slip Details Page- Location & Distance Traveled to Westminster
          Given I have successfully logged into the SRS
          When I click on the title of one of these sent slips e.g. ‘Slipping request 26/09/2017 - 10:00’
          Then I should see the requested Location e.g: ’Location: London’
          And below this I should see the requested time taken to ‘Travel to Westminster’ e.g: ‘Travel Time To Westminster:1 Hour’


Scenario: View Sent Slip Details Page- Reason 
          Given I have successfully logged into the SRS
          When I click on the title of one of these sent slips e.g. ‘Slipping request 26/09/2017 - 10:00’
          Then I should see the selected ‘Reason’ for the request e.g: ‘Reason: Personal’
          And below this I should see the entered details of the request e.g: ‘Details: Dentist appointment’
          

Scenario: View Sent Slip Details Page- Opposition MP & Status
          Given I have successfully logged into the SRS
          When I click on the title of one of these sent slips e.g. ‘Slipping request 26/09/2017 - 10:00’
          Then I should see the selection to ‘Any Opposition MPs in Attendance’ e.g: ‘Any opposition MPs in attendance? No’
          And below this, the current ‘Status’ of the Slip request e.g: ‘Status: Approved or ‘Status: Refused’ or ‘Status: Pending’


Scenario: View Pending Slip Details- Cancel Option
          Given that I have successfully logged into the SRS
          When I click on the title of a Slip with Pending status e.g. ‘Slipping request 26/09/2017 - 10:00’ ‘Status: Pending’
          Then I should see a question in bold asking: ‘Want to cancel your slipping request?’
          And below this I should see a clickable button with the words ‘Cancel my slipping request’
          And below this i should see a clickable link to return to the summary page with the words ‘Take me back to my slipping requests’
          
Scenario: View Unsubmitted Slip Details- Delete Option
          Given that I have successfully logged into the SRS
          When I click on the title of a Slip with Unsubmitted status e.g. ‘Slipping request 26/09/2017 - 10:00’ ‘Status: Unsubmitted’
          Then I should see a heading in bold stating: ‘Check your answers before submitting your slipping request’
          And below this I should see a summary of my Slip with a row of fields which I have completed and those yet to be populated
          And beside each field I should see a clickable button next to it stating: 'Change' 
          And at the end of the page I should see a clickable button that states: 'Submit Slipping Request' 
          And beside this I should see a clickable button that states: 'Delete my Slipping Request'
          And below this i should see a clickable link to return to the summary page with the words ‘Take me back to my slipping requests’

