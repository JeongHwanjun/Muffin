using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientGroup", menuName = "Scriptable Objects/Ingredient/IngredientGroup")]
[Serializable]
public class IngredientGroup : ScriptableObject
{
  public string GroupName;
  public List<Ingredient> tiers;
}
