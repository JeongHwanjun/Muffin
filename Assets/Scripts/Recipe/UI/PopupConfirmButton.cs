using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupConfirmButton : MonoBehaviour
{
    public TMP_InputField cakeNameInputField;

    private Button confirmButton;
    private RecipeEventManager recipeEventManager;

    void Start()
    {
        confirmButton = GetComponent<Button>();
        recipeEventManager = RecipeEventManager.Instance;
    }

    void Update()
    {
        confirmButton.interactable = !string.IsNullOrWhiteSpace(cakeNameInputField.text);
    }

    public void OnClick()
    {
        recipeEventManager.TriggerSaveCake();
    }
}
