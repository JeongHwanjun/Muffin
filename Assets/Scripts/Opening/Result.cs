using UnityEngine;
using UnityEngine.UIElements;

public class Result : MonoBehaviour
{
    public Timer timer;
    public Counter counter;
    public UIDocument resultUI;
    public VisualElement resultContent;

    // 내용을 담을 템플릿
    public VisualTreeAsset contentTemplate;

    private VisualElement root;
    void Start()
    {
        timer.OnTimeout += OnTimeout;
        resultUI = GetComponent<UIDocument>();
        root = resultUI.rootVisualElement;
        root.visible = false;
        resultContent = root.Q<VisualElement>("Contents");
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
        root.visible = true;
    }
}
