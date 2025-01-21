using System;
using UnityEngine;

public class SalesEventManager : MonoBehaviour
{
    public event Action<int> OnConsumeCake;
    public event Action<int> OnServeCake;
    public event Action<Customer> OnCustomerCreated;

    public void TriggerCustomerCreated(Customer _customer){
        OnCustomerCreated?.Invoke(_customer);
    }

    public void TriggerServeCake(int _cakeNumber){
        OnServeCake?.Invoke(_cakeNumber);
    }

    public void TriggerConsumeCake(int consumeQuantity){
        OnConsumeCake?.Invoke(consumeQuantity);
    }
}
