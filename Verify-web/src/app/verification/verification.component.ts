import {DatePipe ,CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiconnectionService } from '../services/apiconnection.service';

@Component({
  selector: 'app-verification',
  standalone: true,
  imports: [ReactiveFormsModule,DatePipe ,CommonModule],
  templateUrl: './verification.component.html',
  styleUrl: './verification.component.css'
})
export class VerificationComponent {

  verificationForm: FormGroup;
  verificationResult: any = null;

  constructor(private fb: FormBuilder, private apiconnectionService: ApiconnectionService) {
    this.verificationForm = this.fb.group({
      verificationCode: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.verificationForm.valid) {
      const verificationData = {
        documentId: this.verificationForm.get('documentId')?.value,
        verifiedByUserId: this.verificationForm.get('verifiedByUserId')?.value,
        timestamp: new Date().toISOString(), 
        status: 'Verified' //todo add more status
      };
  
      this.apiconnectionService.verifyDocument(verificationData).subscribe({
        next: (response) => {
          console.log('Verification successful:', response);
          this.verificationResult = response;
        },
        error: (error) => {
          console.error('Verification failed:', error);
          this.verificationResult = null;
        }
      });
    }
}

}
