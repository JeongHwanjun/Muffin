using UnityEngine;

public class RevertButton : MonoBehaviour
{
    public GameObject prevStage, currentStage;
    [SerializeField] private IngredientType StageType = IngredientType.None;
    RecipeEventManager recipeEventManager;

    void Start()
    {
        recipeEventManager = RecipeEventManager.Instance;
        recipeEventManager.OnMoveToPrevStage += MoveToPrevStage;
    }

    public void OnMouseClick()
    {
        recipeEventManager.TriggerIngredientSub(StageType);
    }
    public void MoveToPrevStage()
    {
        if (prevStage == null) return;
        Debug.LogFormat("MoveStage from {0} to {1}", currentStage.name, prevStage.name);
        currentStage.SetActive(false);
        prevStage.SetActive(true);
    }
}
