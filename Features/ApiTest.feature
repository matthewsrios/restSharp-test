Feature: ApiTest

    Scenario: Should return 200 ok
        Given I have access to api
        When I send a GET request to /posts
        Then I see status 200


    Scenario: Should return valid response
        Given I have access to api
        When I send a GET request to /posts
        Then I see valid post data