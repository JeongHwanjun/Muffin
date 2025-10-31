using UnityEngine;
using UnityEngine.EventSystems;

public class CakeSlot : MonoBehaviour, IPointerClickHandler
{
    public CakeCard cakeCard; // 케이크 모습을 표시할 케이크 카드
    private OpeningReadyEventManager openingReadyEventManager;
    private string id;

    void Start()
    {
        // 시작시 비활성화(안보이게)
        cakeCard.gameObject.SetActive(false);
        openingReadyEventManager = OpeningReadyEventManager.Instance;
    }

    public void SetCakeCard(CakeCard settingCakeCard)
    {
        cakeCard.gameObject.SetActive(true);
        // 드래그&드랍으로 케이크 슬롯에 케이크 설정
        cakeCard.SetImage(settingCakeCard.image.sprite);
        cakeCard.SetText(settingCakeCard.displayName.text);
        id = settingCakeCard.id;
        cakeCard.SetId(id);
    }

    public void UnSetCakeCard()
    {
        cakeCard.SetImage(null);
        cakeCard.SetText("");
        id = null;
        cakeCard.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            // 등록되어있던 카드 재생성
            openingReadyEventManager.TriggerCloneNewCard(id);
            // 등록된 경로 삭제
            openingReadyEventManager.TriggerDeleteCake(cakeCard.cakePath);
            // 카드 슬롯 비우기
            UnSetCakeCard();
        }
    }
}
