import { Validator, AbstractControl, NG_VALIDATORS } from '@angular/forms';
import { Directive } from '@angular/core';


@Directive({

    selector: '[appDropdownValidator]',
    providers: [
        {
            provide: NG_VALIDATORS,
            useExisting: DropdownValidatorDirective,
            multi: true
        }
    ]
})
export class DropdownValidatorDirective  implements Validator {
    validate(control: AbstractControl): { [key: string]: any} | null {
        if (typeof control.value !== 'undefined' && control.value !== null && control.value !== ''){
            return null;
        }
        else{
            return { EmptyDropdown: true }
        }
     }
}