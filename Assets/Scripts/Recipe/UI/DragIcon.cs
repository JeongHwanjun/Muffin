using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragIcon : MonoBehaviour
{
    public GameObject draggablePrefab; // 프리팹화된 아이콘
    public Canvas canvas;
    public RecipeEventManager recipeEventManager;

    [SerializeField] private GameObject draggableItem;

    void Start()
    {
        RecipeEventManager.OnIngredientDropped += UnlinkDraggableItem;
        //InstantiateNewIngredient(draggablePrefab.GetComponent<Ingredient>());
    }

    // 패널이 펼쳐지면 실제 아이템 생성
    void OnEnable()
    {
        draggableItem = InstantiateNewIngredient(draggablePrefab.GetComponent<Ingredient>());
    }
    
    // 패널이 닫힐 때 아이템 파괴
    void OnDisable()
    {
        if (draggableItem)
        {
            Destroy(draggableItem);
        }
    }

    // 아이템이 클릭되면 아이템 할당 해제(파괴하면 안되므로)
    void UnlinkDraggableItem(Ingredient droppedIngredient)
    {
        draggableItem = null;
    }

    public GameObject InstantiateNewIngredient(Ingredient droppedIngredient)
    {
        if (droppedIngredient.ingredientData.id != draggablePrefab.GetComponent<Ingredient>().ingredientData.id)
        {
            return null;
        }
        GameObject cloneIngredient = Instantiate(draggablePrefab, transform.parent.transform);
        RectTransform myRect = GetComponent<RectTransform>();
        RectTransform cloneRect = cloneIngredient.GetComponent<RectTransform>();
        cloneRect.anchoredPosition = myRect.anchoredPosition;
        //cloneRect.anchorMax = myRect.anchorMax;
        //cloneRect.anchorMin = myRect.anchorMin;
        cloneRect.localRotation = myRect.localRotation;
        cloneRect.localScale = myRect.localScale;
        //cloneIngredient.transform.SetParent(canvas.transform);
        // 이벤트 매니저 연결
        cloneIngredient.GetComponent<DraggableItem>().Init(recipeEventManager);

        return cloneIngredient;
    }

    
}
