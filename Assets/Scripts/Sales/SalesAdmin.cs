using System.Collections.Generic;
using UnityEngine;

public class SalesAdmin : MonoBehaviour
{
    private SalesInputManager salesInputManager;
    private SalesEventManager salesEventManager;
    private CustomerManager customerManager;
    private OpeningTimeManager openingTimeManager;
    [SerializeField]
    private SalesUIHandler salesUIHandler;
    public ScreenNumber startScreen;

    public void Initialize(OpeningTimeData openingTimeData, OpeningTimeManager _openingTimeManager){
        openingTimeManager = _openingTimeManager;
        salesInputManager = GetComponentInChildren<SalesInputManager>();
        salesInputManager.Initialize();
        salesEventManager = GetComponentInChildren<SalesEventManager>();
        customerManager = GetComponentInChildren<CustomerManager>();
        List<Cake> cakes = openingTimeManager.GetCakes();
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
        // 원래 데이터가 수정되면 그에 따른 이벤트를 발생시켜 UI를 갱신해야 함. 현재는 임시
        salesUIHandler.OnDataChanged(openingTimeManager.GetCakes());
    }

    private void setUI(ScreenNumber screenNumber){
        bool isSalesScreen = screenNumber == ScreenNumber.Sales;
        Debug.Log("setUI : " + isSalesScreen);
        salesUIHandler.SetUI(isSalesScreen);
        if(isSalesScreen) salesUIHandler.OnDataChanged(openingTimeManager.GetCakes());
    }

    void OnDestroy()
    {
        ScreenSwapper.OnScreenSwapComplete -= setUI;
        salesEventManager.OnConsumeCake -= ConsumeCake;
    }

    public List<Cake> GetCakes(){
        return openingTimeManager.GetCakes();
    }

    public void UpdateCake(int cakeIndex, int quantityChange = 0, int salesChange = 0){
        openingTimeManager.UpdateCake(cakeIndex, quantityChange, salesChange);
    }
}
