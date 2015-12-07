Feature: Get posts feature

Scenario: Get posts list from remote server
	Given There are posts in database
	| Id									| CreateDate			| Text	| Title	 |
	| AEBA973B-1210-49CE-B17D-000363361A81	| 2015-11-26 10:46:21	| Text1 | Title1 |
	| AEBA973B-1210-49CE-B17D-000363361A82	| 2015-11-26 10:46:22	| Text2 | Title2 |
	| AEBA973B-1210-49CE-B17D-000363361A83	| 2015-11-26 10:46:23	| Text3 | Title3 |
	And Server was started
	When I request all posts
	Then I get posts list
	| Id									| CreateDate			| Text	| Title	 |
	| AEBA973B-1210-49CE-B17D-000363361A81	| 2015-11-26 10:46:21	| Text1 | Title1 |
	| AEBA973B-1210-49CE-B17D-000363361A82	| 2015-11-26 10:46:22	| Text2 | Title2 |
	| AEBA973B-1210-49CE-B17D-000363361A83	| 2015-11-26 10:46:23	| Text3 | Title3 |