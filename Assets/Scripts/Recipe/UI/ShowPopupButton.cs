using UnityEngine;
using UnityEngine.UI;

public class ShowPopupButton : MonoBehaviour
{
    public GameObject popUp;
    private RecipeEventManager recipeEventManager;
    private Button popUpButton;

    void Start()
    {
        recipeEventManager = RecipeEventManager.Instance;

        recipeEventManager.OnScriptPlayCompleted += MakeInteractable;
        popUpButton = GetComponent<Button>();

        popUpButton.interactable = false;
    }

    void MakeInteractable()
    {
        // 콤보 출력 완료시 호출되어, 버튼을 누를 수 있는 상태로 바꾼다.
        popUpButton.interactable = true;
    }

    public void OnClick()
    {
        popUp.SetActive(true);
    }
}
