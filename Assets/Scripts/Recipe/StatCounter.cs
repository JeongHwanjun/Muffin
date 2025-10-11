using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatCounter : MonoBehaviour
{
    public RecipeEventManager recipeEventManager;
    public CakeStat pureStat = null;
    public CakeStat multipliedStat = null;
    public CakeStat comboStat = null;
    public CakeStat finalStat = null;
    public StatMultipliers multiplier = null;
    public List<Ingredient> ingredients = new(); // 추가된 재료 리스트
    public List<recipeArrow> recipeArrows = new();
    public ComboResolver comboResolver = null;
    private PlayerData playerData = PlayerData.Instance;

    private bool isComboCalculated = false;
    private int recipeLenFlour = 0, recipeLenBase = 0, recipeLenTopping = 0;
    private int maxLenFlour, maxLenBase, maxLenTopping;

    void Start()
    {
        pureStat = new CakeStat();
        multipliedStat = new CakeStat();
        comboStat = new CakeStat();
        multiplier = new StatMultipliers();


        recipeEventManager.OnIngredientAdd += OnIngredientAdd;
        recipeEventManager.OnIngredientSub += OnIngredientSub;
        maxLenFlour = playerData.recipeLenFlour;
        maxLenBase = playerData.recipeLenBase;
        maxLenTopping = playerData.recipeLenTopping;
    }

    public void OnIngredientAdd(Ingredient newIngredient)
    {
        IngredientType newIngredientType = newIngredient.GetIngredientType(); // 재료 타입 획득
        bool isExceeded = false;

        if (newIngredientType == IngredientType.Flour) // Flour 단계면 배율 변경
        {
            recipeLenFlour += newIngredient.recipeArrows.Count; // 화살표 길이 추가
            StatMultipliers multipliers = new(newIngredient); // 배율 형태로 재가공
            AddMultiplier(multipliers);

            if (recipeLenFlour > maxLenFlour)
            {
                Debug.LogFormat("Flour Recipe Length exceeded : {0}", recipeLenFlour);
                isExceeded = true;
            }
        }
        else if (newIngredientType == IngredientType.Base) // Base 단계면 스탯 변경
		{
            recipeLenBase += newIngredient.recipeArrows.Count;
            CakeStat stat = new(newIngredient); // 스탯 형태로 재가공
            AddPureStat(stat);
            SetMultipliedStat();
            SetFinalStat();

            if (recipeLenBase > maxLenBase)
            {
                Debug.LogFormat("Base Recipe Length exceeded : {0}", recipeLenBase);
                isExceeded = true;
            }
        }
        else if(newIngredientType == IngredientType.Topping) // Topping 단계면 스탯 변경
        {
        
            recipeLenTopping += newIngredient.recipeArrows.Count;
            CakeStat stat = new(newIngredient); // 스탯 형태로 재가공
            AddPureStat(stat);
            SetMultipliedStat();
            SetFinalStat();
            
            if (recipeLenTopping > maxLenTopping)
            {
                Debug.LogFormat("Topping Recipe Length exceeded : {0}", recipeLenTopping);
                isExceeded = true;
            }
            
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

        if (isExceeded) recipeEventManager.TriggerIngredientSub(); // 제한된 레시피 길이 초과시 바로 재료 삭제
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
            recipeLenFlour -= lastIngredient.recipeArrows.Count;
        }
        else if (lastIngredientType == IngredientType.Base)
        {
            // base 대처
            CakeStat stat = new(lastIngredient);
            SubPureStat(stat);
            SetMultipliedStat();
            SetFinalStat();
            recipeLenBase -= lastIngredient.recipeArrows.Count;
        }
        else if (lastIngredientType == IngredientType.Topping)
        {
            // topping 제거
            CakeStat stat = new(lastIngredient);
            SubPureStat(stat);
            SetMultipliedStat();
            SetFinalStat();
            recipeLenTopping -= lastIngredient.recipeArrows.Count;
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

    public List<recipeArrow> GetRecipeArrows(){return recipeArrows;}

    public void AddPureStat(CakeStat diff){pureStat = pureStat + diff;}
    public void SubPureStat(CakeStat diff){pureStat = pureStat - diff;}

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

    public StatMultipliers GetMultipliers(){return multiplier;}

    private void SetMultipliedStat(){multipliedStat = pureStat * multiplier;}

    public CakeStat GetMultipliedStat(){return multipliedStat;}

    private void SetComboStat() // 콤보 적용
    {
        if (!comboResolver)
        {
            Debug.LogWarningFormat("ComboResolver : {0}", comboResolver);
            return;
        }

        Debug.Log("Applying Combo");
        comboStat = CakeStat.CloneCakeStat(multipliedStat);
        foreach (var comboRule in comboResolver.GetMatches(ingredients))
        {
            comboStat += comboRule.delta;
            Debug.LogFormat("Combo Applied : {0}", comboRule.delta.displayName);
        }

        // 콤보 산출했음을 기록
        isComboCalculated = true;
    }
    public CakeStat GetComboStat() {return comboStat;}

    public void PressComboButton() // 임시
    {
        SetComboStat();
        SetFinalStat();
        recipeEventManager.TriggerRefreshUI();
    }

    public void SetFinalStat()
    {
        if (isComboCalculated) finalStat = GetComboStat();
        else finalStat = GetMultipliedStat();
    }
    public CakeStat GetFinalStat()
    {
        // 최종 스탯 반환
        return finalStat;
    }
}
