import { Component, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

import { User } from '../chat/chat-service';

@Component({
    selector: 'registration-component',
    templateUrl: './registration.component.html',
    styleUrls: ['./style.css']
})

export class RegistrationComponent {

    @Input() userData: User;

    form: FormGroup;

    constructor(      
        private _fb: FormBuilder) {

        this.form = this._fb.group({
            email: ['', Validators.compose([Validators.required, Validators.maxLength(50)])],
            name: ['', Validators.compose([Validators.required, Validators.maxLength(100)])]
        });

    }

    prefillUserData() {

        this.userData.Email = this.form.get('email').value;
        this.userData.Name = this.form.get('name').value;
        
        localStorage.setItem('userData', JSON.stringify(this.userData));
    }

}