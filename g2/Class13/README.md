# QA and testing 🚑

## Testing and QA process 🔶

The development cycle of an application is a complex process composed of many phases that each have its own complexity and requirements. In order to ship a complete and quality product, there needs to be adequate knowledge of every phase of that cycle. A development cycle usually goes like this:

1. Understanding the requirements
2. Designing and planning
3. Development
4. Testing
5. Deployment

### QA 🔹

The quality of the software will not be dictated by only the patterns and quality of the code that you use. A large portion of the quality of the software that we are building is how good and precise it represents the requirements that were laid out in the first phases of the development process. How well is it representing the idea and the solution of the problem that the project is intended to solve? This usually means that if we use the most high-tech libraries and patterns but do not cover some important requirements or guidelines, we will still have a bad application that is not doing the job it is meant to do. That is where the Quality Assurance teams come to play. These are people that not only test our application but understand the requirements, and the problem and have a vision for the final solution. Most of the QA testing is done by:

* Manual testing ( QA employee manually tests the application )
* Automatic testing ( QA employee programs scripts that will test parts automatically and create automatic reports )

Besides testing, they write specifications, and reports, write automated tests and sequences as well as give feedback to the development team for the work that has been done. QA is an integral part of developing quality and robust applications.

### Testing phase 🔹

In this phase, there is already implemented and there are already requirements and expectations on how it should work. Because we know what to expect we can test the already done implementation to see if it meets the expectations and if everything works well. This can happen multiple times and it is repeatable until we or the requirements are satisfied.

### Developer tests 🔹

Internal tests that directly work with our application code can also be written to test each individual feature in the code separately if it works as expected. Since we are testing code, these tests are written by the developers themselves. These tests can not only test the features but also test other metrics such as speed, reaction time, security, coordination with other features as well as some abstract metrics such as complexity. There are many types of tests that can be written for a lot of different checks and insights in our application. Some of the more popular and often used are:

* Unit tests - Test the functionality of a particular feature or individual part of the application
* Integration tests - Test the functionality of a whole module, the connection, and communication between its parts
* System tests - Test the functionality of the whole implementation, the connection, and communication between all the logical modules

## Unit tests 🔶

Unit tests are the most direct and precise tests that you can write to test an independent functioning part of an application. They are used to test features separately if they work as intended in an isolated environment. This is why unit tests are written by developers that know the functions and also know the expectations and requirements. The percentage of code covered by tests is called code coverage and it is important to have covered a good portion of the code with tests like these. When the code changes we can just run the tests and see what parts are not working anymore.

### Unit tests structure

Unit tests follow a simple and effective structure formula. There are 3 main things that a unit test needs:

* Arrange - Part of the test where we create all the things that we will need for the tests such as hardcoded values, lists, or instances of classes that we will need
* Act - Part of the test that we execute the functionality with the already prepared resources in the Arrange part
* Assert - Part of the test where we compare the result and decide whether the test is successful or not

### Naming 🔹

There are many naming conventions for unit tests. But there are some similarities between them all making unit tests naming different from naming variables and functions. Names of unit tests don't have to be short. In fact, it is better for them to be long. The name should be descriptive enough for the person reading the name to get a basic understanding of:

* What is being tested
* What is the state of the entity or the case in which the test is focused on
* The outcome or the expectation

A common naming convention is: **MethodName_StateUnderTest_ExpectedBehavior**

Examples:

* CheckAge_AgeUnder18_False
* Sum_NonNumberInput_Exception

### Mocking 🔹

When we talk about unit tests and individual logic testing, individual and isolated really mean individual and isolated. If the service that we are using is dependent on some other class such as repository or another service we need those references to be carefully configured and arranged as well. To do this we must make a fake class that will give the same results as we expect. That is why we create a MOCK class that will do these things for us. To do this easily there are many libraries that mock classes for us. One of the most widely used is MOQ.

### Libraries and systems for writing unit tests 🔹

There are many libraries that help us write easier and better unit tests. Most of the libraries follow a similar formula. The difference is the syntax and some extra features that every library offers to the developer. The main idea is that we have a Test class that covers a range of tests from a similar or same logic section ( User Tests, Product Tests, etc ). In the test class, there are methods that are the actual tests and fields that are some values or instances of classes that we will need for all or most of the tests that we write in the test class. Some more popular libraries for unit tests in ASP.NET Core are:

* [XUnit](https://xunit.net/)
* [NUnit](https://nunit.org/)
* [MSTest](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)

### Visual Studio tests project and MSTests 🔹

The visual studio gives us a project template that is specially designed for testing. This project type is called a Test Project and there are 3 types that come with Visual Studio out of the box:

* MSTest Test Project
* NUnit Test Project
* XUnit Test Project

MSTest is a Microsoft testing tool that is integrated into Visual Studio and can be used for writing unit tests. This tool works similarly to others, with a test class and test methods as the main entities for writing tests. To run tests in Visual Studio just open the **Test Tab** and click **Run -> All**.

### Examples 🔹

#### Simple Unit Test Class

```csharp asp
// Service that will be tested
public class ValueService
{
    public int? Sum(int num1, int num2)
    {
        var result = num1 + num2;
        if (num1 > 0 && num2 > 0 && result < 0) return null;
        return result;
    }
    public bool IsFirstLarger(int num1, int num2)
    {
        return num1 > num2;
    }
    public string GetDigitName(int num)
    {
        List<string> names = new List<string>(){
            "Zero", "One", "Two", "Three", "Four",
            "Five", "Six", "Seven", "Eight", "Nine"};
        return names[num];
    }
}
```

```csharp asp
// Test class in the test project
[TestClass]
public class ValueTests
{
 private readonly ValueService _valueService;
    public ValueTests()
    {
        _valueService = new ValueService();
    }
    [TestMethod]
    public void Sum_PositiveIntegers_ResultNumber()
    {
        // Arrange
        int num1 = 5;
        int num2 = 10;
        int? expectedResult = 15;
        // Act
        int? result = _valueService.Sum(num1, num2);
        // Assert ( Are Equal - Test will pass if values are equal )
        Assert.AreEqual(expectedResult, result);
    }
    [TestMethod]
    public void Sum_LargeNumberIntegers_Null()
    {
        // Arrange
        int num1 = 2111111111;
        int num2 = 2111111111;
        // Act
        int? result = _valueService.Sum(num1, num2);
        // Assert ( IsNull - Test will pass if the result is null )
        Assert.IsNull(result);
    }
    [TestMethod]
    public void IsFirstLarger_FirstIsLarger_True()
    {
        // Arrange
        int num1 = 199;
        int num2 = 3;
        // Act
        bool result = _valueService.IsFirstLarger(num1, num2);
        // Assert ( IsTrue - Test will pass if the result is true )
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void IsFirstLarger_FirstIsNotLarger_False()
    {
        // Arrange
        int num1 = 125;
        int num2 = 126;
        // Act
        bool result = _valueService.IsFirstLarger(num1, num2);
        // Assert ( IsFalse - Test will pass if the result is true )
        Assert.IsFalse(result);
    }
    [TestMethod]
    public void GetDigitName_SingleDigit_SingleDigitName()
    {
        // Arrange
        int num = 7;
        string expectedResult = "Seven";
        // Act
        string result = _valueService.GetDigitName(num);
        // Assert ( Are Equal - Test will pass if values are equal )
        Assert.AreEqual(expectedResult, result);
    }
    // Expecting Exceptions
    // Expecting with attribute
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    [TestMethod]
    public void GetDigitName_DoubleDigit_Exception_Method1()
    {
        // Arrange
        int num = 12;
        // Act
        string result = _valueService.GetDigitName(num);
        // Assert
        // Will pass if the test throws an ArgumentOutOfRangeException
    }
    // Expecting with Assert
    [TestMethod]
    public void GetDigitName_DoubleDigit_Exception_Method2()
    {
        // Arrange
        int num = 12;
 // Act / Assert
        Assert.ThrowsException<NullReferenceException>(() => _valueService.GetDigitName(num));
    }
}
```

#### Implementation using Fake Repositories

```csharp asp
public class FakeUserRepository : IRepository<UserDto>
{
private List<UserDto> users;
public FakeUserRepository()
{
    var md5 = new MD5CryptoServiceProvider();
    var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes("123456sedc"));
    var hashedPassword = Encoding.ASCII.GetString(md5data);

    users = new List<UserDto>()
    {
 new UserDto(){
     Id = 1,
     FirstName = "Bob",
     LastName = "Bobsky",
     Username = "bob007",
     Password = hashedPassword
 }
    };
}
public void Add(UserDto entity)
{
    users.Add(entity);
}

public void Delete(UserDto entity)
{
    users.Remove(entity);
}

public IEnumerable<UserDto> GetAll()
{
    return users;
}

public void Update(UserDto update)
{
    users[users.IndexOf(update)] = update;
}
}
public class FakeNoteRepository : IRepository<NoteDto>
{
private List<NoteDto> notes;
public FakeNoteRepository()
{
    notes = new List<NoteDto>()
    {
 new NoteDto(){
     Id = 1,
     Text = "Don't forget to water the plant",
     Color = "blue",
     Tag = 2,
     UserId = 1
 },
 new NoteDto(){
     Id = 2,
     Text = "Drink more Tea",
     Color = "yellow",
     Tag = 3,
     UserId = 1
 }
    };
}
public void Add(NoteDto entity)
{
    notes.Add(entity);
}

public void Delete(NoteDto entity)
{
    notes.Remove(entity);
}

public IEnumerable<NoteDto> GetAll()
{
    return notes;
}

public void Update(NoteDto update)
{
    notes[notes.IndexOf(update)] = update;
}
}
```

#### Tests using fake repositories

```csharp asp
[TestClass]
public class NoteTests
{
 [TestMethod]
 public void GetUserNotes_ValidUserId_AllNotesForThatUser()
 {
     // Arrange
     INoteService noteService = new NoteService(new FakeUserRepository(), new FakeNoteRepository());
     int expectedResult = 2;
     int userId = 1;
     // Act
     IEnumerable<NoteModel> result = noteService.GetUserNotes(userId);
     // Assert
     Assert.AreEqual(expectedResult, result.ToList().Count);
 }
 [TestMethod]
 public void GetUserNotes_InvalidUserId_null()
 {
     // Arrange
     INoteService noteService = new NoteService(new FakeUserRepository(), new FakeNoteRepository());
     int expectedResult = 0;
     int userId = 3;
     // Act
     IEnumerable<NoteModel> result = noteService.GetUserNotes(userId);
     // Assert
     Assert.AreEqual(expectedResult, result.ToList().Count);
 }
 [TestMethod]
 public void GetNote_ValidUserId_Note()
 {
     // Arrange
     INoteService noteService = new NoteService(new FakeUserRepository(), new FakeNoteRepository());
     int userId = 1;
     int noteId = 1;
     int expectedResult = 1;
     // Act
     NoteModel result = noteService.GetNote(noteId, userId);
     // Assert
     Assert.AreEqual(expectedResult, result.Id);
 }
 [TestMethod]
 public void GetNote_InvalidUserId_Exception()
 {
     // Arrange
     INoteService noteService = new NoteService(new FakeUserRepository(), new FakeNoteRepository());
     int userId = 1;
     int noteId = 9;
     // Act / Assert
     Assert.ThrowsException<NullReferenceException>(() => noteService.GetNote(noteId, userId));
 }
}
```

#### Mocking Repositories

```csharp asp
public static class MockHelper
{
 public static IRepository<UserDto> MockUserRepository()
 {
     var md5 = new MD5CryptoServiceProvider();
     var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes("123456sedc"));
     var hashedPassword = Encoding.ASCII.GetString(md5data);
     List<UserDto> users = new List<UserDto>()
     {
  new UserDto(){
      Id = 1,
      FirstName = "Bob",
      LastName = "Bobsky",
      Username = "bob007",
      Password = hashedPassword
  }
     };
     Mock<IRepository<UserDto>> mockUserRepository = new Mock<IRepository<UserDto>>();

     mockUserRepository.Setup(x => x.GetAll()).Returns(users);

     mockUserRepository.Setup(x => x.Add(
  It.IsAny<UserDto>())).Callback((UserDto user) =>
  {
      users.Add(user);
  });

     mockUserRepository.Setup(x => x.Update(
  It.IsAny<UserDto>())).Callback((UserDto user) =>
  {
      users[users.IndexOf(user)] = user;
  });

     mockUserRepository.Setup(x => x.Delete(
  It.IsAny<UserDto>())).Callback((UserDto user) =>
  {
      users.Remove(user);
  });
     return mockUserRepository.Object;
 }
 public static IRepository<NoteDto> MockNoteRepository()
 {
     List<NoteDto> notes = new List<NoteDto>()
     {
  new NoteDto(){
      Id = 1,
      Text = "Don't forget to water the plant",
      Color = "blue",
      Tag = 2,
      UserId = 1
  },
  new NoteDto(){
      Id = 2,
      Text = "Drink more Tea",
      Color = "yellow",
      Tag = 3,
      UserId = 1
  }
     };
     Mock<IRepository<NoteDto>> mockNotesRepository = new Mock<IRepository<NoteDto>>();

     mockNotesRepository.Setup(x => x.GetAll()).Returns(notes);

     mockNotesRepository.Setup(x => x.Add(
  It.IsAny<NoteDto>())).Callback((NoteDto note) =>
  {
      notes.Add(note);
  });

     mockNotesRepository.Setup(x => x.Update(
  It.IsAny<NoteDto>())).Callback((NoteDto note) =>
  {
      notes[notes.IndexOf(note)] = note;
  });

     mockNotesRepository.Setup(x => x.Delete(
  It.IsAny<NoteDto>())).Callback((NoteDto note) =>
  {
      notes.Remove(note);
  });
     return mockNotesRepository.Object;
 }
}
```

#### Tests using mocked repositories

```csharp asp
[TestClass]
public class NoteTestsMoq
{
 [TestMethod]
 public void GetUserNotes_ValidUserId_AllNotesForThatUser()
 {
     // Arrange
     INoteService noteService = new NoteService(MockHelper.MockUserRepository(), MockHelper.MockNoteRepository());
     int expectedResult = 2;
     int userId = 1;
     // Act
     IEnumerable<NoteModel> result = noteService.GetUserNotes(userId);
     // Assert
     Assert.AreEqual(expectedResult, result.ToList().Count);
 }
 [TestMethod]
 public void GetUserNotes_InvalidUserId_null()
 {
     // Arrange
     INoteService noteService = new NoteService(MockHelper.MockUserRepository(), MockHelper.MockNoteRepository());
     int expectedResult = 0;
     int userId = 3;
     // Act
     IEnumerable<NoteModel> result = noteService.GetUserNotes(userId);
     // Assert
     Assert.AreEqual(expectedResult, result.ToList().Count);
 }
 [TestMethod]
 public void GetNote_ValidUserId_Note()
 {
     // Arrange
     INoteService noteService = new NoteService(MockHelper.MockUserRepository(), MockHelper.MockNoteRepository());
     int userId = 1;
     int noteId = 1;
     int expectedResult = 1;
     // Act
     NoteModel result = noteService.GetNote(noteId, userId);
     // Assert
     Assert.AreEqual(expectedResult, result.Id);
 }
 [TestMethod]
 public void GetNote_InvalidUserId_Exception()
 {
     // Arrange
     INoteService noteService = new NoteService(MockHelper.MockUserRepository(), MockHelper.MockNoteRepository());
     int userId = 1;
     int noteId = 9;
     // Act / Assert
     Assert.ThrowsException<NullReferenceException>(() => noteService.GetNote(noteId, userId));
 }
}
```

## Extra Materials 📘

* [Types of Software Testing](https://www.softwaretestinghelp.com/types-of-software-testing/)
* [Unit Testing in ASP.NET Core](https://code-maze.com/unit-testing-aspnetcore-web-api/)
* [Mocking a Database Repository With MOQ](https://www.codeproject.com/Articles/47603/Mock-a-Database-Repository-using-Moq)
* [Comparison between Testing Frameworks](https://xunit.net/docs/comparisons)
