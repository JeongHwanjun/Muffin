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

    public int GetTotalFlavor()
    {
        return ingredients.Sum(i => i.ingredientData.flavor);
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
        Debug.Log("CakeBuilder : 재료 추가" + newIngredient.ingredientData.ingredientName);
    }

    public CakeData BuildCake()
    {
        CakeData newCake = new CakeData
        {
            cakeID = UnityEngine.Random.Range(0, 5000).ToString(),
            cakeName = GetCakeName(),
            totalFlavor = GetTotalFlavor(),
            totalAppearance = GetTotalAppearance(),
            totalCost = GetTotalCost(),
            recipe = GetFinalRecipe()
        };
        return newCake;
    }
}
