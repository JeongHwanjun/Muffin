using System.Collections.Generic;
using UnityEngine;

public class SalesAdmin : MonoBehaviour
{
    private SalesInputManager salesInputManager;
    private SalesEventManager salesEventManager;
    private CustomerManager customerManager;
    private OpeningTimeManager openingTimeManager;
    [SerializeField]
    private GameObject salesUI;
    public ScreenNumber startScreen;

    public void Initialize(OpeningTimeData openingTimeData, OpeningTimeManager _openingTimeManager){
        openingTimeManager = _openingTimeManager;
        salesInputManager = GetComponentInChildren<SalesInputManager>();
        salesInputManager.Initialize();
        salesEventManager = GetComponentInChildren<SalesEventManager>();
        customerManager = GetComponentInChildren<CustomerManager>();
        List<Cake> cakes = openingTimeData.GetCakeData();
        customerManager.Initialize(cakes);
        // 이벤트 매니저가 준비되면 하위 이벤트 구독 (현재는 딱히 준비 필요 X)
        // SalesEventManager.OnSalesEventManagerReady += SubscribeEvents;
        salesEventManager.OnConsumeCake += ConsumeCake;
        ScreenSwapper.OnScreenSwapComplete += setUI;

        // 기타 설정
        setUI(startScreen);
    }

    private void ConsumeCake(int cakeIndex, int consumeQuantity){
        Debug.Log("Consume Cake");
        openingTimeManager.UpdateCake(cakeIndex, consumeQuantity, consumeQuantity);
    }

    private void setUI(ScreenNumber screenNumber){
        Debug.Log("setUI : " + (screenNumber == ScreenNumber.Sales));
        salesUI.SetActive(screenNumber == ScreenNumber.Sales);
    }

    void OnDestroy()
    {
        ScreenSwapper.OnScreenSwapComplete -= setUI;
    }
}
