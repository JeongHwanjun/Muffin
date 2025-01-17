using UnityEngine;
/*
    MonoBehaviour에서 Unity 이벤트 함수들을 상속해 재정의 하기 위한 기반 클래스
    사용할 이벤트를 protected virtual로 선언해 override 할 수 있게 함.
*/
public abstract class BaseMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake() {}
    protected virtual void OnEnable() {}
    protected virtual void Start() {}
    protected virtual void OnTriggerEnter2D(Collider2D other) {}
    protected virtual void OnTriggerStay(Collider other) {}
    protected virtual void OnTriggerExit2D(Collider2D other) {}
    protected virtual void OnCollisionEnter2D(Collision2D other) {}
    protected virtual void OnCollisionStay2D(Collision2D other) {}
    protected virtual void OnCollisionExit2D(Collision2D other) {}
    protected virtual void Update() {}
    protected virtual void FixedUpdate() {}
    protected virtual void OnDisable() {}
    protected virtual void OnDestroy() {}
}