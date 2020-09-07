# **Game Of Drones Installation**
## Back-end: .NET Core 3.1
I created the majority of the CRUD operations for the backend and a special POST endpoint for the upload of score of a non-existant user. 
I took a Database-first approach for the back-end Models. Take a look at the following diagram of my SQL Server-azure hosted database:

<img src="./ERD_game_of_drones.png">



## Front-end: Angular 10
I'm using the Angular Material UI component library. I'm doing all necessary validations in the Front-end. The logic required to decide what player wins resides in the Front-end.

## Installation Instructions
1. Install the .NET core 3.1.7 SDK from https://dotnet.microsoft.com/download/dotnet-core

2. Download Repository
`sudo git clone https://https://github.com/Javiseeker/game_of_drones_uruit`

3. Open a terminal. Move to the repository's path game_of_drones_uruit/GameOfDrones.API and run the following commands:
`dotnet restore`
`dotnet build`
`dotnet run`

4. Install LTS Nodejs version from https://nodejs.org/es/download/

5. Run the following command in a new terminal:
`npm install -g @angular/cli`

6. Open a new terminal. Move to the repository's path game_of_drones_uruit/GameOfDrones-SPA and run the following commands:
`npm install`
`ng build`
`ng serve --open`
