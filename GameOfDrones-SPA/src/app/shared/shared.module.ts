import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TextboxValidatorDirective } from './directives/textbox-validator.directive';
import { DropdownValidatorDirective } from './directives/dropdown-validator.directive';

@NgModule({
  declarations: [
  TextboxValidatorDirective,
  DropdownValidatorDirective],
  imports: [
    CommonModule
  ],
  exports: [
    TextboxValidatorDirective,
    DropdownValidatorDirective,
  ],
  providers: [
  ]

})
export class SharedModule { }
