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
    public List<Cake> cakes = new List<Cake>(); // 케이크 목록

    private void Awake()
    {
        InitializeCakes();
        SubscribeToEvents();
    }

    // 케이크 초기화
    private void InitializeCakes() // 추후 이미 작성된 Cake 객체의 정보를 그대로 저장하면 됨.
    {
        // 임시 데이터 넣기
        cakes.Add(new Cake
        {
            name = "Chocolate Cake", 
            quantity = 10,
            sales = 0,
            recipe = new List<recipeArrow> { recipeArrow.up, recipeArrow.right, recipeArrow.down, recipeArrow.left, recipeArrow.up },
            price = 15
        });

        cakes.Add(new Cake
        {
            name = "Strawberry Cake",
            quantity = 5,
            sales = 0,
            recipe = new List<recipeArrow> { recipeArrow.left, recipeArrow.left, recipeArrow.left, recipeArrow.right },
            price = 20
        });
    }

    // 케이크 데이터 변경 이벤트 연결
    private void SubscribeToEvents()
    {
        foreach (var cake in cakes)
        {
            cake.OnQuantityChanged += quantity => Debug.Log($"[CakeManager] {cake.name} Quantity Changed: {quantity}");
            cake.OnSalesChanged += sales => Debug.Log($"[CakeManager] {cake.name} Sales Changed: {sales}");
        }
    }

    // 케이크 데이터 변경 예시
    public void UpdateCakeData(int cakeIndex, int quantityChange, int salesChange)
    {
        if(cakeIndex < 0 || cakeIndex >= cakes.Count){
            Debug.Log("Invalid Cake Update Request : " + cakeIndex);
            return;
        }
        var cake = cakes[cakeIndex];
        if (cake != null)
        {
            cake.SetQuantity(cake.quantity + quantityChange);
            cake.SetSales(cake.sales + salesChange);
        }
    }
}
