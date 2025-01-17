using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class DecorationTable : MonoBehaviour {
    public LineEventManager lineEventManager;
    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);
        lineEventManager.TriggerSheetCollision();
        // 충돌 이벤트 발생
    }
}
