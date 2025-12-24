using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Result : MonoBehaviour
{
    public Timer timer;
    public Counter counter;
    public UIDocument resultUI;
    public VisualElement resultContent;

    // 내용을 담을 템플릿
    public VisualTreeAsset contentTemplate;

    private Button confirmButton;
    private VisualElement root;
    void Start()
    {
        timer.OnTimeout += OnTimeout;
        resultUI = GetComponent<UIDocument>();
        root = resultUI.rootVisualElement;
        root.visible = false;
        resultContent = root.Q<VisualElement>("Contents");
        confirmButton = root.Q<Button>("Confirm");
        Debug.Log("Result : " + confirmButton.clickable);
        confirmButton.clicked += OnConfirm;
    }

    void OnTimeout()
    {
        foreach (var (label, value) in counter.GetStatics())
        {
            VisualElement newContent = contentTemplate.Instantiate();
            newContent.Q<Label>("Name").text = label;
            newContent.Q<Label>("Content").text = value;

            resultContent.Add(newContent);
        }
        root.BringToFront();
        root.visible = true;
    }

    void OnConfirm()
    {
        try
        {
            SceneManager.LoadScene("MainScene");
        }
        catch (Exception e) {
            Debug.LogWarningFormat("Result : error occurred - {0}", e);
        }
    }
}
