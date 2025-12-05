using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "ComboRule", menuName = "Scriptable Objects/ComboRule")]
public class ComboRule : ScriptableObject
{
    [Header("트리거 조건")] public bool orderInsensitive = true; // 재료 순서 무시
    [Tooltip("모두 포함되어야 발동하는 재료들(AND)")] public List<IngredientGroup> requireAllGroups = new();
    [Tooltip("조건 만족시 스탯 변화량(음수 가능)")] public Ingredient delta; // 콤보에 의한 스탯 변화량
}
