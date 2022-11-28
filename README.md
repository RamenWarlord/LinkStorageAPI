# LinkStorageAPI

## Setup
Dependencies and packages: 

![image](https://user-images.githubusercontent.com/42223424/204213405-ef4ced8e-f0f4-4bb2-bfef-4683c7305262.png)

Change connection string in appsettings.json to your own mysql database

![image](https://user-images.githubusercontent.com/42223424/204213605-c0f94e52-7894-44a2-81be-66b64130cdb8.png)

## Routes
## GET
### GET [/api/Users/{Optional Id}]
Returns all the users in the database. If an Id is provided, it will return just the specified user by Id.
### Example reponse body to calling ```GET https://localhost:7250/api/Users``` on my end:
```
{
	"statusCode": 200,
	"statusDescription": "Success: Users exist",
	"result": [
		{
			"userId": 1,
			"firstName": "Tim",
			"lastName": "Chang",
			"emailAddress": "tc123@gmail.com"
		},
		{
			"userId": 2,
			"firstName": "Jason",
			"lastName": "Lee",
			"emailAddress": "jl334@yahoo.com"
		},
		{
			"userId": 3,
			"firstName": "Tom",
			"lastName": "Ye",
			"emailAddress": "ty12123@gmail.com"
		}
	]
}
```
### Example reponse body to calling ```GET https://localhost:7250/api/Users/1``` on my end:
```
{
	"statusCode": 200,
	"statusDescription": "Success: User with id: 1 found",
	"result": {
		"userId": 1,
		"firstName": "Tim",
		"lastName": "Chang",
		"emailAddress": "tc123@gmail.com"
	}
}
```
### Example reponse body to calling ```GET https://localhost:7250/api/Users/5``` (User Id 5 does not exist in the database) on my end:
```
{
	"statusCode": 404,
	"statusDescription": "Error NotFound: User with id: 5 does not exist",
	"result": ""
}

```
### GET [/api/Lists/{Optional Id}]
```
{
	"statusCode": 200,
	"statusDescription": "Success: Lists found",
	"result": [
		{
			"listId": 1,
			"userId": 1,
			"listName": "General"
		},
		{
			"listId": 2,
			"userId": 1,
			"listName": "Learning"
		},
		{
			"listId": 3,
			"userId": 2,
			"listName": "Music"
		},
		{
			"listId": 4,
			"userId": 2,
			"listName": "Work"
		}
	]
}
```
