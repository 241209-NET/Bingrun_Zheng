-- On the Chinook DB, practice writing queries with the following exercises

-- BASIC CHALLENGES
-- List all customers (full name, customer id, and country) who are not in the USA
    SELECT FirstName + ' ' + LastName AS 'Full Name', customerid, country
    FROM Customer
    WHERE country != 'USA';

-- List all customers from Brazil
    SELECT FirstName + ' ' + LastName AS 'Full Name', customerid, country
    FROM Customer
    WHERE country = 'Brazil';

-- List all sales agents
    SELECT FirstName + ' ' + LastName AS 'Full Name', EmployeeId, Title
    FROM Employee
    WHERE Title = 'Sales Support Agent';

-- Retrieve a list of all countries in billing addresses on invoices
    SELECT DISTINCT BillingCountry FROM Invoice;

-- Retrieve how many invoices there were in 2009, and what was the sales total for that year?

    -- (challenge: find the invoice count sales total for every year using one query)
    SELECT COUNT(*) AS 'Total Invoices', SUM(Total) AS 'Sales Total'
    From Invoice
    WHERE InvoiceDate >= '2009-1-1' AND InvoiceDate < '2010-1-1';

-- how many line items were there for invoice #37
    SELECT COUNT(*) AS 'Total Items' FROM InvoiceLine
    WHERE InvoiceId = 37;

-- how many invoices per country? BillingCountry  # of invoices -
    SELECT COUNT(*) AS 'Total Invoice', BillingCountry
    FROM Invoice
    GROUP BY BillingCountry;

-- Retrieve the total sales per country, ordered by the highest total sales first.
    SELECT SUM(Total) AS 'Total Sales', BillingCountry
    From Invoice
    GROUP BY BillingCountry
    ORDER BY 'Total Sales' DESC;


-- JOINS CHALLENGES
-- Every Album by Artist
SELECT a.ArtistId, a.Name AS ArtistName, al.Title AS AlbumTitle
FROM Artist a
JOIN Album al ON a.ArtistId = al.ArtistId
ORDER BY a.Name, al.Title;

-- All songs of the rock genre  
SELECT t.Name AS SongName, g.Name AS Genre
FROM Track t
JOIN Genre g ON t.GenreId = g.GenreId
WHERE g.Name LIKE '%Rock%';

-- Show all invoices of customers from brazil (mailing address not billing)
SELECT i.*
FROM Invoice i
JOIN Customer c ON i.CustomerId = c.CustomerId
WHERE c.Country = 'Brazil';

-- Show all invoices together with the name of the sales agent for each one
Select i.InvoiceId, i.InvoiceDate, c.CustomerId, c.FirstName + ' ' + c.LastName 'Customer', e.FirstName + ' ' + e.LastName 'Sales Agent'
From Invoice i
JOIN Customer c ON i.CustomerId = c.CustomerId
JOIN Employee e ON c.SupportRepId = e.EmployeeId

-- Which sales agent made the most sales in 2009?
SELECT CONCAT(e.FirstName, ' ', e.LastName) AS 'Sales Agent', 
       COUNT(i.InvoiceId) AS TotalSales
FROM Employee e
JOIN Customer c ON e.EmployeeId = c.SupportRepId
JOIN Invoice i ON c.CustomerId = i.CustomerId
WHERE YEAR(i.InvoiceDate) = 2009
GROUP BY e.EmployeeId, e.FirstName, e.LastName
ORDER BY TotalSales DESC
OFFSET 0 ROWS FETCH NEXT 1 ROW ONLY;


-- How many customers are assigned to each sales agent?
SELECT CONCAT(e.FirstName, ' ', e.LastName) AS 'Sales Agent', 
       COUNT(c.CustomerId) AS NumberOfCustomers
FROM Employee e
LEFT JOIN Customer c ON e.EmployeeId = c.SupportRepId
GROUP BY e.EmployeeId, e.FirstName, e.LastName;

-- Which track was purchased the most ing 20010?
SELECT t.Name AS TrackName, COUNT(il.TrackId) AS PurchaseCount
FROM Track t
JOIN InvoiceLine il ON t.TrackId = il.TrackId
JOIN Invoice i ON il.InvoiceId = i.InvoiceId
WHERE YEAR(i.InvoiceDate) = 2010
GROUP BY t.TrackId, t.Name
ORDER BY PurchaseCount DESC
OFFSET 0 ROWS FETCH NEXT 1 ROW ONLY;

-- Show the top three best selling artists.
SELECT ar.Name AS ArtistName, COUNT(il.TrackId) AS SalesCount
FROM Artist ar
JOIN Album al ON ar.ArtistId = al.ArtistId
JOIN Track t ON al.AlbumId = t.AlbumId
JOIN InvoiceLine il ON t.TrackId = il.TrackId
GROUP BY ar.ArtistId, ar.Name
ORDER BY SalesCount DESC
OFFSET 0 ROWS FETCH NEXT 3 ROWS ONLY;

-- Which customers have the same initials as at least one other customer?
SELECT DISTINCT c1.FirstName, c1.LastName
FROM Customer c1
JOIN Customer c2 
    ON LEFT(c1.FirstName, 1) = LEFT(c2.FirstName, 1) 
    AND LEFT(c1.LastName, 1) = LEFT(c2.LastName, 1)
WHERE c1.CustomerId < c2.CustomerId;



-- ADVACED CHALLENGES
-- solve these with a mixture of joins, subqueries, CTE, and set operators.
-- solve at least one of them in two different ways, and see if the execution
-- plan for them is the same, or different.

-- 1. which artists did not make any albums at all?
SELECT a.Name
FROM Artist a
LEFT JOIN Album al ON a.ArtistId = al.ArtistId
WHERE al.AlbumId IS NULL;

-- 2. which artists did not record any tracks of the Latin genre?
SELECT a.Name
FROM Artist a
WHERE a.ArtistId NOT IN (
    SELECT DISTINCT ar.ArtistId
    FROM Artist ar
    JOIN Album al ON ar.ArtistId = al.ArtistId
    JOIN Track t ON al.AlbumId = t.AlbumId
    JOIN Genre g ON t.GenreId = g.GenreId
    WHERE g.Name = 'Latin'
);

-- 3. which video track has the longest length? (use media type table)
SELECT t.Name, t.Milliseconds / 1000.0 AS LengthInSeconds
FROM Track t
JOIN MediaType mt ON t.MediaTypeId = mt.MediaTypeId
WHERE mt.Name LIKE '%video%'
ORDER BY t.Milliseconds DESC
OFFSET 0 ROWS FETCH NEXT 1 ROW ONLY;

-- 4. find the names of the customers who live in the same city as the
--    boss employee (the one who reports to nobody)
WITH BossCity AS (
    SELECT City 
    FROM Employee 
    WHERE ReportsTo IS NULL
)
SELECT DISTINCT c.FirstName, c.LastName
FROM Customer c
JOIN BossCity bc ON c.City = bc.City;

-- 5. how many audio tracks were bought by German customers, and what was
--    the total price paid for them?
SELECT COUNT(DISTINCT il.TrackId) AS AudioTracksBought, 
       SUM(il.UnitPrice * il.Quantity) AS TotalPricePaid
FROM InvoiceLine il
JOIN Invoice i ON il.InvoiceId = i.InvoiceId
JOIN Customer c ON i.CustomerId = c.CustomerId
WHERE c.Country = 'Germany' 
  AND EXISTS (
    SELECT 1 
    FROM MediaType mt 
    JOIN Track t ON mt.MediaTypeId = t.MediaTypeId
    WHERE mt.Name LIKE '%audio%' AND t.TrackId = il.TrackId
  );

-- 6. list the names and countries of the customers supported by an employee
--    who was hired younger than 35.
SELECT c.FirstName, c.LastName, c.Country
FROM Customer c
JOIN Employee e ON c.SupportRepId = e.EmployeeId
WHERE DATEDIFF(year, e.HireDate, GETDATE()) < (35 - DATEDIFF(year, e.BirthDate, e.HireDate));


-- DML exercises

-- 1. insert two new records into the employee table.
INSERT INTO Employee (EmployeeId, LastName, FirstName, Title, ReportsTo, BirthDate, HireDate, Address, City, State, Country, PostalCode, Phone, Fax, Email)
VALUES (1001, 'Doe', 'John', 'Sales Support Agent', 2, '1980-01-01', '2023-01-01', '123 Example St', 'Anytown', 'CA', 'USA', '12345', '555-1234', '555-5678', 'john.doe@chinookcorp.com');

INSERT INTO Employee (EmployeeId, LastName, FirstName, Title, ReportsTo, BirthDate, HireDate, Address, City, State, Country, PostalCode, Phone, Fax, Email)
VALUES (1002, 'Smith', 'Jane', 'Sales Support Agent', 2, '1985-05-05', '2023-05-05', '456 Sample Ave', 'Sometown', 'NY', 'USA', '67890', '555-5678', '555-9012', 'jane.smith@chinookcorp.com');

-- 2. insert two new records into the tracks table.
INSERT INTO Track (Name, AlbumId, MediaTypeId, GenreId, Composer, Milliseconds, Bytes, UnitPrice)
VALUES ('New Track 1', 1, 1, 1, 'New Composer', 240000, 10000000, 0.99);

INSERT INTO Track (Name, AlbumId, MediaTypeId, GenreId, Composer, Milliseconds, Bytes, UnitPrice)
VALUES ('New Track 2', 2, 1, 2, 'Another Composer', 210000, 9000000, 0.99);

-- 3. update customer Aaron Mitchell's name to Robert Walter

-- 4. delete one of the employees you inserted.

-- 5. delete customer Robert Walter.