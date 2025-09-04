// 스탯 저장용 클래스
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class CakeStat
{
    [SerializeField] public List<StatModifier> modifiers = new();
    private static Ingredient _fallbackIngredient = null;

    // init
    public CakeStat()
    {
        if (_fallbackIngredient != null)
        {
            SetStats(_fallbackIngredient);
        }
    }
    public CakeStat(Ingredient stat) // Ingredient를 통해 CakeStat을 초기화함
    {
        SetStats(stat);
    }

    void SetStats(Ingredient Ingredient)
    {
        modifiers = Ingredient.modifiers.ToList();
    }

    public static void SetFallback(Ingredient ingredient)
    {
        _fallbackIngredient = ingredient;
        Debug.LogFormat("SetFallback Complete : {0}", _fallbackIngredient);
    }

    public static CakeStat CloneCakeStat(CakeStat original)
    {
        CakeStat newCakeStat = new();
        newCakeStat.modifiers = original.modifiers.ToList();
        return newCakeStat;
    }

    // +
    public static CakeStat operator +(CakeStat a, CakeStat b)
    {
        List<StatModifier> newModifiers = new();
        for (int i = 0; i < a.modifiers.Count; i++)
        {
            StatModifier sm = a.modifiers[i] + b.modifiers[i];
            newModifiers.Add(sm);
        }
        return new CakeStat
        {
            modifiers = newModifiers
        };
    }

    public static CakeStat operator +(CakeStat a, Ingredient b)
    {
        List<StatModifier> newModifiers = new();
        for (int i = 0; i < a.modifiers.Count; i++)
        {
            StatModifier sm = a.modifiers[i] + b.modifiers[i];
            newModifiers.Add(sm);
        }
        return new CakeStat
        {
            modifiers = newModifiers
        };
    }

    // -
    public static CakeStat operator -(CakeStat a, CakeStat b)
    {
        List<StatModifier> newModifiers = new();
        for (int i = 0; i < a.modifiers.Count; i++)
        {
            StatModifier sm = a.modifiers[i] - b.modifiers[i];
            newModifiers.Add(sm);
        }
        return new CakeStat
        {
            modifiers = newModifiers
        };
    }

    // * - 배수 적용
    public static CakeStat operator *(CakeStat a, StatMultipliers m)
    {
        List<StatModifier> newModifiers = new();
        for (int i = 0; i < a.modifiers.Count; i++)
        {
            StatModifier sm = a.modifiers[i] * m.modifiers[i];
            newModifiers.Add(sm);
        }
        return new CakeStat
        {
            modifiers = newModifiers
        };
    }
}