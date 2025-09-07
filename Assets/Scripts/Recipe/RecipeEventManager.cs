using System;
using UnityEngine;

public class RecipeEventManager : MonoBehaviour
{
    public event Action OnRefreshUI;
    public event Action<Ingredient> OnIngredientAdd;
    public event Action<Ingredient> OnIngredientClick;
    public event Action<Ingredient> OnIngredientHover;
    public event Action OnSaveCake;
    public static event Action<Ingredient> OnIngredientDropped;

    public void TriggerIngredientAdd(Ingredient newIngredient)
    {
        Debug.LogFormat("Event : OnIngredientAdd {0}", newIngredient);
        OnIngredientAdd?.Invoke(newIngredient);
    }

    public void TriggerRefreshUI()
    {
        Debug.LogFormat("Event : OnRefreshUI");
        OnRefreshUI?.Invoke();
    }

    public void TriggerOnSaveCake()
    {
        Debug.LogFormat("Event : OnSaveCake");
        OnSaveCake?.Invoke();
    }

    public static void TriggerIngredientDropped(Ingredient droppedIngredient)
    {
        Debug.LogFormat("Event : OnIngredientDropped {0}", droppedIngredient);
        OnIngredientDropped?.Invoke(droppedIngredient);
    }

    public void TriggerIngredientClick(Ingredient clickedIngredient)
    {
        Debug.LogFormat("Event : OnIngredientClick");
        OnIngredientClick?.Invoke(clickedIngredient);
    }

    public void TriggerIngredientHover(Ingredient hoverIngredient)
    {
        Debug.LogFormat("Event : OnIngredientHover");
        OnIngredientHover?.Invoke(hoverIngredient);
    }
}
