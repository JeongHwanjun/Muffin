using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientBase", menuName = "Scriptable Objects/IngredientBase")]
[System.Serializable]
public class IngredientBase : ScriptableObject
{
    public string id;
    public string ingredientName;
    public int cost;
    public int flavor;
    public int taste;
    public int texture;
    public int appearance;
    [Serializable] public struct StatModifier
    {
        public Stat stat;
        public int delta;
    }
    [SerializeField] public string displayName;
    [SerializeField] public List<StatModifier> modifiers = new();

    public List<recipeArrow> recipeArrows;
}
