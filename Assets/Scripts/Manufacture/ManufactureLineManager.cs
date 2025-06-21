using System.Collections.Generic;
using UnityEngine;

public class ManufactureLineManager : MonoBehaviour
{
    // 현재 라인 번호
    private int currentLine = 0;
    // 최대 라인 개수
    [SerializeField] private int maxLine = 3;
    // 배치할 라인들의 좌우 최대 넓이(LineManager를 중심으로 좌우 +-(totalWidth/2)만큼 퍼짐)
    public int totalWidth = 12;
    // 라인들을 저장하는 리스트
    [SerializeField] private List<Line> lines;
    // 생성할 라인의 프리팹
    public GameObject linePrefab;

    // 이벤트 중계자
    [SerializeField] private ManufactureEventManager manufactureEventManager;
    // 어드민
    [SerializeField] private ManufactureAdmin manufactureAdmin;

    void Awake()
    {
        // 시작시 초기화
        Initialize();
    }

    void Start()
    {
        manufactureEventManager.OnLineChange += ChangeLine;
    }

    // Line들을 maxLine 개수에 맞게 생성하고 초기화함
    void Initialize()
    {
        Debug.Log("ManufactureLineManager : Initialize");
        // 라인생성
        float spacing = maxLine > 1 ? (float)totalWidth / (maxLine - 1) : 0f;
        float startX = -(maxLine - 1) / 2f * spacing;
        for (int i = 0; i < maxLine; i++)
        {
            GameObject newLine = Instantiate(linePrefab, transform);
            newLine.transform.localPosition = new Vector2(startX + spacing * i, 1);
            Line newLineScript = newLine.GetComponent<Line>();
            newLineScript.Initialize(i, manufactureAdmin, manufactureEventManager); // 몇번 라인인지, Admin과 EventManager를 전달해 의존성 주입
            newLineScript.LineChange(false); // 일단 생성 후 선택 해제 - 하나의 라인만 선택되도록
            lines.Add(newLineScript); // 리스트에 추가
        }

        // 시작시 가장 왼쪽 라인이 선택된 상태임. 이에 따라 설정해줌
        lines[0].LineChange(true);
        currentLine = 0;
    }

    // 라인 변경
    void ChangeLine(int dir)
    {
        Debug.Log("ManufactureLineManager : ChangeLine");
        int nextLine = currentLine + dir;
        // 범위를 벗어난다면 아무 행위도 하지 않음.
        if (nextLine < 0 || nextLine >= lines.Count) return;

        // 라인 변경
        lines[currentLine].LineChange(false); // 현재 라인을 선택 해제
        lines[nextLine].LineChange(true); // 다음 라인을 선택 활성화
        currentLine = nextLine; // 다음 라인을 현재 라인으로 변경
    }

    public bool isLineReady()
    {
        Debug.Log("ManufactureLineManager : isLineReady");
        if (currentLine < 0 || currentLine >= lines.Count) return false;

        return lines[currentLine].isLineReady;
    }
}
