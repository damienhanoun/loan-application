Feature: LoanOffersProposition
Propose loan offers to customers

    @mytag
    Scenario: Propose loan offers according to customer's wish
        Given a user that wants to borrow 1000 euros
        And the user set his email
        When the user requests loan offers
        Then the user should be proposed a loan offer of 1000 euros