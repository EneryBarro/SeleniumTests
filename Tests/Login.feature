Feature: User login
  As a user
  I want to log in to the system
  So that I can access my account

  Scenario: Login with empty credentials
    Given I navigate to the login page
    When I enter username "" and password ""
    Then I should see the error message "Username is required"

  Scenario: Login with only username
    Given I navigate to the login page
    When I enter username "standard_user" and password ""
    Then I should see the error message "Password is required"

  Scenario: Login with valid credentials
    Given I navigate to the login page
    When I enter username "standard_user" and password "secret_sauce"
    Then I should be redirected to the "Swag Labs" page



