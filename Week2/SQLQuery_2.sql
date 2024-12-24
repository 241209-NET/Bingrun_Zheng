CREATE DATABASE PetTracker;

CREATE TABLE Pets(
    name VARCHAR(50),
    type VARCHAR(50),
    description VARCHAR(255)
);

SELECT * FROM Pets;

INSERT INTO Pets VALUES('Nyla', 'Cat', 'Likes to puke');
INSERT INTO Pets VALUES('Twinchi', 'Chinchilla', 'Likes strawberry treats');
INSERT INTO Pets VALUES('Buddy', 'Beagle', 'Likes long walks');
INSERT INTO Pets VALUES('Everest', 'Dog', 'Chases tail');
INSERT INTO Pets VALUES('Rosie', 'Cat', 'Likes attention');
INSERT INTO Pets VALUES('Chula', 'Cat', 'Likes smelly clothes');

SELECT * FROM Pets
WHERE type = 'Dog';

GO

-- CREATE TABLE Pets2(
--     petID INT PRIMARY KEY IDENTITY(100,10),
--     name VARCHAR(50),
--     type VARCHAR(50),
--     descriptions VARCHAR(255)
-- );

-- drop table Pets2;

-- INSERT INTO Pets2 VALUES('Nyla', 'Cat', 'Likes to puke');
-- INSERT INTO Pets2 VALUES('Twinchi', 'Chinchilla', 'Likes strawberry treats');
-- INSERT INTO Pets2 VALUES('Buddy', 'Beagle', 'Likes long walks');
-- INSERT INTO Pets2 VALUES('Everest', 'Dog', 'Chases tail');
-- INSERT INTO Pets2 VALUES('Rosie', 'Cat', 'Likes attention');
-- INSERT INTO Pets2 VALUES('Chula', 'Cat', 'Likes smelly clothes');

-- SELECT * FROM Pets2;

-- DROP TABLE PETS;
-- DROP TABLE PETS2;



CREATE PROCEDURE dbo.GetPetType
    @Type VARCHAR(50)
AS
BEGIN
    SELECT * FROM Pets 
    WHERE type = @Type;
END;

DROP PROCEDURE GetPetType;

EXEC GetPetType @Type = 'Cat';



CREATE FUNCTION dbo.CountPetsByType 
(
    @Type VARCHAR(50)
)
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;
    SELECT @Count = COUNT(*) 
    FROM Pets 
    WHERE type = @Type;
    
    RETURN @Count;
END;

IF dbo.CountPetsByType('Chinchilla') > 0
BEGIN
    PRINT 'There are Chinchillas in the database.';
END
ELSE
BEGIN
    PRINT 'No Chinchillas found.';
END;