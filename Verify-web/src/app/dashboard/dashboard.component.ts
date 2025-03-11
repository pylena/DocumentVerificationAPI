import { Component } from '@angular/core';
import { ApiconnectionService } from '../services/apiconnection.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  documents: any[] = []; // List of documents

  constructor(private apiConnectionService: ApiconnectionService) {}

  ngOnInit(): void {
    this.apiConnectionService.getDocumentList().subscribe({
      next: (data) => {
        this.documents = data;
      },
      error: (error) => {
        console.error('Failed to load documents:', error);
      }
    });

}
}
