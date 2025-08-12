using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CakeData
{
  public string cakeName;
  public string cakeID;
  public int finalTaste, finalFlavor,finalTexture, finalAppearance, finalCost;
  public List<recipeArrow> recipe;
}
