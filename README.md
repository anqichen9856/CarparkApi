# Carpark Availability Checking System - Backend

## Introduction

This is the backend API for a carpark availability checking system. It can be used together with a frontend (see instructions at https://github.com/anqichen9856/CarparkFrontend).

The API supports the following functionalities:
1. Registration
* Mandatory fields: First name, Last name, email, password
* Optional fields: Contact number

2. Login
* Login using email/password
* Respond with a JWT token upon successful login

3. View Member Details
* Protected API - requires JWT token
* Returns the member details

4. Get Carpark Availability
* Protected API - requires JWT token
* Returns data from this data.gov API: https://data.gov.sg/dataset/carpark-availability

This project is built using .Net Core with MySQL.

## Instructions for Setting Up

### Requirements

* Install MySQL Server at https://dev.mysql.com/downloads/mysql/
* Install Visual Studio at https://visualstudio.microsoft.com/downloads/

### Setting Up Steps

1. Download this project in Zip and decompress it
2. Start your local MySQL Server and initialize a database
3. Open the project in Visual Studio by double clicking `CarparkApi.sln` in the project's root folder
4. In Visual Studio, go to `appsettings.json`, follow the comments to edit the connection string with your local database server details  
5. Open the terminal in the project root directory, and type the following two commands:
    ```
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```
6. Run the project by clicking the `Run` button at the top left corner of Visual Studio
7. The API is now running. You can use it with a frontend by following instructions at https://github.com/anqichen9856/CarparkFrontend

If you have any suggestions or feedback, please feel free to contact me at anqichen@u.nus.edu.


