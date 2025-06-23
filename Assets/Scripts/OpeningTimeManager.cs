using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class OpeningTimeManager : MonoBehaviour
{
    public OpeningTimeData openingTimeData;
    [SerializeField] private GameObject manufactureAdmin;
    [SerializeField] private GameObject salesAdmin;
    public int salesX = -15, salesY = 0;

    private CakeManager cakeManager;
    void Start()
    {
        Initialize();
    }

    public void Initialize(){
        cakeManager = GetComponentInChildren<CakeManager>();
        openingTimeData = GetComponentInChildren<OpeningTimeData>();
    }

    public void UpdateCake(int cakeIndex, int quantityChange = 0, int salesChange = 0){
        // 여기선 변화량만 전달하고, cakeManager에선 기존의 수량, 판매량 + 번화량을 반영한다.
        Debug.Log("OpeningTimeManager : UpdateCake");
        openingTimeData.UpdateCakeData(cakeIndex, quantityChange, salesChange);
    }

    public List<Cake> GetCakes(){
        return openingTimeData.GetCakeData();
    }
}
