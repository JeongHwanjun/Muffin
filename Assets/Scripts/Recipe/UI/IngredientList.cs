using System.Collections.Generic;
using UnityEngine;

public class IngredientList : MonoBehaviour
{
    private Canvas canvas; // 최상위 canvas - draggableItem이 드래그시 참조함
    public RecipeEventManager recipeEventManager;
    public List<IngredientPanel> ingredientPanels;
    private int _sortingOrder = 1;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        // recipeEventManager는 지정됨


        foreach (var ingredientPanel in ingredientPanels) ingredientPanel.Init(canvas, recipeEventManager);
    }

    public void ExpandPanel(GameObject panel) // 패널을 펼칠 때 수행되어 렌더링 우선순위 지정
    {
        var priCanvas = panel.GetComponent<Canvas>(); // 패널을 펼칠 때 렌더링 우선순위 변경용 canvas.
        if (priCanvas == null) priCanvas = panel.AddComponent<Canvas>();
        priCanvas.overrideSorting = true;

        priCanvas.sortingOrder = _sortingOrder++;
    }
}
