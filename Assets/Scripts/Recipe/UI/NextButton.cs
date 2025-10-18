using UnityEngine;

public class NextButton : MonoBehaviour
{
    public GameObject currentStage, NextStage;
    public GameObject origin, targetParent;
    [SerializeField] private IngredientType StageType = IngredientType.None;
    RecipeEventManager recipeEventManager;

    void Start()
    {
        recipeEventManager = RecipeEventManager.Instance;
        recipeEventManager.OnMoveToNextStage += MoveToNextStage;
    }

    public void OnMouseClick()
    {
        // 여기서 조건검사
        recipeEventManager.TriggerClickNextButton(StageType);
        recipeEventManager.OnMoveToNextStage += MoveToNextStage;
    }

    public void MoveToNextStage()
    {
        Debug.LogFormat("MoveStage from {0} to {1}", currentStage.name, NextStage.name);
        currentStage.SetActive(false);
        NextStage.SetActive(true);
    }

    public void CloneCakePanel()
    {
        if(origin != null && targetParent != null) Instantiate(origin, targetParent.transform);
    }
}
