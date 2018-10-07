# Hair Salon MySql

#### 

#### Ahmed Khokar 

## User Stories

1. As a stylist I can add clients.
2. As a stylist I can remove clients.
3. As a stylist I can see all clients for other stylists and myself. 
4. As a stylist I can add/remove specialties.
5. As a stylist I can see all specialties for other stylists.

## Setup/Installation Requirements

* Clone this repository
* Navigate to the HairSolution.Solution/HairSolution directory
* Type dotnet restore
* Type dotnet build && dotnet run
* Open terminal a terminal application(I suggest mono)
```
$ CREATE DATABASE ahmed_khokar;
```
```
$ USE ahmed_khokar;
```
```
$ CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));
```
```
$ CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255));
```
```
$ CREATE TABLE specialties (id serial PRIMARY KEY, name VARCHAR(255));
```
```
$ CREATE TABLE stylists_clients (id serial PRIMARY KEY, stylist_id INT, client_id INT);
```
```
$ CREATE TABLE stylists_specialties (id serial PRIMARY KEY, stylist_id INT, specialty_id INT);
```
* In a web browser, navigate to 'http://localhost:5000/'

## Known Bugs

No known bugs.

## Support and contact details

email amkhokar@gmail.com for any questions

## Technologies Used

1. C#/.Net Core 1.1
2. VS Code
3. Mono
4. Git
5. Razor

### License

This software is licensed under the MIT license.

Copyright (c) 2018 **ahmed khokar**
