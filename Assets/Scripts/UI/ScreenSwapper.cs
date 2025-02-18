using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum ScreenNumber{ // 스크린 번호를 가독성 좋게 표기하기 위한 enum
    Sales = 1,
    Manufacture = 0
}

public class ScreenSwapper : MonoBehaviour
{
    public GameObject[] screens;
    public GameObject clickHandler;
    private ScreenClickHandler screenClickHandler;
    private SalesEventManager salesEventManager;
    private ManufactureAdmin manufactureAdmin;

    public static event Action<ScreenNumber> OnScreenSwapComplete;

    [SerializeField]
    private ScreenNumber currentScreenNumber = ScreenNumber.Sales;

    void Awake()
    {
        // 초기화면으로 초기화
        if(screens.Length > 0){
            foreach(GameObject screen in screens){
                screen.SetActive(false);
            }
            screens[0].SetActive(true);
        }
        if(clickHandler != null){
            screenClickHandler = clickHandler.GetComponent<ScreenClickHandler>();
            if(screenClickHandler != null) screenClickHandler.SwapScreen(0);
        }

        // EventManager 구독
        SalesEventManager.OnSalesEventManagerReady += SubscribeSalesEventManager;
        ManufactureAdmin.OnManufactureAdminReady += SubscribeManufactureAdmin;
    }

    private void SwapScreen(){
        if((int)currentScreenNumber >= screens.Length){
            Debug.Log("Invalid Screen Number : " + currentScreenNumber);
            return;
        }

        if(screens.Length > 0){
            foreach(GameObject screen in screens){
                screen.SetActive(false);
            }
            screens[(int)currentScreenNumber].SetActive(true);
        }

        if(screenClickHandler != null){
            screenClickHandler.SwapScreen((int)currentScreenNumber);
        }

        OnScreenSwapComplete?.Invoke(currentScreenNumber);
    }

    private void setCurrentScreen(ScreenNumber _screenNumber){
        Debug.Log("SetCurrentScreen : " + _screenNumber);
        if(screens.Length > 0){
            currentScreenNumber = (ScreenNumber)Mathf.Clamp((int)_screenNumber,0,screens.Length);
            SwapScreen();
        }
    }

    private void SubscribeSalesEventManager(SalesEventManager _salesEventManager){
        Debug.Log("salesManager 구독 완료!");
        salesEventManager = _salesEventManager;
        salesEventManager.OnSwapScreen += setCurrentScreen;
    }

    private void SubscribeManufactureAdmin(ManufactureAdmin _manufactureAdmin){
        manufactureAdmin = _manufactureAdmin;
        manufactureAdmin.OnSwapScreen += setCurrentScreen;
    }

    private void OnDestroy() {
        // 이벤트 할당 해제
        if(salesEventManager != null) salesEventManager.OnSwapScreen -= setCurrentScreen;
        if(manufactureAdmin != null) manufactureAdmin.OnSwapScreen -= setCurrentScreen;
    }
}
