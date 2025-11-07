using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Customer : MonoBehaviour
{
    public int minQuantity = 1, maxQuantity = 3;
    private int selfIndex;
    public int orderQuantity{get; private set;}
    public int cakeIndex{get; private set;}
    public Cake cake{get; private set;}
    public string orderCakeName;
    public CustomerInfo customerInfo;

    public CustomerManager customerManager;
    public CustomerUIHandler orderBubble;

    private float waiting = 1.0f;

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
        
        PickCake(cakes);

        cake = cakes[cakeIndex];
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
        // 현재는 주어진 List<Cake> 중에서 무작위로 선택함
        System.Random random = new System.Random();
        cakeIndex = random.Next(0, cakes.Count);
        orderQuantity = random.Next(minQuantity,maxQuantity + 1);
    }
}
