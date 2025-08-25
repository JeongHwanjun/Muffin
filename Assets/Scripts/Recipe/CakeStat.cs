// 스탯 저장용 클래스
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CakeStat
{
    [SerializeField] public List<StatModifier> modifiers = new();

    // init
    public CakeStat() { }
    public CakeStat(IngredientBase stat) // IngredientBase를 통해 CakeStat을 초기화함
    {
        SetStats(stat);
    }

    void SetStats(IngredientBase ingredientBase)
    {
        modifiers = ingredientBase.modifiers;
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

// 배율 저장용 클래스
public class StatMultipliers
{
    public List<StatModifier> modifiers;
    public StatMultipliers() { }
    public StatMultipliers(IngredientBase newIngredient) // 재료의 수치로 multiplier를 생성해도 배율로 해석함
    {
        modifiers = newIngredient.modifiers;
    }

    public static StatMultipliers operator +(StatMultipliers a, StatMultipliers b)
    {
        List<StatModifier> newModifiers = new();
        for (int i = 0; i < a.modifiers.Count; i++)
        {
            StatModifier sm = a.modifiers[i] + b.modifiers[i];
            newModifiers.Add(sm);
        }

        return new StatMultipliers
        {
            modifiers = newModifiers
        };
    }
}