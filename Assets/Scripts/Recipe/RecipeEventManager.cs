using System;
using UnityEngine;

public class RecipeEventManager : MonoBehaviour
{
    public event Action<StatMultipliers> OnFlourAdd;
    public event Action OnBaseAdd;
    public event Action OnRefreshUI;
    public event Action<Ingredient> OnIngredientAdd;
    public event Action OnSaveCake;
    public static event Action<Ingredient> OnIngredientDropped;

    public void TriggerFlourAdd(StatMultipliers newFlour)
    {
        Debug.Log("Event : OnFlourAdd");
        OnFlourAdd?.Invoke(newFlour);
    }

    public void TriggerOnBaseAdd()
    {
        Debug.Log("Event : OnFlourAdd");
        OnBaseAdd?.Invoke();
    }

    public void TriggerIngredientAdd(Ingredient newIngredient)
    {
        Debug.LogFormat("Event : OnFlourAdd {0}", newIngredient);
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
}
