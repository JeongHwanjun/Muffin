using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public float popularity = 10.0f;
    [SerializeField]
    private float customerSpacing = 1.0f;
    private float customerInterval = 5.0f, customerCooltime = 5.0f;
    public List<GameObject> customers = new List<GameObject>();

    public GameObject CustomerPrefab;
    public SalesAdmin salesAdmin;
    public SalesEventManager salesEventManager;

    private List<Cake> cakes; // 이 cakes는 고정된 데이터, 스프라이트와 명칭을 받기 위한 변수임.

    public void Initialize(List<Cake> _cakes){
        cakes = _cakes;
        salesEventManager.OnServeCake += TryServeCake;
    }

    private void TryServeCake(int _cakeNumber){
        if(customers.Count <= 0) return;
        if (validServe(_cakeNumber))
        {
            Customer firstCustomer = customers[0].GetComponent<Customer>();
            salesEventManager.TriggerConsumeCake(_cakeNumber, firstCustomer.orderQuantity);
            Debug.Log("Serve Success!");
            //OnServeSuccess();
            salesEventManager.TriggerServeSuccess();
        }
        else
        {
            Debug.Log("Serve Failed!");
            //OnServeFailed();
            salesEventManager.TriggerServeFailed();
        }
        //DeleteCustomer(customers[0]);
    }
    
    private bool validServe(int _cakeNumber){
        // 판매 행위가 실행되었을 때, 해당 행위가 손님의 주문에 부합하는지 판단.
        Customer firstCustomer = customers[0].GetComponent<Customer>();

        bool isSameCakeIndex = _cakeNumber == firstCustomer.cakeIndex;
        Cake cake = salesAdmin.GetCakes()[_cakeNumber];
        bool isCakeQuantityOK = firstCustomer.orderQuantity <= cake.quantity;

        return isSameCakeIndex && isCakeQuantityOK;
    }

    private void InstantiateNewCustomer(){
        // 신규 고객을 생성해 customers에 추가함.
        if(CustomerPrefab != null){
            GameObject newCustomer = Instantiate(CustomerPrefab, transform);
            customers.Add(newCustomer);
            newCustomer.transform.SetLocalPositionAndRotation(new Vector3(0, customers.Count * customerSpacing, 0), quaternion.identity);
            newCustomer.GetComponent<Customer>().Initialize(cakes, this);
            // 추가 후, customer가 생성됨을 알림
            salesEventManager.TriggerCustomerCreated(newCustomer.GetComponent<Customer>());

            // customers를 처음부터 다시 배치함
            lineupCustomer();
        }
    }
    void Update()
    {
        customerCooltime -= Time.deltaTime;
        if(customerCooltime < 0){
            InstantiateNewCustomer();
            customerCooltime = customerInterval;
        }
    }

    private void OnServeSuccess(){
        // 성공시 시각적-청각적 처리들...
        DeleteCustomer(customers[0]);
    }

    private void OnServeFailed(){
        // 실패시 시각적-청각적 처리들...
        DeleteCustomer(customers[0]);
    }

    public void DeleteCustomer(GameObject deletedCustomer){
        customers.Remove(deletedCustomer);
        Destroy(deletedCustomer);

        lineupCustomer();
    }

    private void lineupCustomer(){
        int index = 0;
        foreach(GameObject customer in customers){
            customer.transform.SetLocalPositionAndRotation(new Vector3(0, index * customerSpacing, 0), quaternion.identity);
            customer.GetComponent<Customer>().OnLineChange(index);
            index++;
        }
    }
}
