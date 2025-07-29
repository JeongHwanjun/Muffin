using System.Collections.Generic;

[System.Serializable]
public class CakeSummary
{
    public string id;
    public string name;
    public string imagePath;
}

[System.Serializable]
public class CakeSummaryList
{
    public List<CakeSummary> cakes = new List<CakeSummary>();
}
