public interface IServerRequest<T>
{
    RequestToken Token { get; }
    System.Threading.Tasks.Task<T> ExecuteAsync();
} 