using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Ingredient ingredientData; // 에디터에서 초기화
    private Canvas canvas;
    private RecipeEventManager recipeEventManager;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private GameObject panel;

    public void Init(Canvas c, Ingredient ingredientData, Sprite ingredientSprite)
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        GetComponent<Image>().sprite = ingredientSprite;
        canvas = c;
        this.ingredientData = ingredientData;
        panel = transform.parent.gameObject;

        recipeEventManager = RecipeEventManager.Instance;

        Debug.LogFormat("DraggableItem : Initialized! {0}", recipeEventManager);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("DraggableItem : BeginDrag");
        panel = transform.parent.parent.gameObject; // 현재 부모(panel)를 다른 곳에 기록해둠 - 나중에 비활성화
        transform.SetParent(canvas.transform, worldPositionStays: true); // 부모를 최상위 canvas로 변경. panel을 비활성화해도 남아있게 하기 위함
        //rectTransform.anchorMin = new Vector2(0.5f, 0.5f); // 굳이 바꾸면 좌표는 유지된채로 기준만 바뀌어서 위치가 이상하게 바뀜;;
        //rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        canvasGroup.blocksRaycasts = false; // 한번 드래그를 끝내면 다시 드래그하지 못함
        recipeEventManager.TriggerIngredientClick(ingredientData);

        StartCoroutine(SetPanel());
    }

    IEnumerator SetPanel()
    {
        yield return null;
        panel.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdatePosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("DraggableItem : EndDrag");
        RecipeEventManager.TriggerIngredientDropped(ingredientData);
        if (TryFindValidDropTarget(eventData, out GameObject target))
        {
            Debug.Log("DraggableItem : Dropped on Valid item");
            transform.SetParent(target.transform, worldPositionStays: true); // 부모를 cakePanel로 변경(이미지 캡처를 위해)
            canvasGroup.interactable = false;   // 조작 무효화
            canvasGroup.ignoreParentGroups = true; // 부모 영향 무시

            recipeEventManager.TriggerIngredientAdd(ingredientData);
        }
        else
        {
            Debug.Log("DraggableItem : Dropped on Invalid item");
            Destroy(gameObject);
        }

        //panel.SetActive(false); // 패널 비활성화
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
        //Vector2 pos;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out pos);
        //rectTransform.anchoredPosition = pos;

        float scale = canvas != null ? canvas.scaleFactor : 1f;
        rectTransform.anchoredPosition += eventData.delta / Mathf.Max(0.0001f, scale);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Ingredient의 Stat을 보여주는 UI에 정보 전송 및 갱신
        recipeEventManager.TriggerIngredientHover(ingredientData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        // UI 비활성화 목적의 이벤트 발생
        recipeEventManager.TriggerIngredientHoverExit();
    }
}