
Feature: Homepage View - PAWS2-51

Scenario: Page Title
          Given I have successfully logged into the SRS
          Then I should see a homepage with the heading ‘Slipping’ at the top of the page
          And I should see a text title/ heading in bold identifying which MP the SRS is representing e.g. ‘Sam Smith MP slipping requests'
          

Scenario: Listing slips when zero 
          Given I have successfully logged into the SRS
          And I have submitted no slips
          Then I should not see a an empty list of slips
          And I should not see a link to ‘View All’         
          
Scenario: Listing slips when between one and five
          Given I have successfully logged into the SRS
          And I have submitted 5 or fewer slip requests
          Then I should see a list of up to 5 slip requests for this user
          And each row should show the date of absence and start time of absence e.g. ‘10/01/2017 10:00am’
          And the slip request should be in order of XXX
          And I should not see a link to ‘View All’
          
Scenario: Listing slips when more than five
          Given I have successfully logged into the SRS
          And I have submitted more that five slip requests
          Then I should see a list of up to 5 slip requests for this user
          And the slip request should be in order of XXX
          And I should see a link to ‘View All’
          
          
Scenario: Viewing All Slips
          Given I have successfully logged into the SRS
          And I have submitted more that five slip requests
          And I see a list of up to 5 slip requests for this user
          And I see a clickable button to ‘View All’
          When I click this ‘View All’ button
          Then I should be directed to a new page
          And I should see a longer list with 6 or more submitted slip requests
          And I should see their decision status next to each request




