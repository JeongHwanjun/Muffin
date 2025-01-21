using UnityEngine;
using UnityEngine.PlayerLoop;

public class Customer : MonoBehaviour
{
    public int cakeId, orderQuantity, selfIndex;
    public string orderCakeName;
    public int[] like;
    public float maximumWaiting = 20f;

    public CustomerManager customerManager;

    private void Update() {
        if(maximumWaiting < 0) {
            // CustomerManager의 List<Customer>에서 자신을 제외하기, 그외 기타 자잘한 처리
        } else {
            // 대기시간 카운팅
            maximumWaiting -= Time.deltaTime;
        }
    }

    public void Initialize(){
        // 초기화. 이용가능한 케이크의 속성과 선호도를 활용해 케이크 및 주문수량 결정

    }
}
