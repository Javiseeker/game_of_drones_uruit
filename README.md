# **Game Of Drones Installation**
## Back-end: .NET Core 3.1
I created the majority of the CRUD operations for the backend and a special POST endpoint for the upload of score of a non-existant user. 
I took a Database-first approach for the back-end Models. Take a look at the following diagram of my SQL Server-azure hosted database:

<img src="./ERD_game_of_drones.png">



## Front-end: Angular 10
I'm using the Angular Material UI component library. I'm doing all necessary validations in the Front-end. The logic required to decide what player wins resides in the Front-end.
