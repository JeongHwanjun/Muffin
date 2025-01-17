using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManufactureAdmin : MonoBehaviour
{
    public GameObject LinePrefab;
    private List<GameObject> Line = new List<GameObject>();
    public int maxLine = 3;
    public int totalWidth = 12; // 배치할 라인의 기준 넓이
    public List<List<int>> recipes = new List<List<int>>{
        new List<int> { 0, 1, 2, 3 },
        new List<int> { 2, 3, 0, 1, 2 } // 하 좌 상 우 하
    };

    [SerializeField]
    private int ingredients = 0;

    public event Action<int> SwitchLine;
    public event Action<int> ConsumeIngredient;

    private void Awake() {
        float spacing = maxLine > 1 ? (float)totalWidth/(maxLine - 1) : 0f;
        float startX = -(maxLine-1)/2f * spacing;
        for(int i=0;i<maxLine;i++){
            GameObject newLine = Instantiate(LinePrefab, transform);
            newLine.transform.localPosition = new Vector2(startX + spacing * i, 1);
            newLine.GetComponent<Line>().Init(i);
            newLine.GetComponentInChildren<LineInputManager>().InitializeDependingObjects();
            Line.Add(newLine);
        }
        maxLine = Line.Count;
    }
    void Start()
    {
        CommandData.instance.Recipes = recipes;
        TriggerSwitchLine(0);
    }

    public void Initialize(int ingredients, int lineNumbers){
        // 상위 객체(총괄)에서 데이터를 받아와 이에 맞게 화면 초기화
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerSwitchLine(int _lineNumber){
        int lineNumber = Mathf.Clamp(_lineNumber, 0, maxLine - 1);
        SwitchLine?.Invoke(lineNumber);
    }

    public void TriggerConsumeIngredient(int usage, int lineNumber){
        int nextIngredients = ingredients - usage;
        if(nextIngredients < 0){
            Debug.Log($"TriggerConsumeIngredient 실패 : 재료부족({nextIngredients})");
        } else {
            ingredients = nextIngredients;
            ConsumeIngredient?.Invoke(lineNumber);
        }
    }
}
