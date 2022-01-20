using System.Diagnostics;

var ws = new WebServer();

Stopwatch s = new Stopwatch();
List<Task> list = new();

try
{
    s.Start();
    for (int i = 0; i < 10; i++)
    {
        list.Add(ws.ProcessConnection(i));
    }

    s.Stop();
    Console.WriteLine("I'm here!");
    Console.WriteLine(s.Elapsed);

    await Task.WhenAll(list);

}
catch (Exception ex)
{
    Console.WriteLine($"Some tasks died {ex.Message}");
    foreach (var task in list)
    {
        if (task.IsFaulted)
            Console.WriteLine($"Here is an exception {task.Exception.InnerException.Message}");
        //Console.WriteLine(task.Exception);
    }

}

foreach (var task in list)
{
    if (task.IsCompleted)
        Console.WriteLine("Completed");
}


//return


public class WebServer
{

    public async Task ProcessConnection(int ClientId)
    {
        var t = Random.Shared.Next(1000, 10000);
        Console.WriteLine($"Got a new client, ID is id{ClientId}, and it's going to wait for {t}");
        //try {
        if (t > 6000)
            throw new InvalidOperationException($"Client id{ClientId} does not want to wait for {t}");
        //} catch {}

        await Task.Delay(t);

        Console.WriteLine($"Finished id{ClientId} it was waiting for {t}");
        //return t;
    }

}


