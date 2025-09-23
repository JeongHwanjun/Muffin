using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ScrollRect_NoDrag : ScrollRect
{
    public float wheelScrollSpeed = 0.05f;
    public override void OnInitializePotentialDrag(PointerEventData eventData)
    {
        //if (!ShouldDrag(eventData)) return;
        //base.OnInitializePotentialDrag(eventData);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        //if (!ShouldDrag(eventData)) return;
        //base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        //if (!ShouldDrag(eventData)) return;
        //base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        //if (!ShouldDrag(eventData)) return;
        //base.OnEndDrag(eventData);
    }

    public override void OnScroll(PointerEventData data)
    {
        //base.OnScroll(data); // 아무것도 안함 : Update에서 스크롤 따로 처리
    }

    private bool ShouldDrag(PointerEventData eventData)
    {
        // 드래그 대상이 ScrollView의 자식인지 확인
        return eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<DraggableItem>() == null;
    }

    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scroll) > 0.01f)
        {
            verticalNormalizedPosition += scroll * wheelScrollSpeed;
        }
    }
}
