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
        recipeEventManager.OnIngredientSub += PopIngredient;
    }

    public List<recipeArrow> GetFinalRecipe()
    {
        List<recipeArrow> newRecipe = new List<recipeArrow>();
        foreach (Ingredient item in ingredients)
        {
            foreach (recipeArrow arrow in item.recipeArrows)
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
        //Debug.Log("CakeBuilder : 재료 추가" + newIngredient.ingredientData.ingredientName);
    }

    public void PopIngredient()
    {
        if (ingredients.Count <= 0) return;
        ingredients.RemoveAt(ingredients.Count - 1);
    }
    
    public CakeData BuildCake()
    {
        // 현재는 임시버전. 최종적으론 시너지와 배수를 모두 고려해 빌드해야 함.
        CakeData newCake = new CakeData();
        /*{
            cakeID = UnityEngine.Random.Range(0, 5000).ToString(),
            cakeName = GetCakeName(),
            finalTaste = GetTotalTaste(),
            finalFlavor = GetTotalFlavor(),
            finalTexture = GetTotalTexture(),
            finalAppearance = GetTotalAppearance(),
            finalCost = GetTotalCost(),
            recipe = GetFinalRecipe()
        };*/
        return newCake;
    }
}
