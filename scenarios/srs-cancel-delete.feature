Feature: Form Cancellation and Deletion

Scenario: Authorised User Cancels 'Pending' Slip
          Given that I have successfully logged into the SRS
          And I click on the title of a Slip with Pending status e.g. ‘Slipping request 26/09/2017 - 10:00’ ‘Status: Pending’
          When I click on the button with the words ‘Cancel my slipping request’
          Then I should be taken to a page with a bold heading that states: 'Slipping request cancelled'
          And below this I should see the given date and time of the request which I have just cancelled
          And I should see a clickable 'log out' button at the top of the page
          And below the page I should see a clickable button that states: 'New Slipping Request'
          And below this I should see a clickable button that states: 'Take me back to my slipping requests'
          And at the end of the page I should see instructions on how to Nominate a User that states: 'Want to nominate a user to fill in your slipping requests?
          We need their name and email address. You can send us these details by email or by telephone. 
          Email: pairsandwhipping@parliament.gov.uk
          Telephone: 0208 123 4567'
          
          
Scenario: MP Cancels 'Pending' Slips- Notify Email Sent To MP  
          Given that I have successfully logged into the SRS
          And I click on the title of a Slip with Pending status e.g. ‘Slipping request 26/09/2017 - 10:00’ ‘Status: Pending’
          When I click on the button with the words ‘Cancel my slipping request’
          Then I should receive an email notification stating that my request has been cancelled
          

Scenario: Nominated User Cancels 'Pending' Slip- Notify Email Sent To Applicant and MP
          Given that I have successfully logged into the SRS
          And I am a nominated user
          And I click on the title of a Slip with Pending status e.g. ‘Slipping request 26/09/2017 - 10:00’ ‘Status: Pending’
          When I click on the button with the words ‘Cancel my slipping request’
          Then I should receive an email notification stating that my request has been cancelled
          And my MP should also receive this email notification stating that the slip request has been cancelled
          

Scenario: Authorised User Cancels 'Approved' Slip
          Given that I have successfully logged into the SRS
          And I click on the title of a Slip with Approved status e.g. ‘Slipping request 26/09/2017 - 10:00’ ‘Status: Approved’
          When I click on the button with the words ‘Cancel my slipping request’
          Then I should be taken to a page with a bold heading that states: 'Slipping request cancelled'
          And below this I should see the given date and time of the request which I have just cancelled
          And I should see a clickable 'log out' button at the top of the page
          And below the page I should see a clickable button that states: 'New Slipping Request'
          And below this I should see a clickable button that states: 'Take me back to my slipping requests'
          And at the end of the page I should see instructions on how to Nominate a User that states: 'Want to nominate a user to fill in your slipping requests?
          We need their name and email address. You can send us these details by email or by telephone. 
          Email: pairsandwhipping@parliament.gov.uk
          Telephone: 0208 123 4567'
          
          
Scenario: MP Cancels 'Approved' Slips- Notify Email Sent To MP  
          Given that I have successfully logged into the SRS
          And I click on the title of a Slip with Approved status e.g. ‘Slipping request 26/09/2017 - 10:00’ ‘Status: Approved’
          When I click on the button with the words ‘Cancel my slipping request’
          Then I should receive an email notification stating that my request has been cancelled
          

Scenario: Nominated User Cancels 'Submitted' Slip- Notify Email Sent To Applicant and MP
          Given that I have successfully logged into the SRS
          And I am a nominated user
          And I click on the title of a Slip with Approved status e.g. ‘Slipping request 26/09/2017 - 10:00’ ‘Status: Approved’
          When I click on the button with the words ‘Cancel my slipping request’
          Then I should receive an email notification stating that my request has been cancelled
          And my MP should also receive this email notification stating that the slip request has been cancelled
          
          

Scenario: Authorised User Deletes Unsubmitted Slip
          Given that I have successfully logged into the SRS
          And I click on the title of a Slip with Unsubmitted status e.g. ‘Slipping request 26/09/2017 - 10:00’ ‘Status: Unsubmitted’
          When I click on the button with the words ‘Delete my slipping request’
          Then I should be taken to a page with a bold heading that states: 'Slipping request Deleted'
          And below this I should see the given date and time of the request which I have just cancelled
          And I should see a clickable 'log out' button at the top of the page
          And below the page I should see a clickable button that states: 'New Slipping Request'
          And below this I should see a clickable button that states: 'Take me back to my slipping requests'
          And at the end of the page I should see instructions on how to Nominate a User that states: 'Want to nominate a user to fill in your slipping requests?
          We need their name and email address. You can send us these details by email or by telephone. 
          Email: pairsandwhipping@parliament.gov.uk
          Telephone: 0208 123 4567'
          

          
Scenario: MP Deletes Unsubmitted Slip- Notify Email Sent To MP  
          Given that I have successfully logged into the SRS
          And I click on the title of a Slip with Pending status e.g. ‘Slipping request 26/09/2017 - 10:00’ ‘Status: Unsubmitted’
          When I click on the button with the words ‘Delete my slipping request’
          Then I should receive an email notification stating that my request has been Deleted
          

Scenario: Nominated User Deletes Unsubmitted Slip- Notify Email Sent To Applicant and MP
          Given that I have successfully logged into the SRS
          And I am a nominated user
          And I click on the title of a Slip with Unsubmitted status e.g. ‘Slipping request 26/09/2017 - 10:00’ ‘Status: Unsubmitted’
          When I click on the button with the words ‘Delete my slipping request’
          Then I should receive an email notification stating that my request has been Deleted
          And my MP should also receive this email notification stating that the slip request has been Deleted
          

