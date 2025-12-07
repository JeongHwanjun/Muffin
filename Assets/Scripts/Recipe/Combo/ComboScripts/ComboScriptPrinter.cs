using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

// 콤보 목록을 받아 지정된 템플릿에 맞게 출력하는 스크립트
public class ComboScriptPrinter : MonoBehaviour
{
    public TextMeshProUGUI text;
    [Tooltip("재료 2개 버전 템플릿")]
    public string comboTemplate_2;
    [Tooltip("재료 3개 이상 버전 템플릿")]
    public string comboTemplate_3;
    private Tweener tween;
    private RecipeEventManager recipeEventManager;

    void Start()
    {
        recipeEventManager = RecipeEventManager.Instance;
        recipeEventManager.OnPrintComboScript += PrintComboScript;
    }

    public void PrintComboScript(Stack<ComboRule> comboRules)
    {
        // 콤보 룰의 그룹명의 개수를 셈 -> 해당 개수에 맞춰 미리 준비된 템플릿 출력
        while(comboRules.Count > 0)
        {
            ComboRule comboRule = comboRules.Pop();
            // 2개일 경우 2개 템플릿에 맞춰 출력
            if(comboRule.requireAllGroups.Count == 2)
            {
                List<IngredientGroup> comboGruops = comboRule.requireAllGroups;
                string script = string.Format(comboTemplate_2,comboGruops[0].GroupName, comboGruops[1].GroupName);

                text.text = script;
            }
            else if(comboRule.requireAllGroups.Count == 3)
            {
                List<IngredientGroup> comboGruops = comboRule.requireAllGroups;
                string script = string.Format(comboTemplate_3,comboGruops[0].GroupName, comboGruops[1].GroupName, comboGruops[2].GroupName);

                text.text = script;
            }
            Debug.Log("대사 출력 완료 : ", comboRule.delta);
        }
    }
}
