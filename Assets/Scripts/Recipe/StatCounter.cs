using UnityEngine;

public class StatCounter : MonoBehaviour
{
    public RecipeEventManager recipeEventManager;
    public CakeStat pureStat = null;
    public CakeStat multipliedStat = null;
    public StatMultipliers multiplier = null;
    public CakeStat finalStat = null;

    void Awake()
    {
        pureStat = new CakeStat();
        multipliedStat = new CakeStat();
        multiplier = new StatMultipliers();
        finalStat = new CakeStat();

        recipeEventManager.OnIngredientAdd += OnIngredientAdd;
    }

    public void OnIngredientAdd(Ingredient newIngredient)
    {
        CakeStat diff = new CakeStat(newIngredient.ingredientData);
        AddPureStat(diff);

        recipeEventManager.TriggerRefreshUI(); // UI 갱신
    }
    public void AddPureStat(CakeStat diff)
    {
        // 순수스탯(재료의 총합)
        pureStat = pureStat + diff;
    }

    public void AddMultiplier(StatMultipliers diff)
    {
        // 배율 변화
        multiplier = multiplier + diff;
    }

    public CakeStat GetMultipliedStat()
    {
        multipliedStat = pureStat * multiplier;
        // 배율 스탯 반환
        return multipliedStat;
    }

    public void ApplyCombo()
    {
        CakeStat comboStat = new CakeStat // 임의의 콤보 스탯
        {
            taste = 2,
            flavor = 12,
            texture = 0,
            appearance = 0,
            cost = 0
        };
        // 콤보 적용 => 최종 스탯 반영
        finalStat = multipliedStat + comboStat;
        // 아무튼 콤보 반영 됐다고 침 - 추후 구현
    }

    public CakeStat GetFinalStat()
    {
        // 최종 스탯 반환
        return finalStat;
    }
}
