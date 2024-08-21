# Repo Radar

This project is a demo full-stack application featuring a client built with Angular (latest version - 18 - with standalone mode); and a server built with ASP.NET Core (latest version - 8).

The project was built from scratch (ng new & VS wizard's "New Project").

The central focus is the backend, showcasing various features such as a well-structured, layered architecture; REST compliant API; JWT Cookie Authentication; Communication with two of GitHub's APIs via an HTTP Client, and more.

## Initial Setup

### A. Server

1. **Start the Server:**
   -  Select the `"https"` launch profile (Kestrel via HTTPS).
   -  The browser should automatically open Swagger at [https://localhost:7076/swagger](https://localhost:7076/swagger).

### B. Client

1. **Browse to RepoRadar/ClientPages folder**

2. **Installation:**

   ```bash
   npm install
   ```

3. **Start the Client:**
   ```bash
   npm start
   ```
   The browser should automatically open at [http://localhost:4200/](http://localhost:4200/).

### C. Start Using - Login Credentials

Username: admin  
Password: abc123

#### Note About GitHub's API Rate Limits

I've added my personal access token to overcome basic rate limits (in appsettings.json).
[I am aware that in a real app this should be done with secrets or in another secure way - not in appsettings.json - especially since this is a public GitHub repo].

If you do reach the rate limit after all, you may put your own access token, or switch in Program.cs to the mock repository implementation.

## Two Central Models

Taking a moment to look at these is in my opinion the best way to get familiar with this project.

-  **UserAuthentication**

   1. Represents current authentication status (`isLoggedIn`).
   2. The REST API allows GETting (find out if the user is logged in or not), POSTing (doing login), DELETing (doing logout)

-  **Repo** & **ExtendedRepo**
   1. Represents basic data about a GitHub repository.
   2. The REST API allows GETting (multiple repos at once by their name - specifically to GET the user's favorites) and GETting by keyword (for search).
   3. Note: The Server uses `Repo` model while the client uses `ExtendedRepo` model - a repo with added `isFavorite` aspect.
      In essence, `ExtendedRepo` is compliance with OOP and to avoid handling multiple different yet related arrays (regular repos, favorite repos).
   4. The unique identifier of a repo is in this format: `owner/name`, i.e. `liorwebdemos/RepoRadar`.
      The slash (`/`) in the repo's unique identifier didn't allow to create "perfect" REST convention compatible endpoints - but they're "very RESTy" nonetheless.

## Additional Features (Partial List)

-  **JWT Cookie Authentication**
-  **Server Http Client**  
   _Communicates with GitHub's REST and GraphQL APIs. GraphQL allowed me to query multiple repos at once (when querying favorites) instead of making multiple round trips to the GitHub API._
-  **Mapping of DTOs to Entities**  
   _Basic mapping; no AutoMapper._
-  **Separation of Concerns**  
   _Everything in its own layer: Business Logic, Data Access, Routing (Controllers), Regular Models vs DTOs, etc._
   _(... Though not fully - some model validations were added as data annotations. It would've been cleaner to create a separate validators layer with FluentValidation, etc)._
-  **High/Low-Level Code Separation**  
   _Minimal low-level logic; when necessary, it's placed in static helpers._
-  **Documentation**  
   _Included for controller actions, models, and model properties (where needed). In a real project, there would've been more documentation. Also, there are technical comments throughout._
-  **Neat Work Environment**  
   _Formatting with Prettier and EditorConfig, project-level editor settings configurations, more..._

## Dependencies

### Client:

-  **TailwindCSS**
-  **class-transformer**  
   _(Wasn't mandatory, but I used it to create class instances from some anonymous objects)._

### Server:

-  **Newtonsoft.Json**
-  **Microsoft.AspNetCore.Authentication.JwtBearer**

## Final Note

A lot of small to medium compromises were made due to the fact that this is a demo project with a specific time frame - this is no enterprise level project:
From not adding a global middleware to catch, log, and re-throw exceptions in the back; to not showing a loading indicator while fetching data in the front; to using template-driven forms instead of reactive ones (which I always do); to not fully adopting a 3rd party UI library like Angular Material (which by the way, I have much experience with, including creating and customizing a theme: https://webforwin.com/projects/sqlink-pipeline/).
This is all to say that I can't even list the amount of small to medium compromises that I've knowingly made throughout the way.

That said, I believe that the many strong points that this project does possess help in hinting towards the fact that I am well-aware of all these additional aspects that are important in an enterprise app.
So even though one can't build an enterprise app in two days, I do believe this project showcases some of my abilities, strengths and overall attitude as a developer.

In any doubt, feel free to reach out to me. We can discuss different aspects of this project in detail, since they can never fit one README document.
Finally, to deliver sooner I also knowingly went a little off-script from initial requirements: i.e. there's no avatar of the owner. In another case, saving only the favorite repos unique identifier in the LS instead of the whole object has created a more work for me - but it has the advantage that data about favorites is always kept fresh.

## Enjoy!
