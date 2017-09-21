Feature: Cookie Warning

Scenario: User Sees Cookie Warning on First Access to Landing Page
          Given I have entered the URL of the SRS
          When I first access the landing page with a heading in bold stating'
          Then I should see a Cookie warning appear at the top of the page stating: 'GOV.UK uses cookies to make the site simpler. Find out more about cookies'
          And there should be a clickable link to the Gov.uk Cookie information within the text stating:'Find out more about cookies'
          
Scenario: Cookie Warning Dissappears After Interaction with The Page
          Given I have entered the URL of the SRS
          And I see a Cookie Warning appear at the top of the page
          When I interact with the page by clicking another link 
          Then I should no longer see this cookie warning
          And I should no longer see this cookie warning on future visits to the SRS page 
          
Scenario: User Clicks on Cookie Warning
          Given I have entered the URL of the SRS
          And I see a Cookie Warning appear at the top of the page
          When I click the link that states: 'Find out more about cookies'
          Then I should be directed to this link: https://www.gov.uk/help/cookies
          And I should no longer see this cookie warning on future visits to the SRS page 
