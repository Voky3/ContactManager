# ContactManager – ASP.NET Core MVC CRUD App

This project is a simple contact management web application built for technical interview.
It demonstrates layered ASP.NET Core MVC architecture with manual ADO.NET database access and a minimal frontend using plain JavaScript, HTML, and CSS.

---

## ? Purpose

App supports full CRUD operations on a `Contact` entity, including:

- Creating a new contact
- Viewing the contact list (filtering/sorting to be added)
- Editing existing contacts
- Deleting contacts
- Returning validation errors on invalid input

---

## ?? Architecture Overview

This is a **classic web app backend** using **ASP.NET Core MVC**:
- **Controllers** handle incoming HTTP requests (used as API endpoints)
- **Services** contain application logic
- **Repositories** handle all database access
- **DTOs** separate transport-layer models from domain models

No Razor or frontend frameworks are used — the HTML/JS frontend is static and talks to the backend using **AJAX** requests.

---

## ?? Folder Structure

