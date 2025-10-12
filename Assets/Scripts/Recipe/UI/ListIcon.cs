using UnityEngine;
using UnityEngine.UI;

public class ListIcon : MonoBehaviour
{
    private Button iconButton;         // 클릭용 아이콘 버튼
    public GameObject dropdownPanel;  // 항목을 포함하는 패널
    public IngredientGroup ingredientGroup; // 자신이 표현하는 Ingredient가 어떤 분류에 속하는지
    private IngredientListHolder ingredientListHolder; // 렌더링 순서 조절용으로 참고. ExpandPanel 실행 후 sortingOrder 변경
    private RecipeEventManager recipeEventManager;
    private bool shouldActiveSelf = false;

    void Start()
    {
        iconButton = GetComponent<Button>();
        ingredientListHolder = GetComponentInParent<IngredientListHolder>();
        recipeEventManager = RecipeEventManager.Instance;
        recipeEventManager.OnPanelExpand += OnPanelExpand;

        SetPanelActive(false);

        iconButton.onClick.AddListener(() =>
        {
            shouldActiveSelf = true;
            recipeEventManager.TriggerPanelExpand(); // 패널 확장시 다른 패널을 비활성화
        });
    }

    public void SetPanelActive(bool value) { dropdownPanel.SetActive(value); }

    private void OnPanelExpand()
    {
        // 패널 확장 이벤트임. 이 이벤트를 받으면 자신이 비활성화되어야 하는지 체크하고 비활성화함.
        if (shouldActiveSelf)
        {
            bool isActive = dropdownPanel.activeSelf;
            SetPanelActive(!isActive);
            ingredientListHolder.OnExpandPanel(dropdownPanel); // 렌더링 순서 조절
            shouldActiveSelf = false;
            return;
        }

        SetPanelActive(false);
    }
}
