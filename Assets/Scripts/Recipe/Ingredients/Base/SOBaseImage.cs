using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SOBaseImage", menuName = "Scriptable Objects/SOBaseImage")]
public class SOBaseImage : ScriptableObject
{
  public List<IngGroupImagePair> BaseImageList;
  private Dictionary<IngredientGroup, Sprite> baseImageDict;

  public Dictionary<IngredientGroup, Sprite> BaseImageDict
    {
        get
        {
            if (baseImageDict == null)
            {
                baseImageDict = BaseImageList
                    .Where(p => p.ingredientGroup != null)
                    .ToDictionary(p => p.ingredientGroup, p => p.sprite);
            }
            return baseImageDict;
        }
    }
}

[Serializable]
public class IngGroupImagePair
{
  public IngredientGroup ingredientGroup;
  public Sprite sprite;
}