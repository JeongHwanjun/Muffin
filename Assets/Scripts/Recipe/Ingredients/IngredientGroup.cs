using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "IngredientGroup", menuName = "Scriptable Objects/Ingredient/IngredientGroup")]
[Serializable]
public class IngredientGroup : ScriptableObject
{
  [HideInInspector] public string guid;
  public string GroupName;
  public List<Ingredient> tiers;

#if UNITY_EDITOR
  private void OnValidate()
  {
    // 에디터에서만 실행
    if (string.IsNullOrEmpty(guid))
    {
      guid = GUID.Generate().ToString();
      EditorUtility.SetDirty(this);
      AssetDatabase.SaveAssets();
    }
  }
#endif
}
