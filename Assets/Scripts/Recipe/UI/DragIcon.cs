using UnityEngine;
using UnityEngine.EventSystems;

public class DragIcon : MonoBehaviour
{
    public GameObject draggablePrefab; // 프리팹화된 아이콘
    public Canvas canvas;
    public RecipeEventManager recipeEventManager;

    void Start()
    {
        RecipeEventManager.OnIngredientDropped += InstantiateNewIngredient;
        InstantiateNewIngredient(draggablePrefab.GetComponent<Ingredient>());
    }

    /*
    public void OnPointerDown(PointerEventData eventData)
    {
        // 드래그 가능한 아이콘 생성
        GameObject newIcon = Instantiate(draggablePrefab, canvas.transform);
        newIcon.transform.SetAsLastSibling();

        // 초기 위치 마우스 근처로
        RectTransform rt = newIcon.GetComponent<RectTransform>();
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out pos);
        rt.anchoredPosition = pos;

        // 드래그 상태로 진입시키기 (선택 사항: 직접 호출 또는 내부 처리)
        ExecuteEvents.Execute<IBeginDragHandler>(newIcon, eventData, ExecuteEvents.beginDragHandler);
        ExecuteEvents.Execute<IDragHandler>(newIcon, eventData, ExecuteEvents.dragHandler);
    }
    */

    public void InstantiateNewIngredient(Ingredient droppedIngredient)
    {
        if (droppedIngredient.ingredientData.id != draggablePrefab.GetComponent<Ingredient>().ingredientData.id)
        {
            return;
        }
        GameObject cloneIngredient = Instantiate(draggablePrefab, transform.parent.transform);
        RectTransform myRect = GetComponent<RectTransform>();
        RectTransform cloneRect = cloneIngredient.GetComponent<RectTransform>();
        cloneRect.anchoredPosition = myRect.anchoredPosition;
        //cloneRect.anchorMax = myRect.anchorMax;
        //cloneRect.anchorMin = myRect.anchorMin;
        cloneRect.localRotation = myRect.localRotation;
        cloneRect.localScale = myRect.localScale;
        cloneIngredient.transform.SetParent(canvas.transform);
        // 이벤트 매니저 연결
        cloneIngredient.GetComponent<DraggableItem>().Init(recipeEventManager);
    }
}
