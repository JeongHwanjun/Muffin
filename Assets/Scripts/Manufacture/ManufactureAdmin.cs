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
    public List<List<recipeArrow>> recipes = new List<List<recipeArrow>>();

    public static event Action<ManufactureAdmin> OnManufactureAdminReady;
    public event Action<ScreenNumber> OnSwapScreen;

    public void Initialize(int _maxLine, OpeningTimeManager _openingTimeManager){
        // 상위 객체(OpeningTimeManager)에서 데이터를 받아와 이에 맞게 화면 초기화
        maxLine = _maxLine;
        openingTimeManager = _openingTimeManager;

        // 라인생성
        float spacing = maxLine > 1 ? (float)totalWidth/(maxLine - 1) : 0f;
        float startX = -(maxLine-1)/2f * spacing;
        for(int i=0;i<maxLine;i++){
            GameObject newLine = Instantiate(LinePrefab, transform);
            newLine.transform.localPosition = new Vector2(startX + spacing * i, 1);
            newLine.GetComponent<Line>().Init(i, this); // 몇번 라인인지, manufactureAdmin을 전달해 의존성 주입
            Line.Add(newLine);
        }

        CommandData.instance.Recipes = recipes;

        // 부모에게서 cake의 recipe 데이터를 받아옴
        List<Cake> cakes = openingTimeManager.GetCakes();
        //Debug.Log(cakes.Count);
        foreach(Cake cake in cakes){
            recipes.Add(cake.recipe);
        }

        // ScreenSwapper에 준비됐다고 알림
        OnManufactureAdminReady?.Invoke(this);
    }

    public void TriggerSwapScreen(ScreenNumber _screenNumber){ // 스크린 전환 키를 입력받아 전환을 시작하는 함수
        Debug.Log("TriggerSwapScreen of ManufactureAdmin");
        OnSwapScreen?.Invoke(_screenNumber);
    }

    // Line이 준비됐는지 확인하는 함수. EventManager에서 사용
    public bool isLineReady(int lineNumber){
        // 근데 GetComponent가 느린데 이거 맞나? 추후에 성능개선 요함.
        return Line[lineNumber].GetComponent<Line>().isLineReady;
    }
}
