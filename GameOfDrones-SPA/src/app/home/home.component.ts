import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { ApiService } from '../shared/services/api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})


export class HomeComponent implements OnInit {
  displayedColumns: string[] = ['uName', 'gsScore'];
  dataSource = new MatTableDataSource();
  // scoreboard: any;
  constructor(
    public dialog: MatDialog,
    private apiService: ApiService

  ) { }

  ngOnInit(): void {
    this.getScoreBoard();
  }

  getScoreBoard(): void{
    this.apiService.getGameStatistics().subscribe(
      response => {
        console.table(response)
        this.dataSource = new MatTableDataSource(response);
      },
      error => {
        console.log(error);
      }
    )
  }

}
