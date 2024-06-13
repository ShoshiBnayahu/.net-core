# .Net-Core-Todo

## Description

This project is an ASP.NET MVC Web API built using .NET Core. It provides endpoints for managing to-do items and users with authorization and logging functionalities.


## Endpoints

### /api/todo

- **GET** `/api/todo`
  - **Authorize**: User/Admin
  - **Description**: Get all user's to-do items
  - **Request body**: None
  - **Response body**: List of to-do items

- **GET** `/api/todo/{id}`
  - **Authorize**: User/Admin
  - **Description**: Get a user's to-do item by ID
  - **Request body**: None
  - **Response body**: To-do item

- **POST** `/api/todo`
  - **Authorize**: User/Admin
  - **Description**: Add a new to-do item to user
  - **Request body**: To-do item
  - **Response body**: To-do item location

- **PUT** `/api/todo/{id}`
  - **Authorize**: User/Admin
  - **Description**: Update user's to-do item
  - **Request body**: To-do item

- **DELETE** `/api/todo/{id}`
  - **Authorize**: User/Admin
  - **Description**: Delete user's to-do item
  - **Request body**: None

### /api/user

- **GET** `/api/user`
  - **Authorize**: User/Admin
  - **Description**: Get my user
  - **Request body**: None
  - **Response body**: User

- **GET** `/api/user`
  - **Authorize**: Admin
  - **Description**: Get all users
  - **Request body**: None
  - **Response body**: List of users

- **POST** `/api/user`
  - **Authorize**: Admin
  - **Description**: Add a new user
  - **Request body**: User
  - **Response body**: User location

- **DELETE** `/api/user/{id}`
  - **Authorize**: Admin
  - **Description**: Delete user and all his to-do's
  - **Request body**: None

### /api/login

- **POST** `/api/login`
  - **Description**: Login user
  - **Request body**: User
  - **Response body**: JWT token

## Server Side Notes

- Only administrators can add/delete users.
- Users can handle only their own to-do items and cannot see to-do items of other users.
- Both to-do items and users are saved in a JSON file.
- Both to-do items and users are accessed via an injected service through an interface for easy switch to a DB-based app.
- An extension method of IServiceCollection is provided for easy registration.
- Each request is logged to a log file, including start date & time, name of controller & action, name of the logged-in user (if any), and the duration of operation in milliseconds.

## Client Side Notes

- The default page shows the user’s to-do list and allows adding/updating/deleting items.
- If there is no logged-in user (no token saved in local storage or the token expired), the user sees a login page instead of the default page.
- If the user has administrative privileges, there is a link from the to-do list page to the users list page, and vice versa.
- A Postman button is added to the login page.
- Allow users to update their own details (name, password).
- Let administrators watch and edit their own to-do items as a regular user.
- Allow users to log in using their Google account.





## Author

Shoshi Bnayahu

