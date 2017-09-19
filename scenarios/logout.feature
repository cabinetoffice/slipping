Feature: Logout

Scenario: Authorised User Sees Logout Feature On Every Page
          Given that I have successfully logged into the SRS
          When I am completing any page of the form
          Then I should  see a clickable button at the top of the page that states: Slipping
          And Below this i should see a clickable button that states: 'Log Out'

Scenario: Authorised User Clicks 'Log Out' Button
          Given that I have successfully logged into the SRS
          And I am completing any page of the form
          When I click the button at the top of the page that states: 'Log Out'
          Then I should be directed to the Gov.uk home page that gives information about Slipping
          
          
Scenario: Authorised User Clicks 'Slipping' Button
          Given that I have successfully logged into the SRS
          And I am completing any page of the form
          When I click the button at the top of the page that states: 'Slipping'
          Then I should be directed to the home page of the service, 
          And this should show the MP summary page where I land after log in
          
