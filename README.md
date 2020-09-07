# **Game Of Drones Installation**
Backend: .NET Core 3.1
Frontend: Angular 10

I took a Database-first approach for the back-end Models. Take a look at the following diagram of my SQL Server-azure hosted database:

<img src="./ERD_game_of_drones.png">


I created the majority of the CRUD operations for the backend and a special POST endpoint for the upload of score of a non-existant user. I'm doing all necessary validations in the front-end.
