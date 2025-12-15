using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

// 콤보 목록을 받아 지정된 템플릿에 맞게 출력하는 스크립트
public class ComboScriptPrinter : MonoBehaviour
{
    public TextMeshProUGUI text;
    public StatCounter statCounter;
    [Tooltip("재료 2개 버전 템플릿")] public string comboTemplate_2;
    [Tooltip("재료 3개 이상 버전 템플릿")] public string comboTemplate_3;
    public float characterInterval = 0.05f;
    public float interScriptDelay = 1.0f;

    private RecipeEventManager recipeEventManager;

    void Start()
    {
        recipeEventManager = RecipeEventManager.Instance;

        PrintComboScript(statCounter.GetComboRuleStack());
    }

    public void PrintComboScript(Stack<ComboRule> comboRules)
    {
        StartCoroutine(PlayComboScripts(comboRules));
    }

    private IEnumerator PlayComboScripts(Stack<ComboRule> comboRules)
    {
        // 콤보 룰의 그룹명의 개수를 셈 -> 해당 개수에 맞춰 미리 준비된 템플릿 출력
        while(comboRules.Count > 0)
        {
            ComboRule comboRule = comboRules.Pop();
            // 이 콤보룰을 스탯에 추가해줘야 함.
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
            text.maxVisibleCharacters = 0;
            Tween tween = DOTween.To(() => text.maxVisibleCharacters,
                        x => text.maxVisibleCharacters = x,
                        text.text.Length,
                        characterInterval * text.text.Length);
            Debug.Log("루프 하나 끝남");
            yield return tween.WaitForCompletion(); // 완료될 때까지 기다림

            // 스탯 상승
            Debug.Log("스탯 상승");
            statCounter.AddComboStat(comboRule);


            yield return new WaitForSeconds(interScriptDelay); // 지정된 시간만큼 기다린 후 다음 콤보로 넘어감
        }

        // 모든 콤보 출력이 완료됨을 알림.
        OnScriptPlayCompleted();
    }

    private void OnScriptPlayCompleted()
    {
        recipeEventManager.TriggerScriptPlayCompleted();
        Debug.Log("Script Play Completed!");
    }
}
