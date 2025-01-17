using UnityEngine;

public class Sheet : MonoBehaviour
{
    public float speed = 1.0f;

    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        // 아래쪽으로 이동
        rb.linearVelocity = Vector2.down;
    }
}
