using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientList : MonoBehaviour
{
    public IngredientPanel ingredientPanel;
    public List<GameObject> draggableIcons;
    public IngredientGroup ingredientGroup;

    private CanvasGroup canvasGroup;
    private PlayerData playerData;
    public void Init(Canvas canvas) // Start 시점 시작
    {
        Debug.Log("IngredientList Initialize");
        ingredientPanel.Init(canvas);

        canvasGroup = GetComponent<CanvasGroup>();
        playerData = PlayerData.Instance;

        if(playerData.ingredientTierDict.TryGetValue(ingredientGroup, out int tier))
        {
            Debug.Log("Tier : " + tier);
            if (tier <= 0) // 사용 불가능한 티어라면 숨기고 상호작용 불가능하게
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
            else
            {
                for(int i = 0; i < tier; i++)
                {
                    CanvasGroup iconCanvasGroup = draggableIcons[i].GetComponent<CanvasGroup>();
                    iconCanvasGroup.alpha = 1;
                    iconCanvasGroup.interactable = true;
                    iconCanvasGroup.blocksRaycasts = true;
                }
            }
        }
    }
}
