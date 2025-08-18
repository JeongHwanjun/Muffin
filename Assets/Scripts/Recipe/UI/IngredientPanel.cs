using System.Collections.Generic;
using UnityEngine;

public class IngredientPanel : MonoBehaviour
{
    public List<GameObject> ingredients;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        rectTransform.sizeDelta = new Vector2(ingredients.Count * 100, 100);

        int index = 0;
        foreach (GameObject ingredient in ingredients)
        {
            RectTransform ingredientTransform = ingredient.GetComponent<RectTransform>();
            ingredientTransform.anchoredPosition = new Vector2(index * 100, 0);
            index++;
        }
    }
}
