namespace SumNumbersFromFile
{
    internal static class Solutions
    {
        internal static string[] ReadDataFromFileIntoArray(string filename)
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

        internal static int[] GetIntegerDataParts(string[] dataParts)
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

        internal static int[] SimpleSolution(string filename)
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

        internal static int[] SimpleSolution2(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"The input file named {filename} was not found!");
                return Array.Empty<int>();
            }

            string[] dataLines = File.ReadAllLines(filename);
            return Array.ConvertAll(dataLines, Convert.ToInt32);
            //return Array.ConvertAll(dataLines, int.Parse);

        }
        internal static IEnumerable<int> SimpleSolutionEnumerable(string filename)
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
        internal static IEnumerable<string> EnumerableSolution1(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"The input file named {filename} was not found!");
                yield break;
            }

            using (var streamReader = new StreamReader(filename, new FileStreamOptions { Access = FileAccess.Read, Mode = FileMode.Open, Options = FileOptions.SequentialScan }))
            {
                while (!streamReader.EndOfStream)
                {
                    yield return streamReader.ReadLine();
                }
            }
        }

        internal static IEnumerable<int> EnumerableSolution2(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"The input file named {filename} was not found!");
                yield break;
            }

            using (var streamReader = new StreamReader(filename, new FileStreamOptions { Access = FileAccess.Read, Mode = FileMode.Open, Options = FileOptions.SequentialScan }))
            {
                while (!streamReader.EndOfStream)
                {
                    if (int.TryParse(streamReader.ReadLine(), out int value))
                        yield return value;

                    continue;
                }
            }
        }

        internal static IEnumerable<int> LINQSolution1(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"The input file named {filename} was not found!");
                return Enumerable.Empty<int>();
            }

            return File.ReadAllLines(filename).Select(line =>
                int.TryParse(line, out int value) ? value : default);
        }

        internal static IEnumerable<int> LINQSolution2(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"The input file named {filename} was not found!");
                return Enumerable.Empty<int>();
            }

            return from line in File.ReadAllLines(filename)
                   select int.TryParse(line, out int value) ? value : default;
        }

        internal static IEnumerable<int> LINQSolution3(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"The input file named {filename} was not found!");
                return Enumerable.Empty<int>();
            }

            // Throws FormatException if string is not convertable to int
            return File.ReadAllLines(filename)
                .Where(line => !string.IsNullOrEmpty(line))
                .Select(line => Convert.ToInt32(line));
        }

        internal static IEnumerable<int> LINQSolution4(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"The input file named {filename} was not found!");
                return Enumerable.Empty<int>();
            }

            // Throws FormatException if string is not convertable to int
            return from line in File.ReadAllLines(filename)
                   where !string.IsNullOrEmpty(line)
                   select Convert.ToInt32(line);
        }

        internal static IEnumerable<int> LINQSolution4WithExceptionHandling(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"The input file named {filename} was not found!");
                return Enumerable.Empty<int>();
            }

            return from line in File.ReadAllLines(filename)
                   where !string.IsNullOrEmpty(line)
                   select line.MapDefaultIfException<string, int, FormatException>(Convert.ToInt32);
        }
    }
}
