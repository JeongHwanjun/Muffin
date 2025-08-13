using TMPro;
using UnityEngine;

/* 케이크의 속성값을 볼 수 있도록 수정 및 관리 */
public class StatDisplayer : MonoBehaviour
{
    public StatCounter statCounter;
    public RecipeEventManager recipeEventManager;
    public TextMeshPro tasteText, flavorText, textureText, appearanceText, costText;
    private CakeStat values;
    private StatMultipliers multipliers;

    void Start()
    {
        RefreshText();

        recipeEventManager.OnRefreshUI += RefreshText;
    }

    public void RefreshText()
    {
        GetCakeValue();
        SetText();
    }

    public void GetCakeValue()
    {
        values = statCounter.GetMultipliedStat();
    }

    public void GetMultipliers()
    {
        // 배율 가져오기
    }

    public void SetText()
    {
        tasteText.text = values.taste.ToString();
        flavorText.text = values.flavor.ToString();
        textureText.text = values.texture.ToString();
        appearanceText.text = values.appearance.ToString();
        costText.text = values.cost.ToString();
    }
}
