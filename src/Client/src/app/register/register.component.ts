import { Component, EventEmitter, Input, OnInit, Output, inject, input, output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { NgIf } from '@angular/common';
// import { TextInputComponent } from "../_forms/text-input/text-input.component";
// import { DatePickerComponent } from '../_forms/date-picker/date-picker.component';
import { Router } from '@angular/router';

@Component({
    selector: 'app-register',
    standalone: true,
    templateUrl: './register.component.html',
    styleUrl: './register.component.css',
    imports: [FormsModule]
})
export class RegisterComponent implements OnInit {
  private accountService = inject(AccountService);
  // private fb = inject(FormBuilder);
  private router = inject(Router);
  
  registerForm: FormGroup = new FormGroup({});
  maxDate = new Date();
  validationErrors: string[] | undefined;

  // @Input() usersFromHomeComponent : any;   => This is used before angular 17

 // This is after angular 17 a new way for writing Input properties . An extension to Angular signal and does have compiler support.
    //  usersFromHomeComponent  = input.required<any>();

// @Output() cancelRegister = new EventEmitter<any>();

  cancelRegister = output<boolean>();  // New way for output properrty

  model: any = {};

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18)
  }

  initializeForm() {
  //   this.registerForm = this.fb.group({
  //     gender: ['male'],
  //     username: ['', Validators.required],
  //     knownAs: ['', Validators.required],
  //     dateOfBirth: ['', Validators.required],
  //     city: ['', Validators.required],
  //     country: ['', Validators.required],
  //     password: ['', [Validators.required, Validators.minLength(4), 
  //         Validators.maxLength(8)]],
  //     confirmPassword: ['', [Validators.required, this.matchValues('password')]],
  //   });
  //   this.registerForm.controls['password'].valueChanges.subscribe({
  //     next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
  //   })
  // }

  // matchValues(matchTo: string): ValidatorFn {
  //   return (control: AbstractControl) => {
  //     return control.value === control.parent?.get(matchTo)?.value ? null : {isMatching: true}
  //   }
  }

  register() {
    // const dob = this.getDateOnly(this.registerForm.get('dateOfBirth')?.value);
    // this.registerForm.patchValue({dateOfBirth: dob});
    this.accountService.register(this.model).subscribe({
      next: (response) => 
        {
          console.log(response);
          this.cancel();
          // this.router.navigateByUrl('/members');
        },
      error: error => this.validationErrors = error
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

  private getDateOnly(dob: string | undefined) {
    if (!dob) return;
    return new Date(dob).toISOString().slice(0, 10);
  } 
}