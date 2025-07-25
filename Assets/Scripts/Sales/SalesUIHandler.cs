using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SalesUIHandler : MonoBehaviour
{
    [SerializeField] private CakeManager cakeManager;
    [SerializeField] private SalesAdmin salesAdmin;
    private VisualElement root, cardContainer;
    private List<TemplateContainer> cards;

    public void Initialize()
    {
        Debug.Log("SalesUIHandler : Initialize");
        root = GetComponent<UIDocument>().rootVisualElement;
        cardContainer = root.Q<VisualElement>("Cards");
        Debug.Log("SalesUIHandler : cardContainer : " + cardContainer);
        cards = cardContainer.Query<TemplateContainer>().Where(el => el.name.StartsWith("Card")).ToList();
        Debug.Log("SalesUIHandler : cards : " + cards);

        cakeManager.OnCakeDataChanged += OnDataChanged;
    }

    void Awake()
    {
        Initialize();
    }

    public void OnDataChanged(int cakeIndex)
    {
        // 케이크 정보 변경(수량, 스프라이트, 가격 등 뭐든)시 호출되도록
        if (root == null || cardContainer == null || cards == null) Initialize();
        Debug.Log("SalesUIHandler : UI 갱신 시도");
        List<Cake> cakes = salesAdmin.GetCakes();
        int index = 0;
        foreach (TemplateContainer card in cards)
        {
            VisualElement cakeImage = card.Q<VisualElement>("Image");
            Label cakeName = card.Q<Label>("Name");
            Label cakeQuantity = card.Q<Label>("Quantity");

            cakeImage.style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(cakes[index].spritePath));
            cakeName.text = cakes[index].name;
            cakeQuantity.text = cakes[index].quantity.ToString();
            index++;
        }
    }

    public void SetUI(bool ON)
    {
        root.visible = ON;
    }

    void OnDisable()
    {
        cakeManager.OnCakeDataChanged -= OnDataChanged;
    }
}
