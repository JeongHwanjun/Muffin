using System;

[Serializable]
public class Ingredient
{
    public string name;
    public int usage;
    public int price;

    public event Action<int> OnUsageChanged;

    public void SetUsage(int newUsage)
    {
        if (usage != newUsage)
        {
            usage = newUsage;
            OnUsageChanged?.Invoke(usage);
        }
    }
}
