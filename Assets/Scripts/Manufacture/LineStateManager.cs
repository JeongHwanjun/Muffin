using UnityEngine;

public class LineStateManager : MonoBehaviour
{
    public static LineStateManager instance;

    public ManufactureEventManager manufactureEventManager;

    private int currentLine = 0;
    public int CurrentLine => currentLine;


    private void Awake() {
        instance = this;
    }

    public void SetLine(int newLine) {
        if (currentLine == newLine) return;
        currentLine = newLine;
        
    }
}