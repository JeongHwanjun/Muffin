using System.Collections.Generic;
using UnityEngine;

public class SalesAdmin : MonoBehaviour
{
    private SalesInputManager salesInputManager;
    private SalesEventManager salesEventManager;
    private CustomerManager customerManager;
    private OpeningTimeManager openingTimeManager;

    public void Initialize(OpeningTimeData openingTimeData, OpeningTimeManager _openingTimeManager){
        openingTimeManager = _openingTimeManager;
        salesInputManager = GetComponentInChildren<SalesInputManager>();
        salesInputManager.Initialize();
        salesEventManager = GetComponentInChildren<SalesEventManager>();
        customerManager = GetComponentInChildren<CustomerManager>();
        List<Cake> cakes = openingTimeData.GetCakeData();
        customerManager.Initialize(cakes);
    }
}
