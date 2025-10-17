using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerDataSerializable
{
	public int cakeCounter = 0; // 지금까지 만든 케이크의 번호
	public int cakeNumLimit = 10; // 케이크 최대 개수

	public int recipeLenFlour = 3, recipeLenBase = 3, recipeLenTopping = 5; // 각 단계의 화살표 한도를 정하는 변수

  public Dictionary<string, int> ingredientTierDict = null;

  public PlayerDataSerializable() { }

  public PlayerDataSerializable(PlayerData runtimeData)
  {
    cakeCounter = runtimeData.cakeCounter;
    cakeNumLimit = runtimeData.cakeNumLimit;
    recipeLenFlour = runtimeData.recipeLenFlour;
    recipeLenBase = runtimeData.recipeLenBase;
    recipeLenTopping = runtimeData.recipeLenTopping;

    ingredientTierDict = new();
    foreach (var kvp in runtimeData.ingredientTierDict)
    {
      if (kvp.Key != null)
        ingredientTierDict[kvp.Key.guid] = kvp.Value;
    }
  }
  
  public void ApplyTo(PlayerData runtimeData, List<IngredientGroup> allGroups) // 가지고있는 데이터를 PlayerData로 전환
  {
    runtimeData.cakeCounter = cakeCounter;
    runtimeData.cakeNumLimit = cakeNumLimit;
    runtimeData.recipeLenFlour = recipeLenFlour;
    runtimeData.recipeLenBase = recipeLenBase;
    runtimeData.recipeLenTopping = recipeLenTopping;

    runtimeData.ingredientTierDict = new();

    foreach (var kvp in ingredientTierDict)
    {
      IngredientGroup group = allGroups.Find(g => g.guid == kvp.Key);
      if (group != null)
        runtimeData.ingredientTierDict[group] = kvp.Value;
      else
        Debug.LogWarning($"[PlayerData] Missing IngredientGroup for GUID {kvp.Key}");
    }
  }
}
