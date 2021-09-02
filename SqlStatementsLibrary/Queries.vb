Public Class Queries
    ''' <summary>
    ''' Although C# allows multi-line strings they must be enclosed in quotes while
    ''' with VB.NET we can discard quotes with XML Literals
    ''' </summary>
    ''' <returns>Primary key for @TableName set in C# code</returns>
    Public Shared ReadOnly Property IdentityStatement As String
        Get
            Return _
        <SQL>
            SELECT c.COLUMN_NAME 
            FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS p 
            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE c ON c.TABLE_NAME = p.TABLE_NAME AND c.CONSTRAINT_NAME = p.CONSTRAINT_NAME 
            INNER JOIN INFORMATION_SCHEMA.COLUMNS cls ON  c.TABLE_NAME = cls.TABLE_NAME AND c.COLUMN_NAME = cls.COLUMN_NAME 
            WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' AND c.TABLE_NAME = @TableName
        </SQL>.Value

        End Get
    End Property
    ''' <summary>
    ''' Get table names for a database
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property TableNamesStatement As String
        Get
            Return _
                <SQL>
                   SELECT 
	                   TABLE_NAME 
                   FROM 
	                   INFORMATION_SCHEMA.TABLES 
                   WHERE 
	                   TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME != 'sysdiagrams' AND TABLE_NAME NOT IN ('TableNames','TableColumnInformation') 
                   ORDER BY TABLE_NAME;
        </SQL>.Value

        End Get
    End Property

End Class
