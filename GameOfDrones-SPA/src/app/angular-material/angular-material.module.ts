import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatCardModule} from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatButtonModule} from '@angular/material/button';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
   imports: [
      MatButtonModule,
      MatGridListModule,
      MatFormFieldModule,
      MatInputModule,
      MatTableModule,
      MatCardModule,
      MatDialogModule,
      MatProgressSpinnerModule,
      CommonModule,
      MatSnackBarModule,
      MatSelectModule
   ],
   exports: [
    MatButtonModule,
    MatGridListModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatCardModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    CommonModule,
    MatSnackBarModule,
    MatSelectModule
   ],
   providers: []
})

export class AngularMaterialModule {}
