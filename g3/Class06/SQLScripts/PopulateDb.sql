INSERT INTO Users (Username, Password, FirstName, LastName, Age) VALUES 
('johndoe', 'password123', 'John', 'Doe', 29),
('janedoe', 'securepass', 'Jane', 'Doe', 25),
('mikesmith', 'passw0rd', 'Mike', 'Smith', 34),
('emilyjohnson', 'qwerty123', 'Emily', 'Johnson', 28),
('davemiller', 'letmein', 'Dave', 'Miller', 42);
GO


INSERT INTO Notes (Text, Color, Tag, UserId) VALUES 
('Grocery shopping list', 'red', 1, 1),
('Meeting notes', 'blue', 2, 2),
('Vacation plans', 'green', 3, 3),
('Workout routine', 'yellow', 1, 4),
('Project ideas', 'purple', 2, 5);
GO

INSERT INTO Contacts (UserFullName, Phone, Address, Email, UserId) VALUES 
('John Doe', '123-456-7890', '123 Elm St, Springfield, IL', 'johndoe@example.com', 1),
('Jane Doe', '234-567-8901', '456 Oak St, Springfield, IL', 'janedoe@example.com', 2),
('Mike Smith', '345-678-9012', '789 Pine St, Springfield, IL', 'mikesmith@example.com', 3),
('Emily Johnson', '456-789-0123', '101 Maple St, Springfield, IL', 'emilyjohnson@example.com', 4),
('Dave Miller', '567-890-1234', '202 Birch St, Springfield, IL', 'davemiller@example.com', 5);
GO