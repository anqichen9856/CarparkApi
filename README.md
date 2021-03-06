# Carpark Availability Checking System - Backend

## Introduction

This is the backend API for a carpark availability checking system. It can be used together with a frontend (see instructions at https://github.com/anqichen9856/CarparkFrontend).

The API supports the following functionalities:
1. Registration
* Mandatory fields: First name, Last name, Email, Password
* Optional fields: Contact number

2. Login
* Login using email & password
* Respond with a JWT token upon successful login

3. View Member Details
* Protected API - requires JWT token
* Returns the member details

4. Get Carpark Availability
* Protected API - requires JWT token
* Returns data from this data.gov API: https://data.gov.sg/dataset/carpark-availability

This project is built using .Net Core 3.1 with MySQL.

## Instructions for Setting Up

### Requirements

* Install MySQL Server at https://dev.mysql.com/downloads/mysql/
* Install Visual Studio at https://visualstudio.microsoft.com/downloads/
* Install .NET Core 3.1 SDK at https://dotnet.microsoft.com/download

### Setting Up Steps

1. Download this project in Zip and decompress it
2. Start your local MySQL Server
3. Open the project in Visual Studio by double clicking `CarparkApi.sln` in the project root directory
4. In Visual Studio, go to `appsettings.json`, follow the comments to update the connection string with your local MySQL server password
5. Open the terminal in the project root directory, and type the following command:
    ```
    dotnet ef migrations add InitialCreate
    ```
6. Run the project by clicking the `Run` button at the top left corner of Visual Studio
7. The API is now running. You can use it with a frontend by following instructions at https://github.com/anqichen9856/CarparkFrontend

If you have any suggestions or feedback, please feel free to contact me at anqichen@u.nus.edu.


