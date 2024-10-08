# CSV Analyzer
Command line program to analyze the length of the values of a CSV file

This is a command line program useful when resolving an importo to a SQL Database and a truncate exception is thrown. For example, when importing it through a Data Factory pipeline:
```
Failure happened on 'Sink' side. ErrorCode=SqlOperationFailed,'Type=Microsoft.DataTransfer.Common.Shared.HybridDeliveryException,
Message=A database operation failed. Please search error to get more details.,Source=Microsoft.DataTransfer.ClientLibrary,
''Type=System.InvalidOperationException,Message=The given value of type String from the data source cannot be converted to type nvarchar of the specified target column.,
Source=System.Data,''Type=System.InvalidOperationException,Message=String or binary data would be truncated.,Source=System.Data,'
```

The problem on this cases, when you have millions of rows in a CSV file with hundreds of columns, makes hard to find which row is causing the issue. 

The program, as result, shown the list of columns and the max lenth found for each column, on which line number and with which value. Then you can adjust your SQL database column size accordingly or take other actions. 
