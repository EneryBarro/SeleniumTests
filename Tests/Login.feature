Feature: User login
  As a user
  I want to log in to the system
  So that I can access my account

  Scenario Outline: Login attempt with different input clearing
    Given I navigate to the login page with "<browser>"
    When I enter username "<username>" and password "<password>"
    And I perform "<clear_action>" on input fields
    And I click the login button
    Then <result>

    Examples:
      | browser  | username      | password     | clear_action              | result                                                |
      | chrome   | standard_user | secret_sauce | clear both input fields   | I should see the error message "Username is required" |
      | chrome   | standard_user | secret_sauce | clear only password field | I should see the error message "Password is required" |
      | chrome   | standard_user | secret_sauce | do not clear fields       | I should be redirected to the "Swag Labs" page        |
      | edge     | standard_user | secret_sauce | clear both input fields   | I should see the error message "Username is required" |
      | edge     | standard_user | secret_sauce | clear only password field | I should see the error message "Password is required" |
      | edge     | standard_user | secret_sauce | do not clear fields       | I should be redirected to the "Swag Labs" page        |
      | firefox  | standard_user | secret_sauce | clear both input fields   | I should see the error message "Username is required" |
      | firefox  | standard_user | secret_sauce | clear only password field | I should see the error message "Password is required" |
      | firefox  | standard_user | secret_sauce | do not clear fields       | I should be redirected to the "Swag Labs" page        |


