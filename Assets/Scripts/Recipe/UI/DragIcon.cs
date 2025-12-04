using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragIcon : MonoBehaviour
{
    public GameObject draggablePrefab; // 프리팹화된 Ingredient
    private Canvas canvas;
    private RecipeEventManager recipeEventManager;

    [SerializeField] private GameObject draggableItem;
    [SerializeField] private Ingredient ingredientData; // Ingredient의 정보를 담고 있는 SO
    private Sprite sprite;

    public void Init(Canvas parentCanvas, Ingredient ingredientData, Sprite ingredientSprite) // Start 시점 호출
    {
        canvas = parentCanvas;
        recipeEventManager = RecipeEventManager.Instance;
        this.ingredientData = ingredientData;
        sprite = ingredientSprite;
        GetComponent<Image>().sprite = sprite;
        recipeEventManager.OnIngredientClick += UnlinkDraggableItem;
    }

    // 패널이 펼쳐지면 실제 아이템 생성
    void OnEnable()
    {
        draggableItem = InstantiateNewIngredient(ingredientData);
    }
    
    // 패널이 닫힐 때 아이템 파괴
    void OnDisable()
    {
        Debug.Log("DragIcon : Destroy DraggableItem");
        if (draggableItem)
        {
            Destroy(draggableItem);
        }
    }

    // 아이템이 클릭되면 아이템 할당 해제(파괴하면 안되므로)
    void UnlinkDraggableItem(Ingredient droppedIngredient)
    {
        if (draggableItem && ingredientData == droppedIngredient)
        {
            draggableItem = null;
        }
    }

    public GameObject InstantiateNewIngredient(Ingredient droppedIngredient)
    {
        if(droppedIngredient == null || ingredientData == null) return null;
        if (droppedIngredient?.id != ingredientData?.id) return null;

        GameObject cloneIngredient = Instantiate(draggablePrefab, transform);
        // 이벤트 매니저 연결
        cloneIngredient.GetComponent<DraggableItem>().Init(canvas, ingredientData, sprite);

        return cloneIngredient;
    }
}
