using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientList : MonoBehaviour
{
    public IngredientPanel ingredientPanel;
    public List<GameObject> draggableIcons;
    public IngredientGroup ingredientGroup;
    public GameObject panelIcon;

    private CanvasGroup canvasGroup;
    private PlayerData playerData;
    private Sprite panelSprite;
    

    public void Init(Canvas canvas) // Start 시점 시작
    {
        Debug.Log("IngredientList Initialize");
        ingredientPanel.Init(canvas);

        canvasGroup = GetComponent<CanvasGroup>();
        panelSprite = panelIcon.GetComponent<Image>().sprite;
        playerData = PlayerData.Instance;

        if (playerData.ingredientTierDict.TryGetValue(ingredientGroup, out int tier))
        {
            if (tier <= 0) // 사용 불가능한 티어라면 숨기고 상호작용 불가능하게
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
            else
            {
                for (int i = 0; i < tier; i++)
                {
                    CanvasGroup iconCanvasGroup = draggableIcons[i].GetComponent<CanvasGroup>();
                    iconCanvasGroup.alpha = 1;
                    iconCanvasGroup.interactable = true;
                    iconCanvasGroup.blocksRaycasts = true;
                }
            }
        }

        InitializeDragIcons(canvas);
    }

    private void InitializeDragIcons(Canvas parentCanvas)
    {
        int index = 0;
        foreach(Ingredient tier in ingredientGroup.tiers)
        {
            DragIcon dragIcon = draggableIcons[index].GetComponent<DragIcon>();
            dragIcon.Init(parentCanvas, tier, panelSprite);
        }
    }
}
