using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SalesInputManager : MonoBehaviour
{
    // 이 화면으로 전환되었을 때 입력을 활성화 해야 함
    public InputAction SwitchToManufactureKey;
    public InputAction[] ServeCakeKeys;

    public SalesAdmin salesAdmin;
    public SalesEventManager salesEventManager;

    private List<Cake> CakeList;

    public void Initialize(){

    }
    private void Awake() {
        for(int i=0;i<ServeCakeKeys.Length;i++){
            int index = i;
            ServeCakeKeys[index].performed += ctx => ServeCake(index);
        }
        SwitchToManufactureKey.canceled += SwitchToManufacture;

        ScreenSwapper.OnScreenSwapComplete += OnScreenSwapComplete;
    }

    private void OnDestroy() {
        for(int i=0;i<ServeCakeKeys.Length;i++){
            int index = i;
            ServeCakeKeys[index].performed -= ctx => ServeCake(index);
            ServeCakeKeys[index].Disable();
        }

        SwitchToManufactureKey.canceled -= SwitchToManufacture;

        ScreenSwapper.OnScreenSwapComplete -= OnScreenSwapComplete;
    }

    private void OnScreenSwapComplete(ScreenNumber _screenNumber){
        if(_screenNumber == ScreenNumber.Sales) {
            SwitchInput(true);
            Debug.Log("판매 입력 활성화");
        } else {
            SwitchInput(false);
        }
    }

    private void SwitchInput(bool ON) {
        if(ON){
            foreach(InputAction ServeCakeKey in ServeCakeKeys) {
                ServeCakeKey.Enable();
            }
            SwitchToManufactureKey.Enable();
        } else {
            foreach(InputAction ServeCakeKey in ServeCakeKeys) {
                ServeCakeKey.Disable();
            }
            SwitchToManufactureKey.Disable();
        }
    }

    private void ServeCake(int cakeIndex){
        salesEventManager.TriggerServeCake(cakeIndex);
    }

    private void SwitchToManufacture(InputAction.CallbackContext ctx){
        Debug.Log("제작 화면으로 이동");
        // 제작 화면으로 이동
        salesEventManager.TriggerSwapScreen(ScreenNumber.Manufacture);
    }
}
