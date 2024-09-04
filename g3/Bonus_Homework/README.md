# Bonus Homework - For the bravest 💪

For the purpose of a liquer store that sell different type of beverages we need to create an API that will serve 
all the beverages data to the end user.

### Technical requierements:

* Create an API using .net 6 framework  
* The project should be structured nicely in order to accomplish decoupling and reusability of the things (classes, methods etc)
* Every user should have access to all the beverages data and single beverage data
* Every user can filter the beverages by type and/or by name
* Only the authenticated users in the system can also add, update or delete beverages from the system 
* Only the authenticated users can make an order with only one beverage, or list of beverages.
* When registering new user, validate if all the fields are provided corectly. 
* Also if the user enter different password for the fields Password and ConfirmPassword you should return appropriate message for that and let him try again
* Authenticate the user with the Email and Password

The beverage entity should have 
* Name
* Type (Alchocol, Soft drink, Beer)
* Quantity in stock
* Price

The user entity should have
* FirstName
* LastName
* Email
* Password
* ConfirmedPassword 

In order to test the API use Postman. Create a collection so that you can share with us in order to test your API easily

