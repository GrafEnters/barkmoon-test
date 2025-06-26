[System.Serializable]
public class BreedsApiResponse
{
    public BreedData[] data;
}

[System.Serializable]
public class BreedData
{
    public string id;
    public string type;
    public BreedAttributes attributes;
}

[System.Serializable]
public class BreedAttributes
{
    public string name;
    public string description;
} 