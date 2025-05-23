using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SalesUIHandler : MonoBehaviour
{
    private VisualElement root, cardContainer;
    private List<TemplateContainer> cards;

    void Initialize(){
        root = GetComponent<UIDocument>().rootVisualElement;
        cardContainer = root.Q<VisualElement>("Cards");
        Debug.Log("cardContainer : " + cardContainer);
        cards = cardContainer.Query<TemplateContainer>().Where(el => el.name.StartsWith("Card")).ToList();
        Debug.Log("cards : " + cards);
    }

    void OnEnable()
    {
        Initialize();
    }

    public void OnDataChanged(List<Cake> _cakes){
        // 케이크 정보 변경(수량, 스프라이트, 가격 등 뭐든)시 호출되도록
        if(root == null || cardContainer == null || cards == null) Initialize();
        Debug.Log("UI 갱신 시도");
        int index = 0;
        foreach(TemplateContainer card in cards){
            VisualElement cakeImage = card.Q<VisualElement>("Image");
            Label cakeName = card.Q<Label>("Name");
            Label cakeQuantity = card.Q<Label>("Quantity");
            
            cakeImage.style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(_cakes[index].spritePath));
            cakeName.text = _cakes[index].name;
            cakeQuantity.text = _cakes[index].quantity.ToString();
            index++;
        }
    }

    public void SetUI(bool ON){
        if (!ON) {
            root = null;
            cardContainer = null;
            cards = null; // UI가 닫힐 때 캐시 초기화
        }
        gameObject.SetActive(ON);
    }
}
