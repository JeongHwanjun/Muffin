using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class OpeningTimeManager : MonoBehaviour
{
    public OpeningTimeData openingTimeData;
    [SerializeField] private GameObject manufactureAdmin;
    [SerializeField] private GameObject salesAdmin;
    [SerializeField] private ManufactureEventManager manufactureEventManager;
    public int salesX = -15, salesY = 0;

    private CakeManager cakeManager;
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        cakeManager = GetComponentInChildren<CakeManager>();
        openingTimeData = GetComponentInChildren<OpeningTimeData>();
        manufactureEventManager = GetComponentInChildren<ManufactureEventManager>();

        // 이벤트 구독
        if (manufactureEventManager)
        {
            manufactureEventManager.OnCommandValid += ProduceCake;
        }
    }

    public void UpdateCake(int cakeIndex, int quantityChange = 0, int salesChange = 0)
    {
        // 여기선 변화량만 전달하고, cakeManager에선 기존의 수량, 판매량 + 번화량을 반영한다.
        Debug.Log("OpeningTimeManager : UpdateCake");
        openingTimeData.UpdateCakeData(cakeIndex, quantityChange, salesChange);
    }

    private void ProduceCake(int cakeIndex)
    {
        int quantityChange = 1; // 케이크 생산 수량. 변경이 있다면 이 변수를 조작
        UpdateCake(cakeIndex, quantityChange);
        Debug.LogFormat("OpeningTimeManager : ProduceCake - Cake{0}, {1}", cakeIndex, quantityChange);
    }

    public List<Cake> GetCakes()
    {
        return openingTimeData.GetCakeData();
    }

    public int GetBaseCost()
    {
        return openingTimeData.GetBaseCost();
    }
}
