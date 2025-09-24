using System.Collections.Generic;
using System.Linq;
// 배율 저장용 클래스
public class StatMultipliers
{
    public List<StatModifier> modifiers;
    private static Ingredient _fallbackIngredient;
    public StatMultipliers()
    {
        if (_fallbackIngredient != null && _fallbackIngredient.modifiers != null)
            modifiers = CloneModifiers(_fallbackIngredient.modifiers);
        else
            modifiers = new List<StatModifier>();
    }
    public StatMultipliers(Ingredient ingredient) // 재료의 수치로 multiplier를 생성해도 배율로 해석함
    {
        if (ingredient != null && ingredient.modifiers != null && ingredient.modifiers.Count > 0)
            modifiers = CloneModifiers(ingredient.modifiers);
        else if (_fallbackIngredient != null && _fallbackIngredient.modifiers != null)
            modifiers = CloneModifiers(_fallbackIngredient.modifiers);
        else
            modifiers = new List<StatModifier>(); // 마지막 방어
    }

    public static void SetFallback(Ingredient ingredient) // 기본 Ingredient를 세팅함 - Awake 시점에 실행됨.
    {
        _fallbackIngredient = ingredient;
    }

    private static List<StatModifier> CloneModifiers(List<StatModifier> src) // 모디파이어 복사
        => src.Select(m => new StatModifier { stat = m.stat, delta = m.delta }).ToList();

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

    public static StatMultipliers operator -(StatMultipliers a, StatMultipliers b)
    {
        List<StatModifier> newModifiers = new();
        for (int i = 0; i < a.modifiers.Count; i++)
        {
            StatModifier sm = a.modifiers[i] - b.modifiers[i];
            newModifiers.Add(sm);
        }

        return new StatMultipliers
        {
            modifiers = newModifiers
        };
    }
}