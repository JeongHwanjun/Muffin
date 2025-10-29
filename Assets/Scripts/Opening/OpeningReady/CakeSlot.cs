using UnityEngine;

public class CakeSlot : MonoBehaviour
{
    public CakeCard cakeCard; // 케이크 모습을 표시할 케이크 카드

    void Start()
    {
        // 시작시 비활성화(안보이게)
        cakeCard.gameObject.SetActive(false);
    }

    public void SetCakeCard(CakeCard settingCakeCard)
    {
        // 드래그&드랍으로 케이크 슬롯에 케이크 설정
        cakeCard.SetImage(settingCakeCard.image.sprite);
        cakeCard.SetText(settingCakeCard.displayName.text);
    }

    public void UnSetCakeCard()
    {
        
    }
}
