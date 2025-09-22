using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientPanel : MonoBehaviour
{
    private RectTransform rectTransform;
    private HorizontalLayoutGroup horizontalLayoutGroup;
    [SerializeField] private int iconSize;
    private List<DragIcon> dragIcons = new();
    public Canvas canvas;
    public RecipeEventManager recipeEventManager;

    public void Init(Canvas parentCanvas, RecipeEventManager eventManager) // awake 시점 호출
    {
        canvas = parentCanvas;
        recipeEventManager = eventManager;

        rectTransform = GetComponent<RectTransform>();
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();

        float paddingLeft = horizontalLayoutGroup.padding.left, paddingRight = horizontalLayoutGroup.padding.right;
        float paddingTop = horizontalLayoutGroup.padding.top, paddingBot = horizontalLayoutGroup.padding.bottom;

        rectTransform.sizeDelta = new Vector2(transform.childCount * (iconSize + paddingLeft + paddingRight), iconSize + paddingTop + paddingBot);

        dragIcons.AddRange(GetComponentsInChildren<DragIcon>());
        foreach (var icon in dragIcons) icon.Init(canvas, recipeEventManager);
    }
}
