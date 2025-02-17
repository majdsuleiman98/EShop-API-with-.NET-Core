# E-Shop (.NET E-Commerce Project)

## Overview
E-Shop is a robust e-commerce application built using .NET with a clean architecture approach. The project incorporates modern design patterns and technologies to ensure maintainability, scalability, and high performance.

## Features
- **Clean Architecture** (Core Layer, Infrastructure Layer, API Layer)
- **Generic Repository Pattern** for data access abstraction
- **Unit of Work Pattern** to manage database transactions
- **Specification Pattern** for filtering products efficiently
- **Stripe API Integration** for secure online payments
- **Redis Caching** for optimizing product queries and reducing database traffic
- **Redis Basket Storage** to store user cart information efficiently
- **Role-Based Access Control** (Admin, User) for authorization management
- **Rate Limiting Middleware** to protect against excessive requests (5 requests per user every 10 seconds)

## Technologies Used
- **.NET 8**
- **Entity Framework Core**
- **SQL Server**
- **Redis**
- **Stripe API**
