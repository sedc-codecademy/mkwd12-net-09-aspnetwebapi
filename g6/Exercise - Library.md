# Exercise

## Part 1
We need to create an API that keeps and manages our favorite books. It should have the option to:

* get book by id (two methods: route param and query string)
* get all books 
* filter books by genre and/or author
* create new book
* update a book
* delete a book (two methods: get the id from body, get the id from route)

A book contains:
* id
* title - required field
* description
* genre - required field

An author contains:
* id
* firstname - required field
* lastname - required field

One book can have one author and one author can write many books.

### Use DTOs

### All fields except description are required. If description is entered maximum length is 500 characters.

## Part 2

Create domain models

Create database

Create architecture and implement:
* Get
* Get by id
* Filter
* Add
* Update
* Delete

Refactor each method so it implemented using N-tier architecture

* Bonus: use the appSettings.json file for the connection string

