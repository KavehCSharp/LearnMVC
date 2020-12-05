<div dir='rtl'>

# Demo
[Demo](https://crudimagerepository.herokuapp.com)

(demo run with in-memory db, database will be remove after app going to sleep, i don't know about heroku limitation, please use small image under 200KB!)


# CRUD Repository
مثال برای همه موارد زیر
- Asp.Net Core 3.1 MVC
- Code First DB
- EF Core
- InMemory DB
- Repository Pattern
- FluentValidation
- AutoMapper
- DI in Asp.Net Core 3.1
- Upload to folder (db as base64)

## Install
متناسب با دیتابیس یکی را نصب کنید
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.InMemory (default)
dotnet add package Microsoft.EntityFrameworkCore.SQLite