namespace SumNumbersFromFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            goto AfterRefactor; // DO NOT USE goto!!! Only to skip the original code without uncommenting it or surrounding it with an if(false), only to keep the original level of indentation and keep the code highlighting active.
            // Before refactor - left here for reference
            // DISCLAIMER: I do not really understand what the author of this code is trying to achieve. Or in which version of C# he/she is trying to achieve it.
            // In my solution I asumed C# 7.3 and :NET Framework 4.*
            FileStream stream = new FileStream("datas.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            int line = 0;
            int parts = 0;
            int[] totalLines = new int[100]; // A List<int> would be more beneficial
            // Do you only want to read the file for the first missing line? Because the code starting with the for loop suggests otherwise.
            // You can not test an integer for null if its a non nullable value thype -> "int? line" can be null, but "int line can not" 
            while (line != null)
            {
                // If you just want to fix the conversation error use the commented out liens as a reference, but it will not solve the other problems:
                //string tempLine = reader.ReadLine();
                //if (int.TryParse(tempLine, out int convertedLine))
                //    continue;
                //totalLines[parts] = convertedLine;
                //parts++;
                // Original code inside the loop:
                line = Convert.ToInt32(reader.ReadLine()); // Conver.ToInt32 will never return null, because int is a value type, the default value of int is 0. Although, there are exceptions like: int? @int = null;
                totalLines[parts] = line; // line is an integer variable it will never be null here.
                parts++;
            }
            reader.Close(); // Use using block or in modern C# using statement
            stream.Close(); // Use using block or in modern C# using statement

            int dataParts = 0;
            for (int i = 0; i < totalLines.Length; i++)
            {
                if (totalLines[i] != null) // The integer array totalLines can not contain null value. From this if block I infer that there can be lines in your file that does not containg value, but again I do not know what the actual task is or what the datas.txt fie contains.
                {
                    dataParts++;
                }
            }
            Console.WriteLine($"The file contains datas from {dataParts} members.");

AfterRefactor:
            // After refactor - naive aproach, tried to follow the original code and not the task.
            string filename = "datas.txt";
            string[] dataFromFile = ReadDataFromFileIntoArray(filename);
            int[] processedData = GetIntegerDataParts(dataFromFile);
            Console.WriteLine($"The file contains datas from {processedData.Length} members.");
            // Another aproach
            Console.WriteLine($"Simple solution result: {SimpleSolution(filename).Length}");
            // Advanced
            int count = SimpleSolutionEnumerable(filename).Count();
            Console.WriteLine($"Count: {count}");

            Console.ReadKey(true);
        }

        private static string[] ReadDataFromFileIntoArray(string filename)
        {
            string[] dataLines = new string[100]; // For the sake of simplicity suppose that the file will contains at most 100 lines
            int index = 0;
            try
            {
                // If the file is not found return an empty collection
                if (!File.Exists(filename))
                {
                    // Log that the file you specified in the filename variable are not found
                    Console.WriteLine($"The input file named {filename} was not found!");
                    return Array.Empty<string>();
                }


                FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                // This will call Dispose automatically after exiting of the code block, and will also Close the FileStream
                using (StreamReader reader = new StreamReader(stream))
                {
                    do
                    {
                        dataLines[index] = reader.ReadLine(); // Read the input, and do nothing else
                        index++;
                    } while (!reader.EndOfStream && index < dataLines.Length);
                }
            }
            catch (IndexOutOfRangeException ex) // Catch your exceptions. More specific first and the most general comes last.
            {
                Console.WriteLine($"Index was out of range...{Environment.NewLine}{ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ther was an I/O exception...{Environment.NewLine}{ex.Message}");
            }
            catch (Exception ex) // For any other uncought exception
            {
                Console.WriteLine($"There was an error: {ex.Message}");
            }

            return dataLines;
        }

        private static int[] GetIntegerDataParts(string[] dataParts)
        {
            var parts = new List<int>();
            // Prefer iterator over loop if you iterate through the whole collection
            foreach (string part in dataParts)
            {
                // If the value was succesfully parsed into int, only then add it to the parts list
                if (int.TryParse(part, out int value))
                    parts.Add(value);
            }
            return parts.ToArray();
        }

        private static int[] SimpleSolution(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"The input file named {filename} was not found!");
                return Array.Empty<int>();
            }

            string[] dataLines = File.ReadAllLines(filename);
            var convertedData = new List<int>(dataLines.Length);
            foreach (var dataLine in dataLines)
            {
                if (int.TryParse(dataLine, out int value))
                    convertedData.Add(value);
            }

            return convertedData.ToArray();
        }

        private static IEnumerable<int> SimpleSolutionEnumerable(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"The input file named {filename} was not found!");
                yield break;
            }

            string[] dataLines = File.ReadAllLines(filename);
            var convertedData = new List<int>(dataLines.Length);
            foreach (var dataLine in dataLines)
            {
                if (int.TryParse(dataLine, out int value))
                    yield return value;
            }
        }
    }
}
