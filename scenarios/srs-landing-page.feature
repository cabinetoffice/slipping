Feature: Landing Page View

Scenario: User sees Guidance for requirements to complete SRS 
          Given I have entered the URL of the SRS
          When I first access the landing page
          Then I should see a homepage with the heading ‘Slipping Application’ at the top of the page in Bold
          And below this I should see the text:
          'Welcome to the new Slipping Application. This allows users to submit slip requests, view the status of their previous slip requests and edit current slip requests.
          And Below this I should see instruction text in bold stating: 
          'Before you start, you will need: Date and time of your slipping request, Location, 
          How many hours away you will be from Westminister, Reason for your slipping request, 
          List of any opposition MP's also attending
          This service takes less than 2 minutes to complete'
          And below this I should see a log in button that directs me to the Windows Live Log In Page
         
          
Scenario: User sees contact details for Whips Office
          Given I have entered the URL of the SRS
          When I first access the landing page 
          Then I should see a bold heading stating: 'Get in Touch With Us'
          And below this I should see text stating:  
          'If you have any queries or concerns please get in touch with the Government Whips Admin Unit via email or telephone. 
          Want to nominate a user to fill in your slipping requests? We need their name and email address.
          You can send us these details by email or by telephone. 
          Email: GWAU@parliament.uk'
