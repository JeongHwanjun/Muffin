using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class DecorationTable : MonoBehaviour {
    public Line line;

    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        line.switchLineReady();
        Destroy(other.gameObject);
    }
}
