// Write a program in C# that reads a CSV file and displays the maximum string lenght for each column.
// The CSV file has a header line that contains the column names.
// The program should display the column names and the maximum string length.
// The program should be able to handle large files.
// The program should display the results as soon as possible.
// The program should not use a lot of memory.
// The program should be able to process files with a large number of columns.
// The program should be robust enough to handle malformatted files.
// The program should display appropriate error messages if the file is malformatted.
// The program should be able to process files with a large number of rows.

using System.Text.RegularExpressions;

public class CsvColumn
{
    public string Name;
    public int MaxLength;
    public string Value;
    public int LineNumber;
}

class Program

    
{
    static void Main(string[] args)
    {
        // check if the file path is provided in args[0]
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the file path as an argument.");
            return;
        }

        Console.WriteLine($"Processing CSV file '{args[0]}'...");

        string filePath = args[0]; // Update with your CSV file path

        try
        {
            using (var reader = new StreamReader(filePath))
            {
                int lineCount = 0;
                string headerLine = reader.ReadLine();
                if (headerLine == null)
                {
                    Console.WriteLine("The file is empty.");
                    return;
                }

                string[] headers = headerLine.Split(',');
                CsvColumn[] columns = new CsvColumn[headers.Length];
                foreach (var header in headers)
                {
                    columns[Array.IndexOf(headers, header)] = new CsvColumn { Name = header, MaxLength = 0, Value = "", LineNumber = 0 };
                }

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineCount++;
                    string[] fields = ParseCsvLine(line);

                    if (fields.Length != headers.Length)
                    {
                        Console.WriteLine($"[Warning] Line {lineCount}: Malformatted line detected. Skipping line.");
                        //continue;
                    }

                    for (int i = 0; i < fields.Length; i++)
                    {
                        if (fields[i].Length > columns[i].MaxLength)
                        {
                            columns[i].MaxLength = fields[i].Length;
                            columns[i].Value = fields[i];
                            columns[i].LineNumber = lineCount;
                        }
                    }
                }

                Console.WriteLine("Finished processing the file. Results:");

                for (int i = 0; i < headers.Length; i++)
                {
                    Console.WriteLine($"{headers[i]}: maxLength={columns[i].MaxLength}, line={columns[i].LineNumber}, value={columns[i].Value}");
                }

                Console.WriteLine("Done.");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("The specified file was not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static string[] ParseCsvLine(string line)
    {
        var pattern = @"(?:^|,)(?:""(?<value>(?:[^""]|"""")*)""|(?<value>[^,]*))";
        var matches = Regex.Matches(line, pattern);
        var values = new string[matches.Count];

        for (int i = 0; i < matches.Count; i++)
        {
            values[i] = matches[i].Groups["value"].Value.Replace("\"\"", "\"");
        }

        return values;
    }
}





