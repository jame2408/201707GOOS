Feature: BudgetController

@CleanBudgets
Scenario: Add a budget record
        When add a budget
        | Amount | Month   |
        | 2000   | 2017-02 |
        Then ViewBag should have a message for adding successfully
        And it should exist a budget record in budget table
        | Amount | YearMonth |
        | 2000   | 2017-02   |


@CleanBudgets
Scenario: Add a budget when there was existed a record of unique month
        Given go to adding budget page
        And Budget table existed budgets
        | Amount | YearMonth |
        | 999    | 2017-10   |
        When I add a buget 2000 for "2017-10"
        Then it should display "updated successfully"