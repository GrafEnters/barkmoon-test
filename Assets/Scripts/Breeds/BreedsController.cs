using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class BreedsController
{
    private readonly BreedsView _view;
    private readonly PopupController _popup;
    private readonly RequestQueue _queue;
    private const string BreedsUrl = "https://dogapi.dog/api/v2/breeds";
    private const string FactUrl = "https://dogapi.dog/api/v2/facts?breed_id={0}";
    private BreedData[] _breeds;
    private RequestToken _currentToken;
    private RequestToken _factToken;

    public BreedsController(BreedsView view, PopupController popup, RequestQueue queue)
    {
        _view = view;
        _popup = popup;
        _queue = queue;
        _view.OnBreedClick += OnBreedClick;
    }

    public void RequestBreeds()
    {
        CancelAll();
        _view.ShowLoader(true);
        _popup.Hide();
        _currentToken = new RequestToken();
        _queue.Enqueue(() => FetchBreeds(_currentToken));
    }

    public void CancelAll()
    {
        _currentToken?.Cancel();
        _factToken?.Cancel();
        _view.ShowLoader(false);
        _popup.Hide();
    }

    private async Task FetchBreeds(RequestToken token)
    {
        using (var req = UnityWebRequest.Get(BreedsUrl))
        {
            var op = req.SendWebRequest();
            while (!op.isDone)
            {
                if (token.IsCancellationRequested)
                    return;
                await Task.Yield();
            }
            if (req.result != UnityWebRequest.Result.Success)
                return;
            var json = req.downloadHandler.text;
            var data = JsonUtility.FromJson<BreedsApiResponse>(json);
            if (data?.data != null && data.data.Length > 0)
            {
                _breeds = data.data;
                int count = Mathf.Min(10, _breeds.Length);
                string[] names = new string[count];
                for (int i = 0; i < count; i++)
                    names[i] = $"{i+1} - {_breeds[i].attributes.name}";
                _view.UpdateBreeds(names);
            }
            _view.ShowLoader(false);
        }
    }

    private void OnBreedClick(int idx)
    {
        if (_breeds == null || idx < 0 || idx >= _breeds.Length)
            return;
        _factToken?.Cancel();
        _view.ShowLoader(true);
        _popup.Hide();
        var breed = _breeds[idx];
        _factToken = new RequestToken();
        _queue.Enqueue(() => FetchFact(breed, _factToken));
    }

    private async Task FetchFact(BreedData breed, RequestToken token)
    {
        string url = string.Format(FactUrl, breed.id);
        using (var req = UnityWebRequest.Get(url))
        {
            var op = req.SendWebRequest();
            while (!op.isDone)
            {
                if (token.IsCancellationRequested)
                    return;
                await Task.Yield();
            }
            if (req.result != UnityWebRequest.Result.Success)
                return;
            var json = req.downloadHandler.text;
            var data = JsonUtility.FromJson<BreedFactApiResponse>(json);
            string fact = (data?.data != null && data.data.Length > 0) ? data.data[0].attributes.body : breed.attributes.description;
            _popup.Show(breed.attributes.name, fact);
            _view.ShowLoader(false);
        }
    }
} 