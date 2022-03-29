# ONYXProducts
WebApi Technical Challenge for ONYX Capital

TECHNICAL CHALLENGE: 

Requirements

Create a .NET 5 or 6 “Products” Web API with the following endpoints:
GET / - anonymous, health check/OK endpoint
GET /api/products - secured (using your implementation of choice) endpoint returning a list of all products in JSON format
Include another secured endpoint to get all products of a specific colour
Include appropriate unit and integration tests
Push to a public repo on GitHub and send the link


NOTES:

The WebApi has been targeted for .NET 6

Folder structure:

I have implemented an Hexagonal/Clean Architecture approach to organize the Artifacts/Projects and as such:
- Domain Layer
- Application Layer
   - Ports definition (interfaces betwween application and adapters)
   - Services implementation
 - Adapters
   -  Persistence
   -  WebApi
- Configuration
   -  Mostly in WebApi project.

- There are 2 projects for tests: 
   - Integrations tests.
   - Unit tests.
 Both using some third party libraries: FluentAssertions, Moq, XUnit. 


I did not include a database interaction for simplicity, and I used some dummy data local.
I chose JWT Bearer as authorization process. As indicated in the code comments, the encrypted key was not saved in a secret or appsetting, it was just hardcoded for simplicity.

The healthcheck endpoind did not include any particular "check" so I make up this random check as a proof of concept of how I woud implement it.

Hope you like it!




