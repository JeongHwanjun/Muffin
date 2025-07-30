using UnityEngine;
using UnityEngine.EventSystems;

public class CakePanel : MonoBehaviour, IDropHandler
{
    public CakeBuilder cakeBuilder;
    public RecipeEventManager recipeEventManager;

    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem dropped = eventData.pointerDrag?.GetComponent<DraggableItem>();
        if (dropped != null)
        {
            cakeBuilder.AddIngredient(dropped.ingredientData);
            //RecipeEventManager.TriggerIngredientDropped(dropped.GetComponent<Ingredient>());
        }
    }
}
