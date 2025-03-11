#  Document Verification System

A full-stack document verification system where users can **upload official documents** and **verify them using a unique digital code**.

## Built With
- **Backend**: ASP.NET Core (.NET 8) with **Entity Framework Core & Dapper**
- **Frontend**: Angular 
- **Database**: SQL Server

---

## Features
**Upload Documents** – Users can upload official documents.  
**Unique Verification Code** – Each document gets a unique digital code
**Verify Documents** – Users can verify documents using the unique code.  

## Entity Framework vs Dapper for fetching Data

* EF Core may not be as fast as Dapper for complex queries, especially when dealing with large datasets. This is due to the overhead of tracking entities, generating SQL dynamically, and handling lazy loading.
* Dapper is generally faster than EF Core for read-heavy operations


### API Documentation
* Endpoint: POST /api/documents
* Uploads a document and generates a unique verification code.

* Endpoint: GET /api/documents/{id}
* Description: Retrieves details of a specific document.

* Endpoint: POST /api/verify
* Description: Verifies a document using a unique code.
* Client Side Features
* Dashboard (/dashboard)
Displays a list of uploaded documents.
Shows the status and verification code.
  * Document Upload (/upload)
Allows users to upload documents.
Validates form inputs.
* Verification Page (/verify)
Users can enter a verification code to check document status.
