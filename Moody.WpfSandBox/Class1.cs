using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Moody.WpfSandBox
{
    public class Sample
    {
        
        public async Task SampleMethod()
        {

            Dispatcher currentDispatcher = null;
            Thread t = new Thread(() =>
            {
                currentDispatcher = Dispatcher.CurrentDispatcher;
                Dispatcher.Run();

            });
            
            t.SetApartmentState(ApartmentState.STA);
            t.Name = "DispatcherThread";
            t.Start();

            await Task.Delay(1000);

            await currentDispatcher.InvokeAsync(PostThreadOnConsole);
            
            // await PostThreadOnConsole();
            // await Task.Delay(1000).ConfigureAwait(true);
            // await PostThreadOnConsole();
            // await Task.Run(PostThreadOnConsole);
        }

        private async Task PostThreadOnConsole()
        {
            PostSynchronization();
            
            WriteThreadData("Before 1. Delay");
            await Task.Delay(100);
            
            PostSynchronization();
            
            WriteThreadData("Before 2. Delay");
            await Task.Delay(100);
            
            PostSynchronization();
            WriteThreadData("After Delay");

        }

        private static void PostSynchronization()
        {
            if (SynchronizationContext.Current == null)
            {
                Console.WriteLine("Thread has no Synchronization-Context!");
            }
            else
            {
                Console.WriteLine("Thread has Synchronization-Context!");
            }
        }

        private void WriteThreadData(string prefix)
        {
            Console.WriteLine($"{prefix} Thread-ID: {Thread.CurrentThread.ManagedThreadId} and Name: {Thread.CurrentThread.Name}");

        }
    }
}