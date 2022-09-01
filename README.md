## Rules

• Gambling where a random number between 0 - 9 should be generated.

• The player has a starting account of 10,000 points and can use any
Set partial amount on the randomly generated number.

• If he is correct, he wins 9 times his stake.
task

• Build REST APIs with .NET and C#

• The player sends his bet as a request to the program

o Example:

{
 "points": 100
 "bet": 3
}

• If the bet is successful, the player gets his account balance returned.

o Example:
{
 "account": 10900
 "status": "won"
 "points": "+900"
}

tips

• Error handling

• Identification of players


## Development Env: 
Visual Studio 2022
.NET Core 6

Database:
Sqlite

## Test Users
Username1: Player1, Password: Password1

Username2: Player2, Password: Password2


## Assumptions:

User:

Username is unique value

Two players exist in the system: Player1, Player2

User can stake only whole numbers

User can bet on numbers 0-9

Account:

Intial account balance for each user will be 10000 points


## Steps:

1: Launch the service from Visual Studio start up or from dotnet CLI

2: In the Swagger UI, make a POST request to Auth resource using the default user credentials

3: On successfull login, a token will be received in the response

4: Copy the generated token, click on Authorize button and paste the token along with 'Bearer' keyword

5: Make a POST request to Gamble resource providing Bet and Points

6: On winning, Player will receive 9 times the stake points. On losing, Player will loose the stake points.
