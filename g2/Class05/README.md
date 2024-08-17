# Workshop 
## Part 1
We need to create an API that keeps and manages our favorite movies. It should have the option to:
* keep data in a fixed static class

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

## Use DTOs

## All fields except description are required. If description is entered maximum length is 250 characters.


