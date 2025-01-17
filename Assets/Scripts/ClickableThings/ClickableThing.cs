using UnityEngine;
using UnityEngine.InputSystem;

/*
    클릭 가능한 오브젝트의 원형
*/
public abstract class ClickableThing : BaseMonoBehaviour
{
    public abstract void OnClick();
}
