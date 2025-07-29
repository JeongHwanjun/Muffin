using System;
using UnityEngine;

public class RecipeEventManager : MonoBehaviour
{
    public event Action<Ingredient> OnIngredientAdd;
    public event Action OnSaveCake;


    public void TriggerIngredientAdd(Ingredient newIngredient)
    {
        OnIngredientAdd?.Invoke(newIngredient);
    }

    public void TriggerOnSaveCake()
    {
        OnSaveCake?.Invoke();
    }
}
