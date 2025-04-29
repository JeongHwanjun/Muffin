using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class DecorationTable : MonoBehaviour {
    public Line line;
    private void OnTriggerEnter2D(Collider2D other) {
        line.lineReady();
        Destroy(other.gameObject);
    }
}
