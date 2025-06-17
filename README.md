# ContactManager - ASP.NET Core MVC CRUD App

Small app using **ASP.NET Core MVC**, **SQLite**, and **vanilla HTML/CSS/JS**, built for technical interview.
Demonstrates layered ASP.NET Core MVC architecture with manual ADO.NET database access and a minimal frontend using plain JavaScript, HTML, and CSS.

---

## Functionality

Supports CRUD operations on a `Contact` entity, including:

- Creating a new contact
- Viewing the contact list (filtering/sorting included)
- Editing existing contacts
- Deleting contacts
- Returning validation errors on invalid input

---

## Architecture Overview

- **Controllers** handle incoming HTTP requests (used as API endpoints)
- **Services** contain application logic
- **Repositories** handle all database access
- **DTOs** transport-layer separating models from domain models
-  **Models** represent core domain objects like `Contact`

No frontend frameworks are used. The HTML/JS frontend is static and talks to the backend using **AJAX** requests.


