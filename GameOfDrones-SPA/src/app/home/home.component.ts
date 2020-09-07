import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { ApiService } from '../shared/services/api.service';
import { NotificationsComponent } from '../notifications/notifications.component';
import { SetUsersComponent } from '../set-users/set-users.component';
import { RoundComponent } from '../round/round.component';
import { MatSnackBar} from '@angular/material/snack-bar';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})


export class HomeComponent implements OnInit {
  displayedColumns: string[] = ['uName', 'gsScore'];
  displayedColumnsCurrentScore: string[] = ['round', 'winner'];
  dataSource = new MatTableDataSource();
  dataSourceCurrentScore = new MatTableDataSource();
  errorMessage: string;
  SNACKBAR_DURATION = 5;
  currentScoreData = [
    {
      round: 1,
      winner: ''
    },
    {
      round: 2,
      winner: ''
    },
    {
      round: 3,
      winner: ''
    },
    {
      round: 4,
      winner: ''
    },
    {
      round: 5,
      winner: ''
    }
  ];
  isLoading: boolean = false;
  gameStarted: boolean = false;
  playersInformation: any =
  [
    {
      player1:
      {
        username: '',
        wins: 0
      }
    },
    {
      player2:
      {
        username: '',
        wins: 0
      }
    }
  ];
  roundCounter: number;
  constructor(
    public setUsersDialog: MatDialog,
    public roundDialog: MatDialog,
    private apiService: ApiService,
    private snackBar: MatSnackBar,
  ) { }

  ngOnInit(): void {
    this.getScoreBoard();
    this.dataSourceCurrentScore = new MatTableDataSource (this.currentScoreData);
  }

  getScoreBoard(): void{
    this.isLoading = true;
    this.apiService.getGameStatistics().subscribe(
      response => {
        this.dataSource = new MatTableDataSource(response);
        this.isLoading = false;
      },
      error => {
        // console.log(error);
      }
    );
  }

  startGame(): void{
    this.gameStarted = true;
    const dialogRef = this.setUsersDialog.open(SetUsersComponent, {
      width: '350px',
      height: '350px',
      hasBackdrop: false,
    });
    dialogRef.afterClosed().subscribe(
      dialogResult => {
        if(dialogResult != null){
          this.setGameVariables(dialogResult);
        } else{
          this.gameStarted = false;
        }
      }
    );
  }

  setGameVariables(usernamesJson: any): void{
    this.playersInformation[0].player1.username = usernamesJson.player1;
    this.playersInformation[0].player1.wins = 0;
    this.playersInformation[1].player2.username = usernamesJson.player2;
    this.playersInformation[1].player2.wins = 0;
    this.roundCounter = 0;
    this.checkGameStatus();
  }
  checkGameStatus(): void {
    if (this.playersInformation[0].player1.wins !== 3 && this.playersInformation[1].player2.wins !== 3){
      this.roundCounter += 1;
      this.playRound();
    }
    else{
      if(this.playersInformation[0].player1.wins === 3){
        this.showNotification(this.playersInformation[0].player1.username);
        this.apiService.postGameStatistics(this.playersInformation[0].player1.username, 100).subscribe(
          response => {
            this.getScoreBoard();
          },
          error => {
            // console.log(error);
          }
        );;
        this.gameStarted = false;
        this.currentScoreData.forEach(
          item => {
            item.winner = '';
          }
        );
      }
      if(this.playersInformation[1].player2.wins === 3){
        this.showNotification(this.playersInformation[1].player2.username);
        this.apiService.postGameStatistics(this.playersInformation[1].player2.username, 100).subscribe(
          response => {
            this.getScoreBoard();
          },
          error => {
            // console.log(error);
          }
        );
        this.gameStarted = false;
        this.currentScoreData.forEach(
          item => {
            item.winner = '';
          }
        );
      }
    }
  }

  playRound(): void{
    const dialogRef2 = this.roundDialog.open(RoundComponent, {
      width: '350px',
      height: '350px',
      hasBackdrop: false,
      disableClose: true,
      data:
      {
        player1: this.playersInformation[0].player1.username,
        player2: this.playersInformation[1].player2.username,
        round: this.roundCounter
      }
    });
    dialogRef2.afterClosed().subscribe(
      dialogResult => {
        // {playerPositionId: 0 o 1, playerId: 'player1' o 'player2', username: username que gano}
        // devolver la ronda que se le dio, y el usuario que gano en un json.
        this.playersInformation[dialogResult.playerPositionId][dialogResult.playerId].wins += 1;
        // asignar el winner dependiendo de la ronda y  el ganador.
        this.currentScoreData[this.roundCounter - 1].winner = dialogResult.username;
        this.checkGameStatus(); /// se vuelve a llamar la funcion que esta pendiente del status del juego
      }
    );

  }

  showNotification(username: string): void {
    this.snackBar.openFromComponent(NotificationsComponent, {
      duration: this.SNACKBAR_DURATION * 1000,
      data: username
    });
  }
}
