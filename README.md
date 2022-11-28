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
Returns all the lists in the database. If an Id is provided, it will return just the specified list by ListId(pk).
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
### GET [/api/Links/{Optional Id}]
Follows same concept as above two: Returns all the links in the database. If an Id is provided, it will return just the specified link by LinkId(pk).

## POST
### POST [/api/Users/]
Takes in firstname, lastname, emailaddress and posts a new entry to the Users table in the database. Id is auto_increment in the mysql so we don't need to pass it.
### Example request body to ```POST https://localhost:7250/api/Users/``` on my end:
```
{
		"firstName": "David",
		"lastName": "Ye",
		"emailAddress": "bingchilling14@gmail.com"
}
```
### Example response body to above mentioned request body:
```
{
	"statusCode": 201,
	"statusDescription": "Success: User was created.",
	"result": {
		"userId": 4,
		"firstName": "David",
		"lastName": "Ye",
		"emailAddress": "bingchilling14@gmail.com"
	}
}
```
### POST [/api/Lists/]
Takes in corresponding user id(fk) and the list name and posts new entry to Lists table in database. List id(pk) is auto increment so it is automatically done in mysql. We dont need to pass it.
### POST [/api/Links/]
Takes in corresponding list id(fk) and the link item and posts new entry to Links table in database. Link id(pk) is auto increment so it is automatically done in mysql. We dont need to pass it.

## DELETE
### DELETE [/api/Users/{Required Id}]
### Example reponse body to calling ```DELETE https://localhost:7250/api/Users/4``` (user exists) on my end:
```
{
	"statusCode": 200,
	"statusDescription": "Success: User was deleted.",
	"result": ""
}
```
### Example reponse body to calling ```DELETE https://localhost:7250/api/Users/6``` (user does not exist) on my end:
```
{
	"statusCode": 404,
	"statusDescription": "Error NotFound: Users does not exist",
	"result": null
}
```
### DELETE [/api/Lists/{Required Id}]
### DELETE [/api/Links/{Required Id}]
Same concept as seen with Users. Lists require ListId(pk). Links require LinkId(pk).
