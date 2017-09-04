Feature: Slipping request with time and date

Scenario: Valid date
	Given that I start a new slipping request
	Then I should see two date fields representing the start and end dates of the absence
	And I should be able to pick dates from today to five years in advance
	And the end date must be on or after the start date

Scenario: Valid time
	Given that I start a new slipping request
	Then I should see two time fields representing the start and end times of the absence
	And I should be able to pick times from now until the end of the day in 15 minute increments
	And the end time must be at least 15 minutes after the start time

Scenario: Invalid time
	Given that I start a new slipping request
	And I enter an invalid start or end time of the absence request
	Then I should not be able to continue to complete my slip request
	And I should see an error message

Scenario: Invalid date
	Given that I start a new slipping request
	And I enter an invalid date or end date of the absence request
	Then I should not be able to continue to complete my slip request
	And I should see an error message

Scenario: Successful date submission
	Given that I start a new slipping request
	And I enter a valid start and end time of the absence request
	And I enter a valid start and end date of the absence request
	Then I should be able to continue to complete my slip request
