using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOBaseImage", menuName = "Scriptable Objects/SOBaseImage")]
public class SOBaseImage : ScriptableObject
{
  public List<IngGroupImagePair> BaseImageList;
  public Dictionary<IngredientGroup, Sprite> BaseImageDict;

  void OnValidate()
  {
    BaseImageDict = new();
    foreach (var baseImage in BaseImageList){
      if (baseImage.ingredientGroup == null) continue;
      BaseImageDict.Add(baseImage.ingredientGroup, baseImage.sprite);
    }

    Debug.Log("Base-Image Link Complete");
  }
}

[Serializable]
public class IngGroupImagePair
{
  public IngredientGroup ingredientGroup;
  public Sprite sprite;
}