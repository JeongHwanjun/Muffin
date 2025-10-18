using UnityEngine;

// 원래 다음 스테이지로 이동하는것만 하는 애인데 그냥 단계 이동의 실질 동작을 담당하는 애가 됨.
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
        if (origin != null && targetParent != null) Instantiate(origin, targetParent.transform);
    }
}
