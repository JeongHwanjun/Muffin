using UnityEngine;

public class ManufactureLineManager : MonoBehaviour
{
    // 현재 라인 번호
    private int currentLine = 0;
    // 최대 라인 개수
    private int maxLine = 3;
    // 커맨드 입력 가능 여부를 저장하는 배열
    private bool[] lineReady;

    [SerializeField]
    private ManufactureEventManager eventManager;

    void Awake()
    {

    }

    void Start()
    {
        eventManager.OnLineChange += ChangeLine;

    }

    void ChangeLine(int dir)
    {
        int nextLine = currentLine + dir;
        // 범위를 벗어난다면 아무 행위도 하지 않음.
        if (nextLine < 0 ||nextLine >= maxLine) return;

        currentLine = nextLine;
    }

    public bool isLineReady(int lineNumber)
    {
        if(lineNumber < 0  )
        return lineReady[lineNumber];
    }
}
