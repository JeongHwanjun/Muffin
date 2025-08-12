using TMPro;
using UnityEngine;

/* 케이크의 속성값을 볼 수 있도록 수정 및 관리 */
public class StatDisplayer : MonoBehaviour
{
    public TextMeshPro tasteText, flavorText, textureText, appearanceText, costText;
    private int tasteValue, flavorValue, textureValue, appearanceValue, costValue;

    void Start()
    {
        tasteValue = 0;
        flavorValue = 0;
        textureValue = 0;
        appearanceValue = 0;
    }

    public void RefreshText()
    {
        GetCakeValue();
        SetText();
    }

    public void GetCakeValue()
    {
        // 아무튼 가져옴
    }

    public void SetText()
    {
        tasteText.text = tasteValue.ToString();
        flavorText.text = flavorValue.ToString();
        textureText.text = textureValue.ToString();
        appearanceText.text = appearanceValue.ToString();
        costText.text = costValue.ToString();
    }
}
