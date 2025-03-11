import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
/*
export interface Document {
  id: number;
  title: string;
  createdAt: string;
  userId: number;
  filePath: string;
}  */
@Injectable({
  providedIn: 'root'
})


export class ApiconnectionService {

  private baseUrl = 'https://localhost:44309/api/';

  constructor(private http: HttpClient) {}
   
  //Returns the list of documents
  getDocumentList():  Observable<any[]> {
    return this.http.get<any>(this.baseUrl + 'documents');
  }

  // Upload a document to the server
  uploadDocument(document: FormData): Observable<any> {
    return this.http.post(`${this.baseUrl}/Documents`, document);
  }

   // Verify a document
   verifyDocument(verificationData: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/verify`, verificationData);
  }


}
