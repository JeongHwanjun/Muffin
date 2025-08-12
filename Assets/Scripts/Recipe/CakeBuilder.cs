using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CakeBuilder : MonoBehaviour
{
    public List<Ingredient> ingredients;
    public RecipeEventManager recipeEventManager;
    public TMP_InputField cakeName;

    void Start()
    {
        recipeEventManager.OnIngredientAdd += AddIngredient;
    }

    /* cake value 획득 함수들 - 다른 스크립트에서도 사용함. */
    public int GetTotalTaste()
    {
        return ingredients.Sum(i => i.ingredientData.taste);
    }
    public int GetTotalFlavor()
    {
        return ingredients.Sum(i => i.ingredientData.flavor);
    }

    public int GetTotalTexture()
    {
        return ingredients.Sum(i => i.ingredientData.texture);
    }

    public int GetTotalAppearance()
    {
        return ingredients.Sum(i => i.ingredientData.appearance);
    }

    public int GetTotalCost()
    {
        return ingredients.Sum(i => i.ingredientData.cost);
    }

    public string GetCakeName()
    {
        if (cakeName.text == null) return "임시이름";
        return cakeName.text;
    }

    public List<recipeArrow> GetFinalRecipe()
    {
        List<recipeArrow> newRecipe = new List<recipeArrow>();
        foreach (Ingredient item in ingredients)
        {
            foreach (recipeArrow arrow in item.ingredientData.recipeArrows)
            {
                newRecipe.Add(arrow);
            }
        }

        return newRecipe;
    }

    public void AddIngredient(Ingredient newIngredient)
    {
        ingredients.Add(newIngredient);
        // 재료 추가 이벤트 발생
        Debug.Log("CakeBuilder : 재료 추가" + newIngredient.ingredientData.ingredientName);
    }

    public CakeData BuildCake()
    {
        // 현재는 임시버전. 최종적으론 시너지와 배수를 모두 고려해 빌드해야 함.
        CakeData newCake = new CakeData
        {
            cakeID = UnityEngine.Random.Range(0, 5000).ToString(),
            cakeName = GetCakeName(),
            finalTaste = GetTotalTaste(),
            finalFlavor = GetTotalFlavor(),
            finalTexture = GetTotalTexture(),
            finalAppearance = GetTotalAppearance(),
            finalCost = GetTotalCost(),
            recipe = GetFinalRecipe()
        };
        return newCake;
    }
}
