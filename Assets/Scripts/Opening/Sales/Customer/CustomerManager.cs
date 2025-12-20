using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public float popularity = 10.0f;
    [SerializeField]
    private float customerSpacing = 1.0f;
    private float customerInterval = 5.0f, customerCooltime = 5.0f;
    public List<GameObject> customers = new List<GameObject>();
    public CustomerDatabase customerDatabase; // 손님별 정보

    public GameObject CustomerPrefab;
    public SalesAdmin salesAdmin;
    private SalesEventManager salesEventManager;

    private List<Cake> cakes; // 이 cakes는 고정된 데이터, 스프라이트와 명칭을 받기 위한 변수임.

    public void Initialize(List<Cake> _cakes)
    {
        salesEventManager = SalesEventManager.Instance;
        cakes = _cakes;
        salesEventManager.OnServeCake += TryServeCake;
        salesEventManager.OnServeSuccess += OnServeSuccess;
        salesEventManager.OnServeFailed += OnServeFailed;
    }

    private void TryServeCake(int _cakeNumber){
        if(customers.Count <= 0) return;
        if (validServe(_cakeNumber))
        {
            Customer firstCustomer = customers[0].GetComponent<Customer>();
            salesEventManager.TriggerConsumeCake(_cakeNumber, firstCustomer.orderQuantity);
            Debug.Log("Serve Success!");
            salesEventManager.TriggerServeSuccess();
        }
        else
        {
            Debug.Log("Serve Failed!");
            salesEventManager.TriggerServeFailed();
        }
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
            // 어떤 손님을 생성할지 결정함
            int customerId = PickCustomerType();
            newCustomer.GetComponent<Customer>().Initialize(cakes, this, customerDatabase.GetById(customerId));
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

        salesEventManager.TriggerCustomerDeleted();

        lineupCustomer();
    }

    private void lineupCustomer()
    {
        int index = 0;
        foreach (GameObject customer in customers)
        {
            customer.transform.SetLocalPositionAndRotation(new Vector3(0, index * customerSpacing, 0), quaternion.identity);
            customer.GetComponent<Customer>().OnLineChange(index);
            index++;
        }
    }
    
    private int PickCustomerType()
    {
        // cakes의 선호도를 기준으로 어떤 손님을 생성할지 확률적으로 결정함
        // 룰렛 휠 선택으로 결정
        // 가중치 총합
        int[] roulette = new int[customerDatabase.Customers.Count]; // 손님 종류만큼 나눔
        for (int i = 0; i < cakes.Count; i++)
        {
            for (int j = 0; j < customerDatabase.Customers.Count; j++)
            {
                roulette[j] = cakes[i].preferences[j];
            }
        }
        // 다트 던지기
        int bullet = UnityEngine.Random.Range(0, roulette.Sum());
        // 어디에 맞았는지 확인
        int cumulative = 0, index = 0;
        foreach(int choice in roulette)
        {
            cumulative += choice;
            if (bullet < cumulative)
            {
                Debug.LogFormat("Picking CustomerType : {0}", customerDatabase.Customers[index].displayName);
                return index;
            }
            index++;
        }
        return 0;
    }
}
