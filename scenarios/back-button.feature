Feature: Back button

Scenario: Authorised User Sees 'Back' feature On Every Page
          Given that I have successfully logged into the SRS
          When I access any page of the form after the first log in page
          Then I should see a clickable button at the top left of the page that states: 'Back'

Scenario: Authorised User Clicks 'Back' Button
          Given that I have successfully logged into the SRS
          And I access any page of the form after the first log in page
          When I click the button at the top left of the page that states: 'Back'
          Then I should be directed to the previous page which I was on before clicking 'Back'
