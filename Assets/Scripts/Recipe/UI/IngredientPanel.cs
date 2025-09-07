using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientPanel : MonoBehaviour
{
    private RectTransform rectTransform;
    private HorizontalLayoutGroup horizontalLayoutGroup;
    [SerializeField] private int iconSize;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();

        float paddingLeft = horizontalLayoutGroup.padding.left, paddingRight = horizontalLayoutGroup.padding.right;
        float paddingTop = horizontalLayoutGroup.padding.top, paddingBot = horizontalLayoutGroup.padding.bottom;

        rectTransform.sizeDelta = new Vector2(transform.childCount * (iconSize + paddingLeft + paddingRight), iconSize + paddingTop + paddingBot);
    }
}
