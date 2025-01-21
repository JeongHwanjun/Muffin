using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SalesInputManager : MonoBehaviour
{
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
            ServeCakeKeys[index].Enable();
        }
        SwitchToManufactureKey.performed += SwitchToManufacture;
    }

    private void OnDestroy() {
        for(int i=0;i<ServeCakeKeys.Length;i++){
            int index = i;
            ServeCakeKeys[index].performed -= ctx => ServeCake(index);
            ServeCakeKeys[index].Disable();
        }
    }

    private void ServeCake(int cakeIndex){
        salesEventManager.TriggerServeCake(cakeIndex);
    }

    private void SwitchToManufacture(InputAction.CallbackContext ctx){
        // 제작 화면으로 이동
    }
}
