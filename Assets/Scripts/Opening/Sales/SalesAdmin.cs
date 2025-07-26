using System.Collections.Generic;
using UnityEngine;

public class SalesAdmin : MonoBehaviour
{
    [SerializeField] private SalesInputManager salesInputManager;
    [SerializeField] private SalesEventManager salesEventManager;
    [SerializeField] private CustomerManager customerManager;
    [SerializeField] private OpeningTimeManager openingTimeManager;
    [SerializeField] private SalesUIHandler salesUIHandler;
    public ScreenNumber startScreen;

    public void Initialize(){
        // 정적 케이크 정보(스프라이트, 이름 등)를 customerManager에 넘겨 customer 생성 준비
        List<Cake> cakes = openingTimeManager.GetCakes();
        customerManager.Initialize(cakes);

        // 이벤트 구독
        salesEventManager.OnConsumeCake += ConsumeCake;
        ScreenSwapper.OnMoveScreenComplete += SetUI;

        // UI 설정
        SetUI(startScreen);
    }

    void Awake()
    {
        Initialize();
    }

    private void ConsumeCake(int cakeIndex, int consumeQuantity)
    {
        
        Debug.LogFormat("SalseAdmin : Consume Cake{0}", consumeQuantity);
        int quantityDiff = consumeQuantity > 0 ? -consumeQuantity : consumeQuantity;
        openingTimeManager.UpdateCake(cakeIndex, quantityDiff, consumeQuantity);
        // cake 데이터가 업데이트되면서 UI 갱신이 이루어짐
    }

    private void SetUI(ScreenNumber screenNumber)
    {
        bool isSalesScreen = screenNumber == ScreenNumber.Sales;
        Debug.Log("setUI : " + isSalesScreen);
        salesUIHandler.SetUI(isSalesScreen);
    }

    void OnDestroy()
    {
        ScreenSwapper.OnMoveScreenComplete -= SetUI;
        salesEventManager.OnConsumeCake -= ConsumeCake;
    }

    public List<Cake> GetCakes(){
        return openingTimeManager.GetCakes();
    }

    public void UpdateCake(int cakeIndex, int quantityChange = 0, int salesChange = 0){
        openingTimeManager.UpdateCake(cakeIndex, quantityChange, salesChange);
    }
}
