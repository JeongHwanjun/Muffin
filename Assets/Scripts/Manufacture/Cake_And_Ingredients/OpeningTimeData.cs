using System.Collections.Generic;
using UnityEngine;

public class OpeningTimeData : MonoBehaviour
{
    public CakeManager cakeManager;


    void Start()
    {
        cakeManager.UpdateCakeData(0, 8, 1);
    }

    public void UpdateCakeData(int cakeIndex, int quantityChange = 0, int salesChange = 0){
        cakeManager.UpdateCakeData(cakeIndex, quantityChange, salesChange);
    }

    public List<Cake> GetCakeData(){
        return cakeManager.cakes;
    }
}
