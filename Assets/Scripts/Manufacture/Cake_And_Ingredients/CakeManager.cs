using System.Collections.Generic;
using UnityEngine;

public enum recipeArrow {
    up = 0,
    right = 1,
    down = 2,
    left = 3
}

public class CakeManager : MonoBehaviour
{
    public CakeCollection cakeCollection;
    public List<Cake> cakes = new List<Cake>();
    // cakes는 직접접근 X - OpeningTimeData를 통해 접근할 것

    private void Awake()
    {
        InitializeCakes();
        SubscribeToEvents();
    }

    // 케이크 초기화
    private void InitializeCakes() // Cake의 정보는 미리 주어짐, 복사해서 전달 - 조작하면 됨.
    {
        CakeCollection cakes_origin = Instantiate(cakeCollection);
        cakes.Clear();
        foreach(Cake C in cakes_origin.cakes){
            cakes.Add(C);
        }
    }

    // 케이크 데이터 변경 이벤트 연결
    private void SubscribeToEvents()
    {
        
    }

    // 케이크 데이터 변경
    public void UpdateCakeData(int cakeIndex, int quantityChange, int salesChange)
    {
        if(cakeIndex < 0 || cakeIndex >= cakes.Count){
            Debug.Log("Invalid Cake Update Request : " + cakeIndex);
            return;
        }
        var cake = cakes[cakeIndex];
        if (cake != null)
        {
            cake.quantity = cake.quantity + quantityChange;
            cake.sales = cake.sales + salesChange;
        }
    }

}
