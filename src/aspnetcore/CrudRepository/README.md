<div dir='rtl'>

# Demo
[Demo](https://crudrepository.herokuapp.com/)

# CRUD Repository
مثال برای همه موارد زیر
- Asp.Net Core MVC
- EF Core
- InMemory DB
- Repository Pattern
- FluentValidation
- AutoMapper
- DI
- Logging


## Install
متناسب با دیتابیس یکی را نصب کنید
- dotnet add package Microsoft.EntityFrameworkCore.SqlServer
- dotnet add package Microsoft.EntityFrameworkCore.InMemory
- dotnet add package Microsoft.EntityFrameworkCore.SQLite


## نکات مهم
- در CustomerRepository.cs از DbContext استفاده شده است. در آینده به آسانی می توانید با نسخه Generic تعویض کنید.
- در Startup.cs از UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) استفاده شده است.
