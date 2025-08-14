using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* 케이크의 속성값을 볼 수 있도록 수정 및 관리 */
public class StatDisplayer : MonoBehaviour
{
    public StatCounter statCounter;
    public RecipeEventManager recipeEventManager;
    public TextMeshProUGUI tasteText, flavorText, textureText, appearanceText, costText;
    public TextMeshProUGUI tasteMText, flavorMText, textureMText, appearanceMText, costMText;
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
        GetMultipliers();
        SetText();
    }

    public void GetCakeValue()
    {
        values = statCounter.GetMultipliedStat();
    }

    public void GetMultipliers()
    {
        multipliers = statCounter.GetMultipliers();
    }

    public void SetText()
    {
        // 수치
        tasteText.text = values.taste.ToString();
        flavorText.text = values.flavor.ToString();
        textureText.text = values.texture.ToString();
        appearanceText.text = values.appearance.ToString();
        costText.text = values.cost.ToString();

        // 배율
        tasteMText.text = multipliers.taste.ToString();
        flavorMText.text = multipliers.flavor.ToString();
        textureMText.text = multipliers.texture.ToString();
        appearanceMText.text = multipliers.appearance.ToString();
        costMText.text = multipliers.cost.ToString();
    }
}
