using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public float customerInterval, customerSpacing = 0.5f;
    public List<GameObject> customers = new List<GameObject>();

    public GameObject CustomerPrefab;
    public SalesEventManager salesEventManager;

    private List<Cake> cakes;

    public void Initialize(List<Cake> _cakes){
        cakes = _cakes;
    }
    
    private bool validServe(){
        // 판매 행위가 실행되었을 때, 해당 행위가 손님의 주문에 부합하는지 판단.
        // 부합한다 -> true, 소비 이벤트 발생
        // 틀리다   -> false, 실패 함수 실행
        return true;
    }

    private void instantiateNewCustomer(){
        // 신규 고객을 생성해 customers에 추가함.
        if(CustomerPrefab != null){
            GameObject newCustomer = Instantiate(CustomerPrefab, transform);
            customers.Add(newCustomer);
            newCustomer.transform.SetLocalPositionAndRotation(new Vector3(0, customers.Count * customerSpacing, 0), quaternion.identity);
            newCustomer.GetComponent<Customer>().Initialize(cakes, this);
            // 추가 후, customer가 생성됨을 알림
            salesEventManager.TriggerCustomerCreated(newCustomer.GetComponent<Customer>());
        }
    }

    void Start()
    {
        instantiateNewCustomer();
    }

    public void OnCustomersChanged(){

    }
}
