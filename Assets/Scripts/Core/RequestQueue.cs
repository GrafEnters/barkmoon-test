using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RequestQueue
{
    private readonly Queue<Func<Task>> _queue = new Queue<Func<Task>>();
    private bool _isProcessing;

    public void Enqueue(Func<Task> request)
    {
        _queue.Enqueue(request);
        if (!_isProcessing)
            ProcessQueue();
    }

    private async void ProcessQueue()
    {
        _isProcessing = true;
        while (_queue.Count > 0)
        {
            var req = _queue.Dequeue();
            await req();
        }
        _isProcessing = false;
    }
} 