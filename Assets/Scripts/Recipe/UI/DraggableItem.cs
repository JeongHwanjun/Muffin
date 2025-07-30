using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Ingredient ingredientData;
    public Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        ingredientData = GetComponent<Ingredient>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("DraggableItem : BeginDrag");
        canvasGroup.blocksRaycasts = false; // 한번 드래그를 끝내면 다시 드래그하지 못함
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdatePosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("DraggableItem : EndDrag");
        RecipeEventManager.TriggerIngredientDropped(GetComponent<Ingredient>());
        if (TryFindValidDropTarget(eventData, out GameObject target))
        {
            Debug.Log("DraggableItem : Dropped on Valid item");
            canvasGroup.interactable = false;   // 조작 무효화
            canvasGroup.ignoreParentGroups = true; // 부모 영향 무시
        }
        else
        {
            Debug.Log("DraggableItem : Dropped on Invalid item");
            Destroy(gameObject);
        }
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

    private void UpdatePosition(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out pos);
        rectTransform.anchoredPosition = pos;
    }
}