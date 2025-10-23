using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CakeCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public TextMeshPro displayName;

    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    public void Initialize(CakeMetaData cakeMetaData)
    {
        // 이름 지정
        displayName.text = cakeMetaData.displayName;
        // 이미지 로드
        byte[] imageByte = File.ReadAllBytes(cakeMetaData.imagePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageByte);

        Sprite sprite = Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));

        image.sprite = sprite;
    }

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        float scale = canvas != null ? canvas.scaleFactor : 1f;
        rectTransform.anchoredPosition += eventData.delta / Mathf.Max(0.0001f, scale);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (TryFindValidDropTarget(eventData, out GameObject target))
        {
            Debug.Log("DraggableItem : Dropped on Valid item");
            transform.SetParent(target.transform, worldPositionStays: true); // 부모를 cakePanel로 변경(이미지 캡처를 위해)
            canvasGroup.interactable = false;   // 조작 무효화
            canvasGroup.ignoreParentGroups = true; // 부모 영향 무시
        }
        else
        {
            Debug.Log("DraggableItem : Dropped on Invalid item");
            Destroy(gameObject);
        }
        throw new System.NotImplementedException();
    }

    private bool TryFindValidDropTarget(PointerEventData eventData, out GameObject target)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject.GetComponent<CakePanel>() != null)
            {
                target = result.gameObject;
                return true;
            }
        }

        target = null;
        return false;
    }
}
