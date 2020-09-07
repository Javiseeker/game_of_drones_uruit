import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-set-users',
  templateUrl: './set-users.component.html',
  styleUrls: ['./set-users.component.css']
})
export class SetUsersComponent implements OnInit {

  player1Username: string;
  player2Username: string;
  constructor(
    public dialogRef: MatDialogRef<SetUsersComponent>,
  ) { }

  ngOnInit(): void {
  }

  createUsersJson(form: NgForm): void {
    this.dialogRef.close(
      {player1: this.player1Username, player2: this.player2Username}
    );
  }

  validateParams(): boolean| null{
    if (
      this.player1Username === this.player2Username &&
      this.player1Username !== '' &&
      this.player1Username != null &&
      this.player2Username != null &&
      this.player2Username !== ''
      ) {
      return true;
    } else {
      return null;
    }
  }
}
