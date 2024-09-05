# Workshop 🏗️
## Part 3
We are close the finish of our Movie rent API. The last part of the workshop includes impelmentation of Authentication using JWT tokens.
In order to accomplish that we need to

* Implement logic for register/login a user (keep in mind the e2e implementation)
* Configure the JWT based authentication in Program.cs
* Create logic for generation of tokens and hashing user passwords
* Allow each user to have access to the login and register endpoint
* Protect all the endpoints from the MovieController. Only an authenticated user can access those resources
* Implement DTOs for the User logic

A User should contain:
* id
* FirstName - required field
* LastName - required field
* UserName - required field
* Password - required field

**Bonus**
 * **Use methods for generation of the token**
 * **Use methods for validation of each user property and return appropriate message**



