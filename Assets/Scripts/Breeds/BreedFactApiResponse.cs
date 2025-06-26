[System.Serializable]
public class BreedFactApiResponse
{
    public FactData[] data;
}

[System.Serializable]
public class FactData
{
    public string id;
    public string type;
    public FactAttributes attributes;
}

[System.Serializable]
public class FactAttributes
{
    public string body;
} 