using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable Objects/Ingredient/Base")]
public class Base : Ingredient
{
  public override IngredientType GetIngredientType()
  {
    return IngredientType.Base;
  }
}