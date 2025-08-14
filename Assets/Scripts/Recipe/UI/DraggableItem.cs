using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum RecipeStage
{
    Flour = 0,
    Base = 1,
    Toping = 2
}
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Ingredient ingredientData;
    public Canvas canvas;
    public RecipeEventManager recipeEventManager;
    public RecipeStage stage;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public void Init(RecipeEventManager r)
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        ingredientData = GetComponent<Ingredient>();

        recipeEventManager = r;
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

            // 이벤트 발생
            if (stage == RecipeStage.Flour)
            {
                StatMultipliers newFlour = new StatMultipliers(ingredientData.ingredientData);
                recipeEventManager.TriggerFlourAdd(newFlour);
            }
            else if (stage == RecipeStage.Base)
            {
                // base
            }
            else if (stage == RecipeStage.Toping)
            {
                recipeEventManager.TriggerIngredientAdd(ingredientData);
            }
            else
            {
                Debug.LogWarningFormat("DraggableItem : Invalid Stage - {0}", stage);
            }
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