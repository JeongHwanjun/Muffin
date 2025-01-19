using System.Collections.Generic;
using UnityEngine;

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
            recipe = new List<int> { 0, 1, 2, 3, 0 },
            price = 15
        });

        cakes.Add(new Cake
        {
            name = "Strawberry Cake",
            quantity = 5,
            sales = 0,
            recipe = new List<int> { 3, 3, 3, 1 },
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
    public void UpdateCakeData(string cakeName, int quantityChange, int salesChange)
    {
        var cake = cakes.Find(c => c.name == cakeName);
        if (cake != null)
        {
            cake.SetQuantity(cake.quantity + quantityChange);
            cake.SetSales(cake.sales + salesChange);
        }
    }
}
