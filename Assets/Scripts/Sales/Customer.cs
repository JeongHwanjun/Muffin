using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Customer : MonoBehaviour
{
    public int minQuantity = 1, maxQuantity = 3;
    private int cakeIndex, orderQuantity, selfIndex;
    private Cake cake;
    public string orderCakeName;
    public int[] like = {1,1,1};
    public float maximumWaiting = 5.0f;

    public CustomerManager customerManager;
    public CustomerUIHandler orderBubble;



    private void Update() {
        if(maximumWaiting < 0) {
            // CustomerManager의 List<Customer>에서 자신을 제외하기, 그외 기타 자잘한 처리
            customerManager.OnCustomerDeleted(gameObject);
        } else {
            // 대기시간 카운팅
            maximumWaiting -= Time.deltaTime;
        }
    }

    public void Initialize(List<Cake> cakes, CustomerManager _customerManager){
        customerManager = _customerManager;
        // 초기화. 이용가능한 케이크의 속성과 선호도를 활용해 케이크 및 주문수량 결정
        // 현재는 주어진 List<Cake> 중에서 무작위로 선택함
        System.Random random = new System.Random();
        cakeIndex = random.Next(0,cakes.Count);
        orderQuantity = random.Next(minQuantity,maxQuantity);

        cake = cakes[cakeIndex];
        Debug.Log(cake.name);

        orderBubble.Initialize(cake);
    }

    public void OnLineChange(int index){
        selfIndex = index;
        orderBubble.SetBubbleColor(selfIndex == 0);
    }
}
