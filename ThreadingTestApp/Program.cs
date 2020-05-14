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
            //ViewProcessConformation();
            NotepadProcessManipulations();

            Console.WriteLine("Главный поток завершён!");
            Console.ReadLine();
        }

        private static void NotepadProcessManipulations()
        {
            var process = Process.Start("notepad.exe");
            Console.ReadLine();

            Console.WriteLine("Процесс {0}", process.HasExited ? "выгружен" : "работает");
            var pid = process.Id;
            Console.ReadLine();

            var pid_process = Process.GetProcessById(pid);

            Console.WriteLine(pid_process.PriorityClass);
            Console.ReadLine();

            pid_process.PriorityClass = ProcessPriorityClass.High;
            Console.WriteLine(pid_process.PriorityClass);

            Console.ReadLine();
            Console.WriteLine("Выгружаю процесс");
            pid_process.Kill();
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
