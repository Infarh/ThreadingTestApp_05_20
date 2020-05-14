using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;

namespace ThreadingTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ViewProcessConformation();

            Console.WriteLine("Главный поток завершён!");
            Console.ReadLine();
        }

        private static void ViewProcessConformation()
        {
            var processes = Process.GetProcesses();
            foreach (var process in processes)
                try
                {
                    Console.WriteLine("[pid:{0}] {1}",
                        process.Id, process.ProcessName);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
        }
    }
}
