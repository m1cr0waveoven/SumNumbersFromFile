using BenchmarkDotNet.Running;

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
                //if (!int.TryParse(tempLine, out int convertedLine))
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
            string[] dataFromFile = Solutions.ReadDataFromFileIntoArray(filename);
            int[] processedData = Solutions.GetIntegerDataParts(dataFromFile);
            Console.WriteLine($"The file contains datas from {processedData.Length} members.");
            // Another aproach
            Console.WriteLine($"Simple solution result: {Solutions.SimpleSolution(filename).Length}");
            // Advanced
            int count = Solutions.SimpleSolutionEnumerable(filename).Count();
            Console.WriteLine($"Count: {count}");
            count = Solutions.EnumerableSolution2(filename).Count();
            Console.WriteLine($"Count: {count}");
            count = Solutions.LINQSolution4WithExceptionHandling(filename).Count();
            Console.WriteLine($"Count: {count}");


            var summary = BenchmarkRunner.Run<SolutionsBenchmarks>();
            File.WriteAllText(summary.ToString(), "results.txt");

            Console.ReadLine();
            Console.ReadKey(true);
        }

        
    }
}
