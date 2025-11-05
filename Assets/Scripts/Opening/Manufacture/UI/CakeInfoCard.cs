using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CakeInfoCard : MonoBehaviour
{
    private CakeManager cakeManager;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI text;
    private Cake myCake;

    public void Initialize(Cake cake)
    {
        cakeManager = CakeManager.Instance;
        image.sprite = cake.sprite;
        text.text = cake.quantity.ToString();
        myCake = cake;

        cakeManager.OnCakeDataChanged += RefreshCard;
    }

    void RefreshCard(int cakeIndex)
    {
        if(myCake == cakeManager.cakes[cakeIndex]) // 만약 변경된게 나라면
        {
            text.text = myCake.quantity.ToString();
        }
    }
}
