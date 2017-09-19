Feature: Form Submission

Scenario: Nominated User Submits Form- Notify Email Sent To Applicant 
          Given that I have successfully logged into the SRS
          And I am a nominated user
          And I am on the final page entitled ‘Check your answers before submitting your slipping request’
          When click the button that states: 'Submit Slipping Request'
          Then I should receive an email notification stating that my request has been sent and is awaiting review
          And my MP should also receive this email notification stating that the slip request has been sent an is awaiting review
          
Scenario: MP Submits Form- Notify Email Sent To MP  
          Given that I have successfully logged into the SRS
          And I am an MP
          And I am on the final page entitled ‘Check your answers before submitting your slipping request’
          When click the button that states: 'Submit Slipping Request'
          Then I should receive an email notification stating that my request has been sent and is awaiting review
          
Scenario: Authorised User Submits Form- Notify Email Sent To Whips Team 
          Given that I have successfully logged into the SRS
          And I am either a nominated user or an MP
          And I am on the final page entitled ‘Check your answers before submitting your slipping request’
          When click the button that states: 'Submit Slipping Request'
          Then the Whips Offce receive an email notification stating that a new request has been sent and is awaiting review
       
       
Scenario: Authorised User Submits Form- Confirmation Page
          Given that I have successfully logged into the SRS
          And I am on the final page entitled ‘Check your answers before submitting your slipping request’
          When I click the button that states: 'Submit Slipping Request'
          Then I should be taken to a page with a bold heading that states: 'Slipping request submitted'
          And below this I should see the given date and time of the request which I have just submitted
          And I should see a clickable 'log out' button at the top of the page
          And below the page I should see a clickable button that states: 'New Slipping Request'
          And below this I should see a clickable button that states: 'Take me back to my slipping requests'
          And at the end of the page I should see instructions on how to Nominate a User that states: 'Want to nominate a user to fill in your slipping requests?
          We need their name and email address. You can send us these details by email or by telephone. 
          Email: pairsandwhipping@parliament.gov.uk
          Telephone: 0208 123 4567'
          
    
Scenario: Authorised User Submits Form- Clcks New Slipping request
          Given that I have succesfully submitted my Slipping Request
          And I am on a page with a bold heading that states: 'Slipping request submitted'
          And I see a clickable button that states: 'New Slipping Request'
          When I click the button that states: 'New Slipping Request'
          Then I should be directed to the first page of a new Slipping form with the title: 'When Will You Be Going?' 
          And I should be able to begin the Slipping process again 
          
          
Scenario: Authorised User Submits Form- Goes back to home page
          Given that I have succesfully submitted my Slipping Request
          And I am on a page with a bold heading that states: 'Slipping request submitted'
          And I see a clickable button that states: 'Take me back to my slipping requests'
          When I click the button that states: 'Take me back to my slipping requests'
          Then I should be directed to the home page of my SRS with the title of the Slip's Applicant, e.g: 'Sam Smith MP slipping requests'
          And I should be able to view my previously submitted slips 
          And I should now be able to see my most recently sent slip added to the lists of Slipping Requests
          And I should be able to begin the Slipping process again
          
Scenario: Authorised User Submits Form- Logs Out Of SRS
          Given that I have succesfully submitted my Slipping Request
          And I am on a page with a bold heading that states: 'Slipping request submitted'
          And I see a clickable button at the top of the page that states: 'Log Out'
          When I click the button that states: 'Log Out'
          Then I should be directed to the Gov.uk home page that gives information about Slipping
