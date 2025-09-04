using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable Objects/Ingredient/Flour")]
public class Flour : Ingredient
{
    public override IngredientType GetIngredientType()
    {
        return IngredientType.Flour;
    }
}