using System.Collections.Generic;
using UnityEngine;

public class ArrowsBackground : MonoBehaviour
{
    [SerializeField] private GameObject CakeRecipePrefab; // 레시피와 진행상황을 표시할 UI
    private CanvasGroup canvasGroup;
    private CakeManager cakeManager;
    private ManufactureEventManager manufactureEventManager;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;
        cakeManager = CakeManager.Instance;
        manufactureEventManager = ManufactureEventManager.Instance;

        // 하위 레시피 생성
        //1. CakeManager에서 케이크 목록을 가져옴
        List<Cake> cakes = cakeManager.cakes;
        int index = 0;
        foreach (Cake cake in cakes)
        {
            //2. 그 목록을 순회하며 CakeRecipe를 생성하고 초기화함
            GameObject newCakeRecipe = Instantiate(CakeRecipePrefab, transform);
            newCakeRecipe.GetComponent<CakeRecipeUI>().Initialize(cake, index++);
        }

        manufactureEventManager.OnShowRecipe += ShowRecipe;
        manufactureEventManager.OnHideRecipe += HideRecipe;
        

        HideRecipe();
    }

    public void ShowRecipe()
    {
        canvasGroup.alpha = 1;
    }

    public void HideRecipe()
    {
        canvasGroup.alpha = 0;
    }
}
