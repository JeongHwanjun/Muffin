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
    public List<Ingredient> ingredients = new();
    public List<recipeArrow> recipeArrows = new();
    public ComboResolver comboResolver = null;

    void Start()
    {
        pureStat = new CakeStat();
        multipliedStat = new CakeStat();
        comboStat = new CakeStat();
        multiplier = new StatMultipliers();


        recipeEventManager.OnIngredientAdd += OnIngredientAdd;
        recipeEventManager.OnIngredientSub += OnIngredientSub;
    }

    public void OnIngredientAdd(Ingredient newIngredient)
    {
        IngredientType newIngredientType = newIngredient.GetIngredientType(); // 재료 타입 획득

        if (newIngredientType == IngredientType.Flour) // Flour 단계면 배율 변경
        {
            StatMultipliers multipliers = new(newIngredient); // 배율 형태로 재가공
            AddMultiplier(multipliers);
        }
        else if (newIngredientType == IngredientType.Base || newIngredientType == IngredientType.Topping) // Base나 Topping 단계면 스탯 변경
        {
            CakeStat stat = new(newIngredient); // 스탯 형태로 재가공
            AddPureStat(stat);
        }
        else
        {
            Debug.LogErrorFormat("StatCounter : unknown type {0}", newIngredientType);
            return;
        }

        GetMultipliedStat(); // 배율을 반영한 배율 스탯 계산
        ingredients.Add(newIngredient); // 재료 목록에 추가(콤보 카운팅)
        recipeArrows.AddRange(newIngredient.recipeArrows); // 전체 화살표에 재료 화살표를 추가함.
        recipeEventManager.TriggerRefreshUI(); // UI 갱신
    }

    public void OnIngredientSub()
    {
        if (ingredients.Count <= 0) return;

        Ingredient lastIngredient = ingredients[ingredients.Count - 1]; // 마지막 재료 - 스탯에서 제외하기 위함
        IngredientType lastIngredientType = lastIngredient.GetIngredientType();
        // 재료 타입에 따라 다른 처리
        if (lastIngredientType == IngredientType.Flour)
        {
            // flour 대처
            StatMultipliers multiplier = new(lastIngredient);
            SubMultiplier(multiplier);
        }
        else if (lastIngredientType == IngredientType.Base)
        {
            // base 대처
            CakeStat stat = new(lastIngredient);
            SubPureStat(stat);
        }
        else if (lastIngredientType == IngredientType.Topping)
        {
            // topping 대처
            CakeStat stat = new(lastIngredient);
            SubPureStat(stat);
        }
        else
        {
            Debug.LogErrorFormat("Invalid Ingredient Sub : {0}", lastIngredient);
        }

        // 화살표 삭제
        recipeArrows.RemoveRange(ingredients.Count - 1, lastIngredient.recipeArrows.Count); 
        // 재료 목록에서 재료 삭제
        ingredients.RemoveAt(ingredients.Count - 1);
        recipeEventManager.TriggerRefreshUI(); // UI 갱신
    }

    public List<recipeArrow> GetRecipeArrows()
    {
        return recipeArrows;
    }

    public void AddPureStat(CakeStat diff)
    {
        // 순수스탯(재료의 총합)
        pureStat = pureStat + diff;

        finalStat = pureStat;
    }
    public void SubPureStat(CakeStat diff)
    {
        pureStat = pureStat - diff;

        finalStat = pureStat;
    }

    public void AddMultiplier(StatMultipliers diff)
    {
        // 배율 변화
        multiplier = multiplier + diff;

        recipeEventManager.TriggerRefreshUI();
    }
    public void SubMultiplier(StatMultipliers diff)
    {
        multiplier = multiplier - diff;

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
