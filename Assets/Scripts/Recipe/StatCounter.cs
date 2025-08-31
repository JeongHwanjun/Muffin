using System.Collections.Generic;
using UnityEngine;

public class StatCounter : MonoBehaviour
{
    public RecipeEventManager recipeEventManager;
    public CakeStat pureStat = null;
    public CakeStat multipliedStat = null;
    public CakeStat comboStat = null;
    public CakeStat finalStat = null;
    public StatMultipliers multiplier = null;
    public List<IngredientBase> ingredients = new();
    public ComboResolver comboResolver = null;

    void Start()
    {
        pureStat = new CakeStat();
        multipliedStat = new CakeStat();
        comboStat = new CakeStat();
        multiplier = new StatMultipliers();
        

        recipeEventManager.OnFlourAdd += AddMultiplier;
        recipeEventManager.OnIngredientAdd += OnIngredientAdd;
    }

    public void OnIngredientAdd(Ingredient newIngredient)
    {
        CakeStat diff = new CakeStat(newIngredient.ingredientData);
        AddPureStat(diff); // 재료에 의한 순수 스탯 계산
        GetMultipliedStat(); // 배율을 반영한 배율 스탯 계산

        ingredients.Add(newIngredient.ingredientData); // 재료 목록에 추가(콤보 카운팅)

        recipeEventManager.TriggerRefreshUI(); // UI 갱신
    }
    public void AddPureStat(CakeStat diff)
    {
        // 순수스탯(재료의 총합)
        pureStat = pureStat + diff;

        finalStat = pureStat;
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

        finalStat = multipliedStat;
        // 배율 스탯 반환
        return multipliedStat;
    }

    public CakeStat GetComboStat() // 콤보 적용
    {
        if (!comboResolver)
        {
            Debug.LogWarningFormat("ComboResolver : {0}", comboResolver);
        }

        comboStat = CakeStat.CloneCakeStat(multipliedStat);
        foreach (var comboRule in comboResolver.GetMatches(ingredients))
        {
            comboStat += comboRule.delta;
            Debug.LogFormat("Combo Applied : {0}", comboRule.delta.displayName);
        }

        finalStat = comboStat;

        return comboStat;
    }

    public void PressComboButton() // 임시
    {
        finalStat = GetComboStat();
        recipeEventManager.TriggerRefreshUI();
    }

    public CakeStat GetFinalStat()
    {
        // 최종 스탯 반환
        return finalStat;
    }
}
