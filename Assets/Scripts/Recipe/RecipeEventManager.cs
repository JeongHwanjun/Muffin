using System;
using UnityEngine;

public class RecipeEventManager : MonoBehaviour
{
    public event Action OnFlourAdd;
    public event Action OnBaseAdd;
    public event Action OnRefreshUI;
    public event Action<Ingredient> OnIngredientAdd;
    public event Action OnSaveCake;
    public static event Action<Ingredient> OnIngredientDropped;

    public void TriggerFlourAdd()
    {
        OnFlourAdd?.Invoke();
    }

    public void TriggerOnBaseAdd()
    {
        OnBaseAdd?.Invoke();
    }
    public void TriggerIngredientAdd(Ingredient newIngredient)
    {
        OnIngredientAdd?.Invoke(newIngredient);
    }
    public void TriggerRefreshUI()
    {
        OnRefreshUI?.Invoke();
    }

    public void TriggerOnSaveCake()
    {
        OnSaveCake?.Invoke();
    }

    public static void TriggerIngredientDropped(Ingredient droppedIngredient)
    {
        OnIngredientDropped?.Invoke(droppedIngredient);
    }
}
