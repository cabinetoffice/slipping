Feature: Session Timeout

Scenario: Active session
	Given I visit the SRS
	And I click the login call to action
	And I have an active session e.g. interacted with the service within the last 60 minutes
	Then I will be given access to the SRS 

Scenario: Inactive session
	Given I visit the SRS
	And I click the login call to action
	And I have an active session e.g. interacted with the service longer than 60 minutes ago
	Then I will not be given access to the SRS 
