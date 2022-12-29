namespace Moody.Common.Tasks;

public static class TaskExtensions
{
    public static async void FireAndForget(this Task task)
    {
        await task.ContinueWith((t) =>
        {
            Console.WriteLine(t.Exception);
        }, TaskContinuationOptions.OnlyOnFaulted);
    }
}