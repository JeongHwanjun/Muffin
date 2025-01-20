using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public float customerInterval;
    public List<Customer> customers;

    
    private bool validServe(){
        // 판매 행위가 실행되었을 때, 해당 행위가 손님의 주문에 부합하는지 판단.
        // 부합한다 -> true, 소비 이벤트 발생
        // 틀리다   -> false, 실패 함수 실행
        return true;
    }

    private void instantiateNewCustomer(){
        // 신규 고객을 생성해 customers에 추가함.
        // 
    }
}
