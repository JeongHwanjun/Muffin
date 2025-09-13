using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ScrollRect_NoDrag : ScrollRect
{
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

    private bool ShouldDrag(PointerEventData eventData)
    {
        // 드래그 대상이 ScrollView의 자식인지 확인
        return eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<DraggableItem>() == null;
    }
}
