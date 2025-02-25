using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 고객이 원하는 상품을 표기
public class CustomerUIHandler : MonoBehaviour{
    public GameObject orderImageObject;
    public TextMeshProUGUI orderQuantity;
    private SpriteRenderer spriteRenderer;

    public void Initialize(Cake _cake, int _orderQuantity){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        // 케이크 이미지, 수량 할당
        orderImageObject.GetComponent<SpriteRenderer>().sprite = _cake.sprite;
        orderQuantity.text = _orderQuantity.ToString();
    }

    public void SetBubbleColor(bool isFirst){
        Color bubbleColor;
        if(isFirst){
            bubbleColor = new Color(1f, 1f, 1f, 1f);
        } else {
            bubbleColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }

        spriteRenderer.color = bubbleColor;
    }
}