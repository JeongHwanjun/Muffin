using System;
using UnityEngine;

public class SalesEventManager : MonoBehaviour
{
    public event Action<int> OnConsumeCake;
    public event Action<int> OnServeCake;
    public event Action<Customer> OnCustomerEnter;
}
