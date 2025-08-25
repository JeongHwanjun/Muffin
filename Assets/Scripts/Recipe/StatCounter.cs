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

        recipeEventManager.OnFlourAdd += AddMultiplier;
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

        recipeEventManager.TriggerRefreshUI();
    }

    public StatMultipliers GetMultipliers()
    {
        return multiplier;
    }

    public CakeStat GetMultipliedStat()
    {
        multipliedStat = pureStat * multiplier;
        // 배율 스탯 반환
        return multipliedStat;
    }

    public void ApplyCombo()
    {

    }

    public CakeStat GetFinalStat()
    {
        // 최종 스탯 반환
        return finalStat;
    }
}
