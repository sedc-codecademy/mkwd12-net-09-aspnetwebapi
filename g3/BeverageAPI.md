##Beverage API

Goal:
Create an API for a liquor store that sells different types of beverages and provides basic user authentication.

Technical Requirements:
Project Framework: Use .NET 6 to create the API.
Project Structure: Ensure that classes and methods are structured in a way that promotes reusability.


Public Endpoints (No Authentication Required):
View all beverages.
View a single beverage by ID.
Filter beverages by type (Alcohol, Soft drink, Beer) or by name.
Admin Endpoints (Authentication Required):
Add, update, or delete beverages (Admins only).


User Authentication:
Users can register and log in with email and password.
When registering, check that all fields are filled, and that "Password" and "ConfirmPassword" match. Return an appropriate error message if they don’t.
Once authenticated, users receive a token for further actions.


Beverage Entity:
Name
Type (Alcohol, Soft drink, Beer)
Quantity in stock
Price
User Entity:
FirstName
LastName
Email
Password
ConfirmedPassword


Testing:
Use Postman to test the API endpoints.
Share the Postman collection for testing.
