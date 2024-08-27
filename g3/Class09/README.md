# Workshop 🏗️
## Part 2
Now, we need to bring our Movie rental api one level up and switch the using of StaticDb into real database.

In order to accomplish the Part 2 workshop, we need to consider the following requirements
* Install EntitiyFramework and configure the MovieDbContext class]
* Seed data into the database so that we can test our endpoints
* Complete all the service methods for fetch of the movies
* Complete the MovieController with all of the following endpoints
    * get movie by id (two methods: route param and query string)
    * get all movies 
    * filter movies by genre and/or year
    * create new movie
    * update a movie
    * delete a movie (two methods: get the id from body, get the id from route)

A movie contains:
* id
* title - required field
* description
* year - required field
* genre - required field

## Add more DTOs for the methods that need to be implemented



