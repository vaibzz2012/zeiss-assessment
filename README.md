# Product Management API

## Overview
This repository contains a Product Management API built with ASP.NET Core. The API provides CRUD operations for managing products, as well as functionality for stock updates and authentication.

---

## Endpoints
### Product Endpoints
1. **Add Product**  
   **POST** `/api/products`  
   Example Payload:
   ```json
   {
     "name": "Sample Product",
     "stockAvailable": 100,
     "price": 9.99
   }
   ```

2. **Get All Products**  
   **GET** `/api/products`

3. **Get Product by ID**  
   **GET** `/api/products/{id}`

4. **Update Product**  
   **PUT** `/api/products/{id}`  
   Example Payload:
   ```json
   {
     "name": "Updated Product",
     "stockAvailable": 50,
     "price": 19.99
   }
   ```

5. **Delete Product**  
   **DELETE** `/api/products/{id}`

6. **Update Stock**  
   **PATCH** `/api/products/{id}/stock`  
   Example Payload:
   ```json
   {
     "quantity": 10,
     "decrement": true
   }
   ```

---

## Technologies Used
- ASP.NET Core
- Entity Framework Core
- SQL Server


---