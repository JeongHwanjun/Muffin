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
    public GameObject[] screens; // <- 카메라여야함
    public GameObject clickHandler;
    private ScreenClickHandler screenClickHandler;
    public SalesEventManager salesEventManager;
    public ManufactureEventManager manufactureEventManager;

    public static event Action<ScreenNumber> OnMoveScreenComplete;

    [SerializeField] private ScreenNumber currentScreenNumber = ScreenNumber.Sales;

    void Awake()
    {
        // 초기화면으로 초기화
        if (screens.Length > 0)
        {
            foreach (GameObject screen in screens)
            {
                screen.SetActive(false);
            }
            screens[0].SetActive(true);
        }
        if (clickHandler != null)
        {
            screenClickHandler = clickHandler.GetComponent<ScreenClickHandler>();
            if (screenClickHandler != null) screenClickHandler.SwapScreen(0);
        }

        // EventManager 구독
        SubscribeEventManager();
    }

    private void MoveScreen(){
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

        screenClickHandler?.SwapScreen((int)currentScreenNumber);

        OnMoveScreenComplete?.Invoke(currentScreenNumber);
    }

    private void setCurrentScreen(ScreenNumber _screenNumber){
        Debug.Log("SetCurrentScreen : " + _screenNumber);
        if(screens.Length > 0){
            currentScreenNumber = (ScreenNumber)Mathf.Clamp((int)_screenNumber,0,screens.Length);
            MoveScreen();
        }
    }

    private void SubscribeEventManager()
    {
        Debug.Log("ScreenSwapper : 구독 완료!");
        salesEventManager.OnMoveScreen += setCurrentScreen;
        manufactureEventManager.OnMoveScreen += setCurrentScreen;
    }

    private void OnDestroy() {
        // 이벤트 할당 해제
        if(salesEventManager != null) salesEventManager.OnMoveScreen -= setCurrentScreen;
        if(manufactureEventManager != null) manufactureEventManager.OnMoveScreen -= setCurrentScreen;
    }
}
