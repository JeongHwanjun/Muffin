using System;
using System.Collections.Generic;
using UnityEngine;

public class ManufactureAdmin : MonoBehaviour
{
    public GameObject LinePrefab;
    private List<GameObject> Line = new List<GameObject>();
    public OpeningTimeManager openingTimeManager;
    public int maxLine = 3;
    public int totalWidth = 12; // 배치할 라인의 기준 넓이
    public List<List<int>> recipes = new List<List<int>>{
        new List<int> { 0, 1, 2, 3 },
        new List<int> { 2, 3, 0, 1, 2 } // 하 좌 상 우 하
    };

    public event Action<int> SwitchLine;
    public event Action<int> ConsumeIngredient;
    public static event Action<ManufactureAdmin> OnManufactureAdminReady;
    public event Action<ScreenNumber> OnSwapScreen;

    private void Awake() {
        
    }

    public void Initialize(int _maxLine, OpeningTimeManager _openingTimeManager){
        // 상위 객체(OpeningTimeManager)에서 데이터를 받아와 이에 맞게 화면 초기화
        maxLine = _maxLine;
        openingTimeManager = _openingTimeManager;

        float spacing = maxLine > 1 ? (float)totalWidth/(maxLine - 1) : 0f;
        float startX = -(maxLine-1)/2f * spacing;
        for(int i=0;i<maxLine;i++){
            GameObject newLine = Instantiate(LinePrefab, transform);
            newLine.transform.localPosition = new Vector2(startX + spacing * i, 1);
            newLine.GetComponent<Line>().Init(i, this); // 몇번 라인인지, manufactureAdmin을 전달해 의존성 주입
            newLine.GetComponentInChildren<LineInputManager>().InitializeDependingObjects(this);
            Line.Add(newLine);
        }

        CommandData.instance.Recipes = recipes;
        TriggerSwitchLine(0);

        // ScreenSwapper에 준비됐다고 알림
        OnManufactureAdminReady?.Invoke(this);
    }

    public void TriggerSwitchLine(int _lineNumber){
        int lineNumber = Mathf.Clamp(_lineNumber, 0, maxLine);
        SwitchLine?.Invoke(lineNumber);
    }

    public void TriggerConsumeIngredient(int usage, int lineNumber){
        openingTimeManager.UpdateIngredient("Flour", usage);
        // 추후 재료명, 소비량을 변수로 만들기

        // 해당하는 라인에서 sheet 생성
        ConsumeIngredient?.Invoke(lineNumber);
    }

    public void TriggerSwapScreen(ScreenNumber _screenNumber){ // 스크린 전환 키를 입력받아 전환을 시작하는 함수
        Debug.Log("TriggerSwapScreen of ManufactureAdmin");
        OnSwapScreen?.Invoke(_screenNumber);
    }
}
