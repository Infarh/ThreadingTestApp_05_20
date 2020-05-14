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
        private static bool __CanClockWork = true;

        static void Main(string[] args)
        {
            //ViewProcessConformation();
            //NotepadProcessManipulations();
            //ProcessTextFiles();
           
            //StartConsoleHeaderClock();
            var clock_thread = new Thread(StartConsoleHeaderClock);
            clock_thread.IsBackground = true;
            clock_thread.Name = "Поток часов";
            clock_thread.Priority = ThreadPriority.AboveNormal;

            clock_thread.Start();
            Console.WriteLine("Идентификатор потока часов: {0}", clock_thread.ManagedThreadId);

            Console.ReadLine();
            //clock_thread.Join();
            //clock_thread.Abort(); // в крайнем случае! 
            //clock_thread.Interrupt();

            __CanClockWork = false;
            if(!clock_thread.Join(10))
                clock_thread.Interrupt();

            Console.WriteLine("Главный поток завершён!");
            Console.ReadLine();
            Console.WriteLine("Главный поток выгружен.");
        }

        private static void StartConsoleHeaderClock()
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine("Запущен поток: id:{0}, name:{1}, priority:{2}",
                thread.ManagedThreadId, thread.Name, thread.Priority);

            while (__CanClockWork)
            {
                Console.Title = DateTime.Now.ToString("HH:mm:ss.ffff");
                Thread.Sleep(100);
                //Thread.SpinWait(1000);
            }

            Thread.Sleep(500);

            Console.WriteLine("Поток часов завершил свою работу");
        }

        private static void ProcessTextFiles()
        {
            var start_process_info = new ProcessStartInfo("123.txt")
            {
                UseShellExecute = true
            };

            var started_process = Process.Start(start_process_info);
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
