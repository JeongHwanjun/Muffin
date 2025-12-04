using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* 케이크의 속성값을 볼 수 있도록 수정 및 관리 */
public class StatDisplayer : MonoBehaviour
{
    public StatCounter statCounter;
    public RecipeEventManager recipeEventManager;
    public Arrows totalArrows;
    public List<AnimatedText> textValue;
    public List<AnimatedText> textMultipliers;
    public float duration = 0.5f; // 숫자 애니매이션 지속시간

    private CakeStat values = null;
    private StatMultipliers multipliers = null;
    private bool firstRoll = true;

    void Start()
    {
        RefreshUI();

        recipeEventManager.OnRefreshUI += RefreshUI;
    }

    public void RefreshUI()
    {
        GetCakeValue();
        GetMultipliers();
        SetText();
        SetArrows();

        values = null;
        multipliers = null;
    }

    public void GetCakeValue()
    {
        values = statCounter.GetFinalStat();
        if (values == null) values = new();
    }

    public void GetMultipliers()
    {
        multipliers = statCounter.GetMultipliers();
    }

    public void SetText()
    {
        int index = 0;
        // 수치
        foreach (AnimatedText textUI in textValue)
        {
            if (index >= values.modifiers.Count)
            {
                Debug.LogWarningFormat("values의 개수가 예상과 다릅니다. {0}", values.modifiers.Count);
                break;
            }

            // 값을 가져옴
            int newValue = values.modifiers[index++].delta;
            Debug.LogWarningFormat("newValue : {0}, currentValue : {1}", newValue, textUI.currentValue);
            // 변경된 값이 기존과 같으면 스킵
            if(newValue == textUI.currentValue && !firstRoll) continue;
            //Debug.LogWarningFormat("currentValue : {0}", textUI.currentValue);

            textUI.tween?.Kill(); // 기존 tweener 중단
            int from = textUI.currentValue;
            int to = newValue;

            textUI.tween = DOTween.To(
                () => from,
                x =>
                {
                    from = x;
                    textUI.currentValue = x;
                    textUI.text.text = textUI.currentValue.ToString();
                },
                to,
                duration
            ).SetEase(Ease.OutQuad);
        }

        index = 0;
        foreach (AnimatedText textUI in textMultipliers)
        {
            if (multipliers.modifiers != null)
            {
                if (index >= multipliers.modifiers.Count)
                {
                    Debug.LogWarningFormat("multipliers의 개수가 예상과 다릅니다. {0}", multipliers.modifiers.Count);
                    break;
                }
                // 값을 가져옴
                int newMultiplier = multipliers.modifiers[index++].delta;
                // 변경된 값이 기존과 같으면 스킵
                if(newMultiplier == textUI.currentValue && !firstRoll) continue;

                textUI.tween?.Kill(); // 기존 tweener 중단
                int from = textUI.currentValue;
                int to = newMultiplier;

                textUI.tween = DOTween.To(
                    () => from,
                    x =>
                    {
                        from = x;
                        textUI.currentValue = x;
                        textUI.text.text = textUI.currentValue.ToString();
                    },
                    to,
                    duration
                ).SetEase(Ease.OutQuad);
            }
            else
            {
                Debug.LogWarningFormat("multipliers.modifiers가 null임");
                textUI.text.text = "1"; // 해당 정보가 없으면 그냥 1임
            }
        }

        firstRoll = false;
    }

    private void SetArrows()
    {
        totalArrows.RefreshArrows(statCounter.GetRecipeArrows());
    }
}
