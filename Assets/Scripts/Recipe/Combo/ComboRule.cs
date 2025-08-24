using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "ComboRule", menuName = "Scriptable Objects/ComboRule")]
public class ComboRule : ScriptableObject
{
    [Header("트리거 조건")] public bool orderInsensitive = true; // 재료 순서 무시
    [Tooltip("모두 포함되어야 발동하는 재료들(AND)")] public List<Ingredient> requireAllIngredients = new();
    [Tooltip("하나 이상 포함되면 발동하는 재료들(OR)")] public List<Ingredient> requireAnyIngredients = new();
    //[Tooltip("최소 개수 조건: 특정 스탯이 n 이상시 발동")] public List<TraitCountCond> requireTraitCounts = new();
    //[Tooltip("포함되면 발동 안 됨")] public List<Ingredient> excludeIngredients = new();

    
}
