using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Customer : MonoBehaviour
{
    public int minQuantity = 1, maxQuantity = 3;
    private int selfIndex;
    public int cakeIndex;
    public int orderQuantity{get; private set;}
    public Cake cake{get; private set;}
    public string orderCakeName;
    public CustomerInfo customerInfo;

    public CustomerManager customerManager;
    public CustomerUIHandler orderBubble;

    private float waiting = 1.0f;
    private float maximumWaitingTime = 0.0f;

    private void Update() {
        if(waiting < 0) {
            // CustomerManager의 List<Customer>에서 자신을 제외하기, 그외 기타 자잘한 처리
            customerManager.DeleteCustomer(gameObject);
        } else {
            // 대기시간 카운팅
            waiting -= Time.deltaTime;
        }
    }

    public void Initialize(List<Cake> cakes, CustomerManager _customerManager, CustomerInfo whoami){
        customerManager = _customerManager;
        customerInfo = whoami;
        if (cakes.Count <= 0)
        {
            Debug.LogWarning("cakes.Count <= 0");
            return;
        }

        PickCake(cakes); // cakeIndex 설정
        PickQuantity(); // 주문수량 설정

        cake = cakes[cakeIndex];
        maximumWaitingTime = customerInfo.maximumWaiting;
        waiting = customerInfo.maximumWaiting;

        orderBubble.Initialize(cake, orderQuantity);
    }

    public void OnLineChange(int index)
    {
        selfIndex = index;
        orderBubble.SetBubbleColor(selfIndex == 0);
    }

    private void PickCake(List<Cake> cakes)
    {
        // 초기화. 이용가능한 케이크의 속성과 선호도를 활용해 케이크 및 주문수량 결정
        int[] roulette = new int[cakes.Count];
        for (int i = 0; i < cakes.Count; i++)
        {
            roulette[i] = cakes[i].preferences[selfIndex];
        }
        int bullet = Random.Range(0, roulette.Sum());
        int cumulative = 0, index = 0;
        foreach(int choice in roulette)
        {
            cumulative += choice;
            if (bullet < cumulative)
            {
                cakeIndex = index;
                Debug.LogFormat("Picking Cake : {0}", cakes[cakeIndex].name);
                break;
            }
            index++;
        }
    }
    
    private void PickQuantity()
    {
        orderQuantity = Random.Range(1, maxQuantity);
    }

    public float GetWaitingTime()
    {
        return waiting;
    }
    public float GetMaximumWaitingTime()
    {
        return maximumWaitingTime;
    }
}
