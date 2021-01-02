using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

/// <acknowledgements>
/// How to create new managed threads
/// https://docs.microsoft.com/en-us/dotnet/standard/threading/creating-threads-and-passing-data-at-start-time
/// </acknowledgements>
namespace ThreadingApp
{
    public class ThreadingApp
    {
        // The method that will be called when the thread is started.
        public void InstanceMethod()
        {
            int nProcessID = Process.GetCurrentProcess().Id;
            int curThread = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(
                "ThreadingApp.InstanceMethod is running on another thread. "  
                + nProcessID.ToString() +" "+ curThread.ToString());

            // Pause for a moment to provide a delay to make
            // threads more apparent.
            Thread.Sleep(3000);
            Console.WriteLine(
                "The instance method called by the worker thread has ended.");
        }

        public static void StaticMethod()
        {
            int nProcessID = Process.GetCurrentProcess().Id;
            int curThread = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(
                "ThreadingApp.StaticMethod is running on another thread. "
                + nProcessID.ToString() + " " + curThread.ToString());

            // Pause for a moment to provide a delay to make
            // threads more apparent.
            Thread.Sleep(5000);
            Console.WriteLine(
                "The static method called by the worker thread has ended.");
        }
    }

    public class Simple
    {
        public static void Main()
        {
            ThreadingApp serverObject = new ThreadingApp();

            // Create the thread object, passing in the
            // serverObject.InstanceMethod method using a
            // ThreadStart delegate.
            Thread InstanceCaller = new Thread(
                new ThreadStart(serverObject.InstanceMethod));

            // Start the thread.
            InstanceCaller.Start();

            Console.WriteLine("The Main() thread calls this after "
                + "starting the new InstanceCaller thread.");

            // Create the thread object, passing in the
            // serverObject.StaticMethod method using a
            // ThreadStart delegate.
            Thread StaticCaller = new Thread(
                new ThreadStart(ThreadingApp.StaticMethod));

            // Start the thread.
            StaticCaller.Start();

            Console.WriteLine("The Main() thread calls this after "
                + "starting the new StaticCaller thread.");
            Console.ReadLine();
        }
    }
    // The example displays the output like the following:
    //    The Main() thread calls this after starting the new InstanceCaller thread.
    //    The Main() thread calls this after starting the new StaticCaller thread.
    //    ServerClass.StaticMethod is running on another thread.
    //    ServerClass.InstanceMethod is running on another thread.
    //    The instance method called by the worker thread has ended.
    //    The static method called by the worker thread has ended.
}

