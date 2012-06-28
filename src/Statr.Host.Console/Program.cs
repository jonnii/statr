namespace Statr.Host.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine("Starting server");

            using (new ConsoleThreadHost())
            {
                System.Console.WriteLine("Press any key to stop the server");
                System.Console.ReadKey();
            }

            System.Console.WriteLine("!! SERVER STOPPED");
            System.Console.ReadKey();
        }
    }
}
