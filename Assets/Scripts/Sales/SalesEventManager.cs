using System;
using System.Collections.Generic;
using UnityEngine;

public class SalesEventManager : MonoBehaviour
{
    public event Action<int, int> OnConsumeCake;
    public event Action<int> OnServeCake;
    public event Action<Customer> OnCustomerCreated;
    public event Action<ScreenNumber> OnMoveScreen;
    public static event Action<SalesEventManager> OnSalesEventManagerReady;

    private void Start() {
        OnSalesEventManagerReady?.Invoke(this);
    }

    public void TriggerCustomerCreated(Customer _customer){
        OnCustomerCreated?.Invoke(_customer);
    }

    public void TriggerServeCake(int _cakeNumber){
        OnServeCake?.Invoke(_cakeNumber);
    }

    public void TriggerConsumeCake(int cakeIndex, int consumeQuantity){
        OnConsumeCake?.Invoke(cakeIndex, consumeQuantity);
    }

    public void TriggerSwapScreen(ScreenNumber _screenNumber){
        OnMoveScreen?.Invoke(_screenNumber);
    }
}
