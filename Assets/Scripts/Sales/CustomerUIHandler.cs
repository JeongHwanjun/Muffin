using UnityEngine;

// 고객이 원하는 상품을 표기
public class CustomerUIHandler : MonoBehaviour{

    private void OnEnable()
    {
        
    }

    private void Update()
    {
        
    }

    public void Initialize(Cake _cake){
        Debug.Log(_cake.name);
    }
}