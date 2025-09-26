using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CakePanel : MonoBehaviour, IDropHandler
{
    public CakeBuilder cakeBuilder;
    public RecipeEventManager recipeEventManager;
    private Stack<GameObject> draggableItems; // 추가된 아이템들. Revert이벤트와 관련됨.

    void Start()
    {
        // 이벤트 구독
        recipeEventManager = RecipeEventManager.Instance;
        recipeEventManager.OnIngredientSub += OnIngredientSub;
    }
    void OnEnable()
    {
        draggableItems = new();
    }
    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem dropped = eventData.pointerDrag?.GetComponent<DraggableItem>();
        if (dropped != null)
        {
            //RecipeEventManager.TriggerIngredientDropped(dropped.GetComponent<Ingredient>());
            draggableItems.Push(dropped.gameObject);
        }
    }

    void OnIngredientSub()
    {
        // 가장 최근의 재료를 파괴함
        GameObject lastItem = draggableItems.Pop();
        Destroy(lastItem);
    }
}
