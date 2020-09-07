import { Validator, AbstractControl, NG_VALIDATORS } from '@angular/forms';
import { Directive, Input } from '@angular/core';


@Directive({

    selector: '[appTextboxValidator]',
    providers: [
        {
            provide: NG_VALIDATORS,
            useExisting: TextboxValidatorDirective,
            multi: true
        }
    ]
})
export class TextboxValidatorDirective implements Validator {
    @Input() textboxList: any[];
    @Input() validationLevel: string;
    validate(control: AbstractControl): { [key: string]: any } | null {
        if (this.validationLevel === 'High') {
            if (typeof control.value !== 'undefined' && control.value !== null && control.value !== '') {
                if (/\S/.test(control.value)) {
                    if (this.textboxList.some(e => e.value.trim().toLowerCase() === control.value.trim().toLowerCase())) {
                        return { TextboxNotUnique: true }
                    }
                    else {
                        return null;
                    }
                }
                else {
                    return { EmptyTextbox2: true }
                }
            }
            else {
                return { EmptyTextbox: true }
            }
        }
        else if (this.validationLevel === 'Medium') {
            if (typeof control.value !== 'undefined' && control.value !== null && control.value !== '') {
                if (/\S/.test(control.value)) {
                    return null;
                }
                else {
                    return { EmptyTextbox2: true }
                }
            }
            else {
                return { EmptyTextbox: true }
            }
        } else if(this.validationLevel === 'Low') {
            if (/\S/.test(control.value)) {
                return null;
            }
            else {
                return { EmptyTextbox2: true }
            }
        }
    }
}