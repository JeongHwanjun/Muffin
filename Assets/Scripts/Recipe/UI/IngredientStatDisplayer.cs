using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientStatDisplayer : MonoBehaviour
{
    public TextMeshProUGUI[] textValues;
    public TextMeshProUGUI ingredientName;
    public Arrows arrows;
    public GameObject UI;
    public RecipeEventManager recipeEventManager;

    void Start()
    {
        recipeEventManager = RecipeEventManager.Instance;
        recipeEventManager.OnIngredientHover += RefreshUI;
        RecipeEventManager.OnIngredientDropped += ClearUI;
        recipeEventManager.OnIngredientHoverExit += ClearUI;

        ClearUI();
    }

    void RefreshUI(Ingredient ingredient)
    {
        UI.SetActive(true);
        int index = 0;
        foreach (var text in textValues)
        {
            text.text = ingredient.modifiers[index++].delta.ToString();
        }
        ingredientName.text = ingredient.displayName;
        arrows.RefreshArrows(ingredient.recipeArrows);
    }

    void ClearUI(Ingredient ingredient)
    {
        UI.SetActive(false);
    }

    void ClearUI()
    {
        UI.SetActive(false);
    }
}
