using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPanel : MonoBehaviour
{
    public Image cakeImage;
    public List<TextMeshProUGUI> values;
    public TMP_InputField cakeName;
    public StatCounter statCounter;
    public CakeCaptureManager cakeCaptureManager;


    void Start()
    {
        // 스탯 표시
        CakeStat cakeStat = statCounter.GetFinalStat();
        for(int i = 0; i < values.Count; i++)
        {
            values[i].text = cakeStat.modifiers[i].delta.ToString();
        }
    }

    public void SetImage(byte[] pngData)
    {
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(pngData);

        Sprite sprite = Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));

        cakeImage.sprite = sprite;
    }
}
