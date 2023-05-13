namespace Moody.Common.Tasks;

public static class TaskExtensions
{
    public static async void FireAndForget(this Task task)
    {
        try
        {
            await task;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public static async void FireAndForgetAlternative(this Task task)
    {
        await task.ContinueWith((t) =>
        {
            Console.WriteLine(t.Exception);
        }, TaskContinuationOptions.OnlyOnFaulted);
    }
    
}