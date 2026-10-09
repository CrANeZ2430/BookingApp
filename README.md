# BookingApp
A full-stack web application (ASP.NET Core + React) for managing and booking rooms.
### Room Booking Dashboard
![img2.png](./docs/images/img2.png)
### Profile Dashboard
![img1.png](./docs/images/img1.png)
### Profile Addition Page (right after registration)
![img.png](./docs/images/img.png)
### Booking Addition Page
![img3.png](./docs/images/img3.png)
### Members Listing Page (restricted permission)
![img4.png](./docs/images/img4.png)

## Technologies Used
* .NET 10 and ASP.NET Core Web API 🌐
* Mediatr 📮
* Entity Framework Core 🗄️
* Fluent Validation 🛡️
* Docker 🐳
* React ⚛️
* Auth0 🔑
* Nginx 🚦
* xUnit🧪
* Moq and Testcontainers 📦

## Architecture overview
* **Frontend:** **_React_** App built using **_Vite_** with **_Typescript_** served by npx. It handles authorization utilizing **_Auth0 SDK_**.
* **Backend:** Rest API built with **_.NET 10_** and **_ASP.NET Core_**. Backend project is separated into 4 layers: API, Application, Core and Infrastructure (**_Clean architecture_**). These layers are connected using **_MediatR_** library. It is also used as a domain events handler (**_DDD_**). Validation (_**Fluent Validation**_) and Logging behaviors are registered in the MediatR pipeline as well. Utilizes **_Auth0_** as an authorization provider to handle **_Role-based access control_** by utilizing different permissions in the **_JWT access token_**.
* **Database:** _**PostgreSQL**_ is used as a database provider. _**EntityFramework**_ is utilized as an ORM for sketching up migrations (**_Code first approach_**) and for database calls from code.
* **Reverse proxy:** **_Nginx_** is utilized as a reverse proxy for **_HTTPS_** redirection. It listens to all calls and redirects "/" calls to frontend container and "/api" calls to backend container.
* **Docker:** **_Docker compose_** initializes the project by creating containers in a strict order (PostgreSQL container → Migration container → Backend container → Frontend container → Nginx (reverse proxy) container).
* **Docker images:** Backend Dockerfile is used to build Migration and Backend containers, while Frontend one is used to built Fronend container with or without the hot reload. Both are utilizing mutistage build. **Backend image:** (.NET SDK image) `Restoring dependencies` → `Publishing into one folder` → `Installing dotnet ef` + `dotnet database update` or (ASP.NET Core image) `Launching web app`. Final step depends on the type of container. **Frontend image:** (Node.js image) `Intalling packages by npm ci` → `Serving app by Vite` (hot reload container) or `npm build` + `Serving static files (app) by npx` (without hot reload).
* **Testing:** **_xUnit_** and **_Moq_** are used for testing and faking dependencies, while **_Testcontainers_** is utilized for orchestrating PostgreSQL container integration tests.
* **GitHub actions:** For every feature one must create a branch. By making a pull request on GitHub, **_GitHub actions_** runs tests declared in the config.

## Prerequisites
Everything required on your machine for starting this project:
* Docker engine v26.x+ and Docker compose v2.x+
* Node.js (optional, for hot reload)

## Environment Variables
Reference table for all required key-value pairs (match this with a .env.example file in the repo root):

| Variable | Description | Example |
| :--- | :--- | :--- |
|`POSTGRES_USER`|DB admin username|`postgres`|
|`POSTGRES_PASSWORD`|DB admin password|`supersecret`|
|`POSTGRES_DB`|Production database name|`BookingAppDb`|
|`AUTH0_DOMAIN`|Auth0 tenant domain|`random.auth0.com`|
|`AUTH0_AUDIENCE`|Your backend domain|`http://localhost:8080`|
|`AUTH0_CLIENT_ID`|Your App's Auth0 client ID|`CduhfhchdhhrbdbD`|

## Getting started
```
# 1. Clone the repository
git clone https://github.com/your-username/your-repo.git
cd your-repo

# 2. Copy environment variables
cp .env.example .env

# 3. Build and start containers
docker compose up -d --build
```
**Local Ports:**
* Frontend: http://localhost:3000
* Backend API: http://localhost:8080 (use HTTP files inside project directory for testing)
* PostgreSQL: localhost:5432
