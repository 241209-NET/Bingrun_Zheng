-- Server=localhost;User Id=sa;Password=NotPassword@123;TrustServerCertificate=true;

CREATE SCHEMA Project1;
Go

CREATE TABLE Project1.Owners(
    id int PRIMARY KEY IDENTITY,
    name VARCHAR(50) NOT NULL,
    phone VARCHAR(10) UNIQUE,
    address VARCHAR(255) UNIQUE
);

CREATE TABLE Project1.Pets(
    id int PRIMARY KEY IDENTITY,
    name VARCHAR(255) NOT NULL,
    animal_type VARCHAR(255) NOT NULL,
    age int CHECK (age > 0),
    birthday DATE DEFAULT '1990-01-01',
    ownerID INT FOREIGN KEY REFERENCES Project1.Owners(id) ON DELETE CASCADE
);

INSERT INTO Project1.Owners(name, phone, address) VALUES('Kung', '6098675309', '123 My.st USA');
INSERT INTO Project1.Pets(name, animal_type, ownerID) VALUES('Nyla', 'Cat', 1);

SELECT * FROM Project1.Owners;
SELECT * FROM Project1.Pets;