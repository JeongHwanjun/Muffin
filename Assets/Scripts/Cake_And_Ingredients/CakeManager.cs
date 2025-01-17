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
    private void InitializeCakes()
    {
        // 임시 데이터 넣기
        cakes.Add(new Cake
        {
            name = "Chocolate Cake",
            quantity = 10,
            sales = 0,
            recipe = new List<int> { 0, 1, 2, 3, 0 }, // 재료 ID 목록 -> 구체적인 커맨드로 번역 이 방식도 고려해보기
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
    public void UpdateCakeData(string cakeName, int newQuantity, int newSales)
    {
        var cake = cakes.Find(c => c.name == cakeName);
        if (cake != null)
        {
            cake.SetQuantity(newQuantity);
            cake.SetSales(newSales);
        }
    }
}
