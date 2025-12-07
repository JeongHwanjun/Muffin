using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private Ingredient defaultMultiplier;
    private Stack<ComboRule> comboRules = new();

    private bool isComboCalculated = false;
    private int recipeLenFlour = 0, recipeLenBase = 0, recipeLenTopping = 0;
    private int maxLenFlour, maxLenBase, maxLenTopping;

    void Start()
    {
        pureStat = new CakeStat();
        multipliedStat = new CakeStat();
        comboStat = new CakeStat();
        multiplier = new StatMultipliers(defaultMultiplier);


        recipeEventManager.OnIngredientAdd += OnIngredientAdd;
        recipeEventManager.OnIngredientSub += OnIngredientSub;
        recipeEventManager.OnClickNextButton += OnClickNextButton;
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

        if (isExceeded) recipeEventManager.TriggerIngredientSub(IngredientType.None); // 제한된 레시피 길이 초과시 바로 재료 삭제
    }

    public void OnIngredientSub(IngredientType Stage)
    {
        if (ingredients.Count <= 0) return;

        Ingredient lastIngredient = ingredients.Last(); // 마지막 재료 - 스탯에서 제외하기 위함
        IngredientType lastIngredientType = lastIngredient.GetIngredientType();

        // 1. Stage랑 lastIngredientType이 같을 때 = 그냥 삭제하면 됨 = 아무 처리도 하지 않음
        // 2. Stage랑 lastIngredientType이 다른데 Stage가 None은 아닐 때(=유효한 재료이지만 이전 스테이지일 때) = 이전 스테이지로 넘어가야 함
        if (Stage != IngredientType.None && Stage != lastIngredientType)
        {
            // Return to prev Stage
            recipeEventManager.TriggerMoveToPrevStage();
            return; // 이전 Stage로 돌아갈 때 재료는 빼지 않음. 실력부족 + 가시성.
        }
        // 3. Stage랑 lastIngredientType이 다른데 Stage가 None일 때(=제한된 길이로 인해 삭제할 때) = 그냥 삭제하면 됨. = 아무 처리도 하지 않음

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
        // 이미지 삭제
        recipeEventManager.TriggerRemoveLastIngredient();
        // 재료 목록에서 재료 삭제
        ingredients.RemoveAt(ingredients.Count - 1);
        recipeEventManager.TriggerRefreshUI(); // UI 갱신
    }

    void OnClickNextButton(IngredientType Stage)
    {
        // 현재 스테이지에서 재료가 추가되었는지 확인
        if (IsSameTypeWithLastIngredient(Stage)) recipeEventManager.TriggerMoveToNextStage(); // 그렇다면 다은 스테이지로 진행
        else return; // 그렇지 않다면 넘어가지 않음.
    }
    
    bool IsSameTypeWithLastIngredient(IngredientType ingredientType)
    {
        if (ingredients.Count <= 0) return false;
        IngredientType lastType = ingredients.Last().GetIngredientType();
        return ingredientType == lastType;
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

    private void GetComboRules() // 콤보 산출
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
            //comboStat += comboRule.delta; //<- 이것도 콤보 연출시에 해도 될듯?
            Debug.LogFormat("Combo Calculated : {0}", comboRule.delta.displayName);

            // 콤보 룰을 스택에 넣음
            comboRules.Push(comboRule);
            // 이후 콤보 적용시 이 스택을 전달해 콤보 연출 출력
        }

        // 콤보 산출했음을 기록
        isComboCalculated = true;

        // 콤보 산출 완료시 연출 출력
        recipeEventManager.TriggerPrintComboScript(comboRules);
    }
    public CakeStat GetComboStat() {return comboStat;}

    public void PressComboButton() // 임시
    {
        GetComboRules();
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
