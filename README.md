# Contact Manager Application

## Overview

The Contact Manager Application is a web-based system designed to manage contacts. It allows users to upload and manage contact details, including name, date of birth, phone number, marital status, and salary. The application supports basic CRUD operations such as creating, reading, updating, and deleting contact information. Additionally, it includes functionality to import contacts from CSV files.

## Features

- **CRUD Operations**: Create, Read, Update, and Delete contacts.
- **CSV File Upload**: Upload contacts from a CSV file to populate the contact list.
- **Inline Editing**: Edit contact details directly in the table.
- **Responsive Design**: A user-friendly, responsive interface for easy contact management.

## Technologies Used

- **Backend**: ASP.NET Core MVC
- **Frontend**: HTML, CSS, JavaScript (jQuery, DataTables)
- **Database**: SQL Server (or any other database based on your configuration)
- **CSV Parsing**: [CsvHelper](https://joshclose.github.io/CsvHelper/)

## Setup Instructions

### Prerequisites

- .NET 8.0
- SQL Server (or another database of your choice)
- Visual Studio or any compatible IDE for C# development

### Installation

Clone the repository:

   ```bash
   https://github.com/nikita1grynenko/contact-manager-application.git
   ```

### How to Use

- Navigate to the project directory
```bash
cd ContactManagerApplication
```
- Restore the NuGet packages:
```bash
dotnet restore
```
- Configure the connection string for your database in appsettings.json.

- Apply the database migrations (if using Entity Framework):
```bash
dotnet ef database update
```
- Run the application:
```bash
dotnet run
```
### Managing Contacts:

- CSV File Upload:
  - Click on the "Upload CSV" button to upload a CSV file containing contact data.
  - The application will parse the CSV file and add the contacts to the database.
  
- Add new contacts via the form in the dashboard.
  - Edit contact information directly in the contact list table.
  - Delete contacts by selecting the "Delete" button.
  - Sort using columns.

