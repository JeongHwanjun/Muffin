using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public List<Ingredient> ingredients = new List<Ingredient>(); // 재료 목록

    private void Awake()
    {
        InitializeIngredients();
        SubscribeToEvents();
    }

    // 재료 초기화
    private void InitializeIngredients()
    {
        ingredients.Add(new Ingredient
        {
            name = "Flour",
            usage = 50,
            price = 1
        });

        ingredients.Add(new Ingredient
        {
            name = "Sugar",
            usage = 20,
            price = 2
        });
    }

    // 재료 데이터 변경 이벤트 연결
    private void SubscribeToEvents()
    {
        foreach (var ingredient in ingredients)
        {
            ingredient.OnUsageChanged += usage => Debug.Log($"[IngredientManager] {ingredient.name} Usage Changed: {usage}");
        }
    }

    // 재료 데이터 변경
    public void UpdateIngredientData(string ingredientName, int additionalUsage)
    {
        var ingredient = ingredients.Find(i => i.name == ingredientName);
        if (ingredient != null)
        {
            Debug.Log($"사용량 변경 : {ingredientName}, {additionalUsage}");
            ingredient.SetUsage(ingredient.usage + additionalUsage);
        }
    }
}
