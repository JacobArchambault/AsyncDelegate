using System;
using static System.Console;
using static System.Threading.Thread;

namespace AsyncDelegate
{
    public delegate int BinaryOp(int x, int y);

    class Program
    {
        static void Main()
        {
            WriteLine("***** Async Delegate Invocation *****");

            // Print out the ID of the executing thread.
            WriteLine($"Main() invoked on thread {CurrentThread.ManagedThreadId}.");

            // Invoke Add() on a secondary thread.
            BinaryOp b = new BinaryOp(Add);
            IAsyncResult ar = b.BeginInvoke(10, 10, null, null);

            // This message will keep printing until the Add() method is finished.
            while (!ar.AsyncWaitHandle.WaitOne(1000, true))
            {
                WriteLine("Doing more work in Main()!");
            }

            // Obtain the result of the Add() method when ready.
            int answer = b.EndInvoke(ar);
            WriteLine($"10 + 10 is {answer}.");
            ReadLine();
        }
        static int Add(int x, int y)
        {
            // Print out the ID of the executing thread.
            WriteLine($"Add() invoked on thread {CurrentThread.ManagedThreadId}.");

            // Pause to simulate a lengthy operation.
            Sleep(5000);
            return x + y;
        }
    }
}
