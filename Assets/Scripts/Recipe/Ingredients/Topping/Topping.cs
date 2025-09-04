using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable Objects/Ingredient/Topping")]
public class Topping : Ingredient
{
  public override IngredientType GetIngredientType()
  {
    return IngredientType.Topping;
  }
}