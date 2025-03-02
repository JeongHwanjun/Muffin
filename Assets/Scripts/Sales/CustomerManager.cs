using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public float popularity = 10.0f;
    [SerializeField]
    private float customerSpacing = 1.0f;
    private float customerInterval = 5.0f, customerCooltime = 0.0f;
    public List<GameObject> customers = new List<GameObject>();

    public GameObject CustomerPrefab;
    public SalesEventManager salesEventManager;

    private List<Cake> cakes;

    public void Initialize(List<Cake> _cakes){
        cakes = _cakes;
        salesEventManager.OnServeCake += TryServeCake;
    }

    private void TryServeCake(int _cakeNumber){
        Debug.Log("Try Serve Cake, CakeNumber : " + _cakeNumber);
        if(customers.Count <= 0) return;
        if(validServe(_cakeNumber)) {
            salesEventManager.TriggerConsumeCake(_cakeNumber, customers[0].GetComponent<Customer>().orderQuantity);
        }
    }
    
    private bool validServe(int _cakeNumber){
        // serveCake 이벶트를 구독, 해당 이벤트가 발생했을 때 실행.
        // 판매 행위가 실행되었을 때, 해당 행위가 손님의 주문에 부합하는지 판단.
        // 부합한다 -> true, 소비 이벤트 발생
        // 틀리다   -> false, 실패 함수 실행


        return true;
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

    void Start()
    {
        
    }

    void Update()
    {
        customerCooltime += Time.deltaTime;
        if(customerCooltime > customerInterval){
            InstantiateNewCustomer();
            customerCooltime = 0;
        }
    }

    public void OnCustomerDeleted(GameObject deletedCustomer){
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
