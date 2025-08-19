using UnityEngine;
using UnityEngine.UI;

public class ListIcon : MonoBehaviour
{
    public Button iconButton;         // 클릭용 아이콘 버튼
    public GameObject dropdownPanel;  // 항목을 포함하는 패널

    void Start()
    {
        iconButton = GetComponent<Button>();
        SetPanelActive(false);

        iconButton.onClick.AddListener(() =>
        {
            bool isActive = dropdownPanel.activeSelf;
            SetPanelActive(!isActive);
            transform.parent.transform.SetAsLastSibling(); // 아이콘 전체를 flour의 마지막 자식으로 만듬 - UI가 다른 UI 밑에 묻히는걸 방지함
        });
    }

    public void SetPanelActive(bool value){ dropdownPanel.SetActive(value); }
}
