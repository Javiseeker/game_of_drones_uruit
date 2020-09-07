import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-round',
  templateUrl: './round.component.html',
  styleUrls: ['./round.component.css'],
})
export class RoundComponent implements OnInit {
  roundTurn = 'player1';
  selections = ['Rock', 'Paper', 'Scissors'];

  player1Selection: string;
  player2Selection: string;
  tie = false;
  @ViewChild('player1SelectionId') player1SelectionId: any;
  @ViewChild('player2SelectionId') player2SelectionId: any;

  constructor(
    public dialogRef: MatDialogRef<RoundComponent>,
    @Inject(MAT_DIALOG_DATA) public data
  ) {}

  ngOnInit(): void {}

  defineRound(): void {
    if (this.player2Selection == null){
      this.roundTurn = 'player2';
    }
    else {
      if (
         (this.player1Selection === 'Rock' && this.player2Selection === 'Rock')
      || (this.player1Selection === 'Paper' && this.player2Selection === 'Paper')
      || (this.player1Selection === 'Scissors' && this.player2Selection === 'Scissors')
      )
      {
        this.tie = true;
        this.player1Selection = null;
        this.player2Selection = null;
        this.roundTurn = 'player1';
      } else
      {
        if (
           (this.player1Selection === 'Rock' && this.player2Selection === 'Scissors')
        || (this.player1Selection === 'Paper' && this.player2Selection === 'Rock')
        || (this.player1Selection === 'Scissors' && this.player2Selection === 'Paper')
          )
          {
          this.dialogRef.close(
            {playerPositionId: 0, playerId: 'player1', username: this.data.player1}
          );
        }
        else if (
           (this.player2Selection === 'Rock' && this.player1Selection === 'Scissors')
        || (this.player2Selection === 'Paper' && this.player1Selection === 'Rock')
        || (this.player2Selection === 'Scissors' && this.player1Selection === 'Paper')
        ){
          this.dialogRef.close(
            {playerPositionId: 1, playerId: 'player2', username: this.data.player2}
          );
        };

      }
    }
  }
}
