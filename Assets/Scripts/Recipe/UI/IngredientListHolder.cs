using System.Collections.Generic;
using UnityEngine;

public class IngredientListHolder : MonoBehaviour
{
    private Canvas canvas; // 최상위 canvas - draggableItem이 드래그시 참조함
    private RecipeEventManager recipeEventManager;
    private IngredientList[] ingredientLists;
    private int _sortingOrder = 1;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        recipeEventManager = RecipeEventManager.Instance;
        ingredientLists = GetComponentsInChildren<IngredientList>();


        foreach (var ingredientList in ingredientLists) ingredientList.Init(canvas);
    }

    public void OnExpandPanel(GameObject panel) // 패널을 펼칠 때 수행되어 렌더링 우선순위 지정
    {
        var priCanvas = panel.GetComponent<Canvas>(); // 패널을 펼칠 때 렌더링 우선순위 변경용 canvas.
        if (priCanvas == null) priCanvas = panel.AddComponent<Canvas>();
        priCanvas.overrideSorting = true;

        priCanvas.sortingOrder = _sortingOrder++;
    }
}
