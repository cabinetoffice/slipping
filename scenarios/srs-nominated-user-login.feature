Feature: Nominated User Login

Scenario: Nominted User Log In Attempt - PASS - Correct Credentials 
	Given I try to log in to the SRS
	When I enter the correct username and password for my Microsoft Live account
	Then I will be given access to the SRS 

Scenario: Nominated User Log in Attempt - FAIL - Incorrect Credentials 
	Given I try to log in to the SRS
	When I enter the wrong username and password 
	Then I will not be granted access to the SRS 
	And I will see an error message 

Scenario: Nominated User Log in Attempt - FAIL- Incorrect Password
	Given I try to log in
	When I enter the wrong password
	And I enter the correct username
	Then I will see an error message that says my credentials are incorrect
	And I will not be granted access to the system

Scenario: Nominated User Log in Attempt - FAIL - Incorrect Username
	Given I try to log in to the SRS
	When I enter the wrong username
	Then I will see an error message that says my credentials are incorrect
	And I will not be granted access to the system
