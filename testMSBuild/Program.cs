using System;
using System.Threading.Tasks;

namespace testMSBuild
{
    class Program
    {
        static async Task Main(string[] args)
        {
            bool repeat = true;
            do
            {
                string path = string.Empty;

                do
                {
                    Console.WriteLine("Zadej cestu k solution (.sln soubor):");
                    path = @Console.ReadLine();
                } while (!path.EndsWith(".sln"));

                try
                {
                    repeat = !await SyntaxUtils.MakeDocumentation(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + ": " + e.Source);
                    SyntaxUtils.LogWriter?.Log(e.Message + ": " + e.Source, LogWriter.LogType.Error);
                }
            } while (repeat);
            Console.WriteLine("Pro ukončení stiskněte libovolnou klávesu");
            Console.ReadKey();
        }
    }
}
