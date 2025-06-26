using System.Threading;

public class RequestToken
{
    private CancellationTokenSource _cts = new CancellationTokenSource();
    public CancellationToken Token => _cts.Token;
    public void Cancel() => _cts.Cancel();
    public bool IsCancellationRequested => _cts.IsCancellationRequested;
} 