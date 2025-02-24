using UnityEngine;

// 고객이 원하는 상품을 표기
public class CustomerUIHandler : MonoBehaviour{
    public GameObject orderImage;
    private SpriteRenderer spriteRenderer;

    public void Initialize(Cake _cake){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        // 케이크 이미지 할당
        orderImage.GetComponent<SpriteRenderer>().sprite = _cake.sprite;
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