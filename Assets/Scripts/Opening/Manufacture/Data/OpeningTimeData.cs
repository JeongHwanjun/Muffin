using System.Collections.Generic;
using UnityEngine;

public class OpeningTimeData : MonoBehaviour
{
    private CakeManager cakeManager;


    void Start()
    {
        cakeManager = CakeManager.Instance;
        cakeManager.UpdateCakeData(0, 8, 1);
    }

    public void UpdateCakeData(int cakeIndex, int quantityChange = 0, int salesChange = 0)
    {
        cakeManager.UpdateCakeData(cakeIndex, quantityChange, salesChange);
    }

    public List<Cake> GetCakeData()
    {
        if (cakeManager == null) cakeManager = CakeManager.Instance;
        return cakeManager.cakes;
    }

    public int GetBaseCost()
    {
        return cakeManager.baseCost;
    }
}
