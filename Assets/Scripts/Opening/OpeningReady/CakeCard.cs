using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CakeCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public TextMeshProUGUI displayName;
    public string cakePath;
    public string id;

    private Canvas parentCanvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private OpeningReadyEventManager openingReadyEventManager;
    private CakeCardPlaceholder parent;
    public void Initialize(string id, string displayName, Sprite sprite, string path, CakeCardPlaceholder parent)
    {
        // 이름 지정
        SetText(displayName);
        // 이미지 지정
        SetImage(sprite);
        // 케이크 경로 지정
        SetPath(path);
        this.id = id;
        this.parent = parent;
    }
    public void SetText(string text) { displayName.text = text; }
    public void SetImage(Sprite sprite) { image.sprite = sprite; }
    public void SetPath(string path) { cakePath = path; }
    public void SetId(string id){ this.id = id; }

    void Start()
    {
        parentCanvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        openingReadyEventManager = OpeningReadyEventManager.Instance;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 시작 이벤트 발생
        parent.UnchainCard();
        transform.SetParent(parentCanvas.transform, worldPositionStays: true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float scale = parentCanvas != null ? parentCanvas.scaleFactor : 1f;
        rectTransform.anchoredPosition += eventData.delta / Mathf.Max(0.0001f, scale);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 종료 이벤트 발생
        if (TryFindValidDropTarget(eventData, out GameObject target))
        {
            Debug.Log("CakeCard : Dropped on Valid item");
            transform.SetParent(target.transform, worldPositionStays: true); // 부모를 슬롯으로 변경
            canvasGroup.interactable = false;   // 조작 무효화
            canvasGroup.ignoreParentGroups = true; // 부모 영향 무시

            // 경로 등록
            openingReadyEventManager.TriggerEnlistCake(cakePath);

            target.GetComponent<CakeSlot>().SetCakeCard(this);
        }
        else
        {
            Debug.Log("CakeCard : Dropped on Invalid item");
            // 카드 재생성 이벤트 발생
            openingReadyEventManager.TriggerCloneNewCard(id);
        }
        // 이 카드는 쓸모없으므로 파괴함
        Destroy(gameObject);
    }

    private bool TryFindValidDropTarget(PointerEventData eventData, out GameObject target)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject.GetComponent<CakeSlot>() != null)
            {
                target = result.gameObject;
                return true;
            }
        }

        target = null;
        return false;
    }
}
