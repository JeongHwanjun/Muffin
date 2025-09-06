using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientPanel : MonoBehaviour
{
    public List<GameObject> ingredients;
    private RectTransform rectTransform;
    private HorizontalLayoutGroup horizontalLayoutGroup;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();

        float paddingLeft = horizontalLayoutGroup.padding.left, paddingRight = horizontalLayoutGroup.padding.right;
        float paddingTop = horizontalLayoutGroup.padding.top, paddingBot = horizontalLayoutGroup.padding.bottom;

        rectTransform.sizeDelta = new Vector2(ingredients.Count * (100 + paddingLeft + paddingRight), 100 + paddingTop + paddingBot);
    }
}
