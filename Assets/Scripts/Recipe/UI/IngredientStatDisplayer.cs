using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientStatDisplayer : MonoBehaviour
{
    public TextMeshProUGUI[] textValues;
    public GameObject UI;
    public RecipeEventManager recipeEventManager;

    void Start()
    {
        recipeEventManager.OnIngredientHover += RefreshUI;
        RecipeEventManager.OnIngredientDropped += ClearUI;
    }

    void RefreshUI(Ingredient ingredient)
    {
        UI.SetActive(true);
        int index = 0;
        foreach (var text in textValues)
        {
            text.text = ingredient.modifiers[index++].delta.ToString();
        }
    }

    void ClearUI(Ingredient ingredient)
    {
        UI.SetActive(false);
    }
}
