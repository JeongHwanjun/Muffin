using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* 케이크의 속성값을 볼 수 있도록 수정 및 관리 */
public class StatDisplayer : MonoBehaviour
{
    public StatCounter statCounter;
    public RecipeEventManager recipeEventManager;
    public Arrows totalArrows;
    public TextMeshProUGUI[] textValue;
    public TextMeshProUGUI[] textMultipliers;
    private CakeStat values = null;
    private StatMultipliers multipliers;

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
        foreach (TextMeshProUGUI textUI in textValue)
        {
            if (index >= values.modifiers.Count)
            {
                Debug.LogWarningFormat("values의 개수가 예상과 다릅니다. {0}", values.modifiers.Count);
                break;
            }
            textUI.text = values.modifiers[index++].delta.ToString();
        }

        index = 0;
        foreach (TextMeshProUGUI textUI in textMultipliers)
        {
            if (multipliers.modifiers != null)
            {
                if (index >= multipliers.modifiers.Count)
                {
                    Debug.LogWarningFormat("multipliers의 개수가 예상과 다릅니다. {0}", multipliers.modifiers.Count);
                    break;
                }
                textUI.text = multipliers.modifiers[index++].delta.ToString();
            }
            else
            {
                Debug.LogWarningFormat("multipliers.modifiers가 null임");
                textUI.text = "1"; // 해당 정보가 없으면 그냥 1임
            }
        }
    }

    private void SetArrows()
    {
        totalArrows.RefreshArrows(statCounter.GetRecipeArrows());
    }
}
