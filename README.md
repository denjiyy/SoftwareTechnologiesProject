# CHANGE THIS AS YOU SEE FIT!!!

# We have to insert the project title here

## Overview

This project is to be built using the **C#** programming language, leveraging the **ASP.NET Web Framework** to create dynamic web applications. It employs a **Code-First** approach with **EntityFrameworkCore** for database modeling and uses **MSSQL** as the underlying database system. The front end is constructed using **Razor Pages** with **Bootstrap** for responsive styling, while **JavaScript** is automatically generated for client-side validation and interactivity.

## Tech Stack

- **Programming Language**: C#
- **Framework**: ASP.NET (with Razor Pages)
- **ORM**: EntityFrameworkCore (Code-First)
- **Database**: MSSQL
- **Front-End Styling**: Bootstrap
- **Client-Side Interactivity**: JavaScript (auto-generated)

## Project Structure

1. **ASP.NET Framework**: Handles server-side logic and routes HTTP requests to Razor Pages, manages user input, sessions, and various web application components.
  
2. **EntityFrameworkCore (Code-First)**: Utilized for creating and managing database schemas directly through C# code. Migrations and database updates are handled via Entity Framework’s migration tools.

3. **MSSQL**: The project connects to an **MSSQL** database server, providing robust storage, transaction handling, and querying capabilities.

4. **Razor Pages**: This is used for dynamically generating the structure of web pages. It integrates seamlessly with the ASP.NET environment to offer a model-view-controller (MVC)-like structure.

5. **Bootstrap**: All front-end styling is done using **Bootstrap**, ensuring that the site remains responsive and visually consistent across different devices and screen sizes.

6. **JavaScript**: Front-end scripts for form validation, UI interactions, and basic logic are auto-generated, ensuring smooth user experiences and seamless client-server interactions.

## Features

- **Entity Framework Code-First**: Automatic generation of the database schema based on models, allowing for seamless migrations and updates.
  
- **Razor Pages**: Simplified page-based model for constructing web applications with clean separation of concerns between logic and UI.

- **Responsive Design**: The site is fully responsive, thanks to Bootstrap, ensuring compatibility with various devices and screen resolutions.

- **Auto-Generated JavaScript**: Validation and interactivity features are generated without requiring additional manual coding, speeding up the development process.

---

## Installation & Setup

To set up the project locally, follow the steps below:

1. Clone the repository.
2. Open the project in **Visual Studio**.
3. Set up the connection string for your **MSSQL** database in the `appsettings.json` file.
4. Run the following command in the **Package Manager Console** to apply migrations and create the database:

    ```bash
    Update-Database
    ```

5. Build and run the application using **IIS Express** or **Kestrel**.

---

## Contributing

Contributions are welcome! Feel free to open an issue or submit a pull request for any improvements.
