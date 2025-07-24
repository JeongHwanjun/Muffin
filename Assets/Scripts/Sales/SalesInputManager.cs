using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SalesInputManager : MonoBehaviour
{
    // 이 화면으로 전환되었을 때 입력을 활성화 해야 함
    public InputAction MoveToManufactureKey;
    public InputAction[] ServeCakeKeys;

    public SalesAdmin salesAdmin;
    public SalesEventManager salesEventManager;

    private List<Cake> CakeList;
    
    private void Awake()
    {
        for (int i = 0; i < ServeCakeKeys.Length; i++)
        {
            int index = i;
            ServeCakeKeys[index].performed += ctx => ServeCake(index);
        }
        MoveToManufactureKey.performed += SwitchToManufacture;

        ScreenSwapper.OnMoveScreenComplete += OnMoveScreenComplete;
    }

    private void OnDestroy() {
        for(int i=0;i<ServeCakeKeys.Length;i++){
            int index = i;
            ServeCakeKeys[index].performed -= ctx => ServeCake(index);
            ServeCakeKeys[index].Disable();
        }

        MoveToManufactureKey.performed -= SwitchToManufacture;

        ScreenSwapper.OnMoveScreenComplete -= OnMoveScreenComplete;
    }

    private void OnMoveScreenComplete(ScreenNumber _screenNumber){
        if(_screenNumber == ScreenNumber.Sales) {
            EnableInput();
            Debug.Log("판매 입력 활성화");
        } else {
            DisableInput();
        }
    }

    private void EnableInput() {
        foreach(InputAction ServeCakeKey in ServeCakeKeys) {
            ServeCakeKey.Enable();
        }
        MoveToManufactureKey.Enable();
    }
    private void DisableInput() {
        foreach(InputAction ServeCakeKey in ServeCakeKeys) {
            ServeCakeKey.Disable();
        }
        MoveToManufactureKey.Disable();
    }

    private void ServeCake(int cakeIndex)
    {
        salesEventManager.TriggerServeCake(cakeIndex);
    }

    private void SwitchToManufacture(InputAction.CallbackContext ctx){
        Debug.Log("제작 화면으로 이동");
        // 제작 화면으로 이동
        salesEventManager.TriggerSwapScreen(ScreenNumber.Manufacture);
    }
}
