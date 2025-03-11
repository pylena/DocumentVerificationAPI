import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiconnectionService } from '../services/apiconnection.service';

@Component({
  selector: 'app-document-upload',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './document-upload.component.html',
  styleUrl: './document-upload.component.css'
})
export class DocumentUploadComponent {
  uploadForm: FormGroup;
  uploadSuccess: boolean = false;

  constructor(private fb: FormBuilder, private apiconnectionService: ApiconnectionService) {
    this.uploadForm = this.fb.group({
      title: ['', Validators.required],
      file: [null, Validators.required]
    });
  }

  onSubmit(): void {
    if (this.uploadForm.valid) {
      const formData = new FormData();
      formData.append('title', this.uploadForm.get('title')?.value);
      formData.append('file', this.uploadForm.get('file')?.value);
      formData.append('userId', '1'); // only for testing, we can improve it leter on
  
      this.apiconnectionService.uploadDocument(formData).subscribe({
        next: (response: any) => {
          console.log('successfuly upload file:', response);
          this.uploadSuccess = true;
          this.uploadForm.reset();
        },
        error: (error: any) => {
          console.error('error uploading failed:', error);
          this.uploadSuccess = false;
        }
      });
    }
  }

  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.uploadForm.get('file')?.setValue(file);
    }
  }
}
