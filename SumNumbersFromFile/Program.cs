namespace SumNumbersFromFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileStream stream = new FileStream("datas.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            int line = 0;
            int parts = 0;
            int[] totalLines = new int[100];
            while (line != null)
            {
                line = Convert.ToInt32(reader.ReadLine());
                totalLines[parts] = line;
                parts++;
            }
            reader.Close();
            stream.Close();

            int dataParts = 0;
            for (int i = 0; i < totalLines.Length; i++) {
                if (totalLines[i] != null)
                {
                    dataParts++;
                }
            }
            Console.WriteLine($"The file contains datas from {dataParts} members.");
        }
    }
}
