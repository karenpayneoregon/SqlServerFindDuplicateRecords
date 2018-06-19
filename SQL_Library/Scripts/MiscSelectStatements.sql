-----------------------------------------------------------------------------------
--- These statements where used in SSMS to validate the statements returned
--- expected results rather than code them into code. Always best to validate
--- this way rather than write statements in code and when they fail wonder why.
-----------------------------------------------------------------------------------

/*
	This is a formatted version of a duplicate statement againsts Customer table
	that was created in SSMS (SQL-Server Management Studio) to test against the 
	results created in this project.
*/
SELECT
    Cust.CompanyName ,
    Cust.ContactName ,
    Cust.ContactTitle ,
    Cust.Address ,
    Cust.City ,
    Cust.PostalCode,  COUNT(*)
FROM
    dbo.Customers AS Cust
GROUP BY
    Cust.CustomerIdentifier ,
    Cust.CompanyName ,
    Cust.ContactName ,
    Cust.ContactTitle ,
    Cust.Address ,
    Cust.City ,
    Cust.PostalCode
HAVING 
    COUNT(*) > 1


/*
	TechNet article: How to Remove Duplicates from a Table in SQL Server
	https://social.technet.microsoft.com/wiki/contents/articles/22706.how-to-remove-duplicates-from-a-table-in-sql-server.aspx

	The statement below was altered to match the table used to demo finding duplicates
*/
SELECT  Cust.CompanyName ,
        Cust.ContactName ,
        Cust.ContactTitle ,
        Cust.Address ,
        Cust.City ,
        Cust.PostalCode ,
        ROW_NUMBER() OVER ( PARTITION BY Cust.CompanyName, Cust.ContactName,
                            Cust.ContactTitle, Cust.Address, Cust.City,
                            Cust.PostalCode ORDER BY CompanyName ) AS Rnum
FROM    dbo.Customers AS Cust;


/*
	This statement was taken from 
	https://social.technet.microsoft.com/wiki/contents/articles/50921.t-sql-remove-duplicate-rows-from-a-table-using-query-without-common-table-expression.aspx

	and presented to the "Alternate" section of the article.
*/
SELECT  *
FROM    dbo.Customer
WHERE   CustomerIdentifier NOT IN ( SELECT  MIN(CustomerIdentifier)
                                    FROM    dbo.Customer
                                    GROUP BY CompanyName ,
                                            ContactName ,
                                            ContactTitle ,
                                            Address ,
                                            City ,
                                            PostalCode );

/*
	SELECT statement to get table names from the current catalog
*/

SELECT  TABLE_NAME
FROM    INFORMATION_SCHEMA.TABLES
WHERE   TABLE_TYPE = 'BASE TABLE'
        AND TABLE_NAME != 'sysdiagrams'
        AND TABLE_NAME NOT IN ( 'TableNames', 'TableColumnInformation' )
ORDER BY TABLE_NAME;



/*
	Get database names for current catalog filtering out unwanted databases
*/
SELECT  name
FROM    sys.sysdatabases
WHERE   name NOT IN ( 'master', 'msdb', 'tempdb', 'model' )
ORDER BY name;



/*
	Get primary key for a specific table
*/
SELECT  c.COLUMN_NAME ,
        c.ORDINAL_POSITION ,
        cls.DATA_TYPE
FROM    INFORMATION_SCHEMA.TABLE_CONSTRAINTS p
        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE c ON c.TABLE_NAME = p.TABLE_NAME
                                                            AND c.CONSTRAINT_NAME = p.CONSTRAINT_NAME
        INNER JOIN INFORMATION_SCHEMA.COLUMNS cls ON c.TABLE_NAME = cls.TABLE_NAME
                                                     AND c.COLUMN_NAME = cls.COLUMN_NAME
WHERE   CONSTRAINT_TYPE = 'PRIMARY KEY'
        AND c.TABLE_NAME = 'Customer'
ORDER BY c.TABLE_NAME;
