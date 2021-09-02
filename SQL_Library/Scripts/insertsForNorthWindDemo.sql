/*
    Initial population of NorthWindDemo database, Customer table has no duplicates so this
    script will create six duplicate records
*/

INSERT INTO Customer
       SELECT CompanyName, 
              ContactName, 
              ContactTitle, 
              [Address], 
              City, 
              Region, 
              PostalCode, 
              Country, 
              Phone, 
              Fax, 
              Standing
       FROM Customer
       WHERE dbo.Customer.CustomerIdentifier IN(1, 20, 22, 30, 9, 50);