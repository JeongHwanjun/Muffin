using System;
using UnityEngine;

public class RecipeEventManager : MonoBehaviour
{
    public event Action<Ingredient> OnIngredientAdd;
    public event Action OnSaveCake;
    public static event Action<Ingredient> OnIngredientDropped;


    public void TriggerIngredientAdd(Ingredient newIngredient)
    {
        OnIngredientAdd?.Invoke(newIngredient);
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
