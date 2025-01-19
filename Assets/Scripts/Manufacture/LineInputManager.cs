using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
    생산 화면의 입력을 총괄하는 스크립트

    입력 : 마우스 클릭, Stdio
    출력 : 각종 행동들
*/
public class LineInputManager : MonoBehaviour
{
    public InputAction UserClick; // 왼클릭
    public InputAction commandEnter; // 커맨드 입력 시작
    public InputAction[] commandArrows; // 커맨드 방향들
    public InputAction[] lineChangers;

    public LineEventManager lineEventManager;
    [SerializeField]
    private ManufactureAdmin manufactureAdmin;
    private Line line;

    private enum stateTable {Idle, Waiting, Receiving}; // 현재 상태 enum
    private stateTable state = stateTable.Idle; // 현재 상태
    private int selfLineNumber = 0, maxLineNumber = 3;
    
    void Awake()
    {
        // 다른 오브젝트에 의존하지 않고 초기화 할 수 있는 놈들
        // 입력 할당만 함, LineNumber 확인 후 올바른 라인만 입력 활성화
        UserClick.canceled += OnClickOut;
        commandEnter.performed += OnCommandEnter;
        commandEnter.canceled += OnCommandExit;

        for(int i = 0; i < commandArrows.Length; i++){
            int index = i;
            commandArrows[index].performed += ctx => OnCommandArrowsPerformed(index);
        }
        for(int i = 0; i < lineChangers.Length; i++){
            int index=i;
            lineChangers[index].performed += ctx => ChangeLine(index);
        }

        // 이벤트 연결
        lineEventManager.OnSheetCollision += OnSheetCollision;
    }
    public void InitializeDependingObjects(ManufactureAdmin _manufactureAdmin) {
        // 다른 오브젝트가 확실히 초기화 되어야 하는 놈들. ManufactureAdmin에서 이를 보장하고 실행한다.
        line= GetComponentInParent<Line>();
        selfLineNumber = line.LineNumber;

        manufactureAdmin = _manufactureAdmin;
        maxLineNumber = manufactureAdmin.maxLine;

        // manufactureAdmin 이벤트 연결
        manufactureAdmin.SwitchLine += OnSwitchLine;
    }

    private void OnDestroy() {
        // 입력 할당 해제
        UserClick.canceled -= OnClickOut;

        commandEnter.performed -= OnCommandEnter;
        commandEnter.canceled -= OnCommandExit;

        for(int i = 0; i < commandArrows.Length; i++){
            int index = i;
            commandArrows[index].performed -= ctx => OnCommandArrowsPerformed(index);
        }
        for(int i = 0; i < lineChangers.Length; i++){
            int index=i;
            lineChangers[index].performed -= ctx => ChangeLine(index);
        }
        // 입력 비활성화
        SwitchCommandInput(false);
        SwitchLineChangeInput(false);

        // 이벤트 연결 해제
        lineEventManager.OnSheetCollision -= OnSheetCollision;
        manufactureAdmin.SwitchLine -= OnSwitchLine;
    }

    private void OnSheetCollision(){
        if(state == stateTable.Idle){
            state = stateTable.Waiting;
        }
    }
    private void OnCommandEnter(InputAction.CallbackContext ctx) {
        if (state == stateTable.Waiting) {
            state = stateTable.Receiving;
            CommandData.instance.InputCommands.Clear();
            // 커맨드 입력 활성화, 라인 변경 비활성화
            SwitchCommandInput(true);
            SwitchLineChangeInput(false);

            // UI 활성화
            lineEventManager.UISetActive(true);
            // 초기 상태로 UI 갱신
            ValidateCommandArrows();
        }
    }
    private void OnCommandExit(InputAction.CallbackContext ctx) {
        if (state == stateTable.Receiving) {
            Debug.Log("입력된 커맨드 : "+string.Join(", ",CommandData.instance.InputCommands.ToArray()));
            if(CheckRecipe()){
                state = stateTable.Idle; // 상태 초기화
                Debug.Log("정답, 케이크 생산 성공");
            }else {
                state = stateTable.Waiting; // 계속 입력 가능한 상태
                Debug.Log("오답, 케이크 생산 실패");
            }
            CommandData.instance.InputCommands.Clear();
            // 커맨드 입력 비활성화, 라인 변경 활성화
            SwitchCommandInput(false);
            SwitchLineChangeInput(true);
            //UI 비활성화
            lineEventManager.UISetActive(false);
        }
    }

    private void OnCommandArrowsPerformed(int index){
        if(state == stateTable.Receiving){
            CommandData.instance.InputCommands.Add(index);
            ValidateCommandArrows();
        }
    }

    void OnClickOut(InputAction.CallbackContext ctx){
        // 클릭 위치 확인
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        // 물체를 클릭했다면
        if (hit.collider != null)
        {
            // 클릭된 오브젝트에서 ClickableThing을 가져옴
            ClickableThing clickable = hit.collider.GetComponent<ClickableThing>();
            if (clickable != null)
            {
                clickable.OnClick();
            }
        }
    }

    // 각 레시피와 현재 입력 커맨드를 비교해 정답의 개수를 반환, 화살표 UI에 활용
    private void ValidateCommandArrows() {
        List<int> RecipeCorrectList = new List<int>();
        List<int> InputCommands = CommandData.instance.InputCommands;

        foreach(List<int> recipe in CommandData.instance.Recipes){
            int correctCount = 0;
            if(InputCommands.Count <= recipe.Count){
                // 입력 커맨드가 정답 커맨드보다 짧거나 같은 때만 확인
                for(int i=0;i<InputCommands.Count;i++){
                    if(InputCommands[i] == recipe[i]){
                        // 입력 커맨드와 정답 커맨드가 같다면 counter++
                        correctCount++;
                    }
                }
            }
            
            RecipeCorrectList.Add(correctCount);
        }

        CommandData.instance.RecipeCorrectList = RecipeCorrectList;
    }

    // 커맨드 입력 종료시 일치하는 레시피가 있는지 확인.
    public bool CheckRecipe(){
        foreach(List<int> recipe in CommandData.instance.Recipes){
            if(CompareList(recipe, CommandData.instance.InputCommands)){
                Debug.Log("일치하는 레시피 발견 : "+string.Join(", ",recipe.ToArray()));
                CommandData.instance.InputCommands.Clear();
                // UI비활성화
                lineEventManager.UISetActive(false);
                return true;
            }
        }
        CommandData.instance.InputCommands.Clear();
        lineEventManager.UISetActive(false);

        return false;
    }

    private bool CompareList(List<int> a, List<int> b){
        if(a.Count == b.Count){
            for(int i=0;i<a.Count;i++){
                if(a[i] != b[i]) return false;
            }
        }else{
            return false;
        }
        return true;
    }

    private void OnSwitchLine(int lineNumber){
        // 주어진 lineNumber가 내 LineNumber와 같은지 비교
        bool turnOn = lineNumber == selfLineNumber;
        // 자신이 선택 라인이라면 커맨드 입력 활성화, 기본적으로 LineChange도 가능하도록 설정
        if(turnOn){
            UserClick.Enable();
            commandEnter.Enable();
            lineEventManager.CharacterSetActive(true);
        } else {
            UserClick.Disable();
            commandEnter.Disable();
            lineEventManager.CharacterSetActive(false);
        }
        SwitchLineChangeInput(turnOn);
        SwitchCommandInput(false); // 커맨드 입력은 기본적으로 false - commandEnter를 눌러야 활성화
    }

    private void SwitchCommandInput(bool ON){
        if(ON){
            for(int i = 0; i < commandArrows.Length; i++){
                int index = i;
                commandArrows[index].Enable();
            }
        } else {
            for(int i = 0; i < commandArrows.Length; i++){
                int index = i;
                commandArrows[index].Disable();
            }
        }
    }
    private void SwitchLineChangeInput(bool ON){
        if(ON){
            Debug.Log(selfLineNumber + "번 라인 : LineChange 활성화");
            for(int i = 0; i < lineChangers.Length; i++){
                int index=i;
                lineChangers[index].Enable();
            }
        } else {
            Debug.Log(selfLineNumber + "번 라인 : LineChange 비활성화");
            for(int i = 0; i < lineChangers.Length; i++){
                int index=i;
                lineChangers[index].Disable();
            }
        }
    }

    private void ChangeLine(int index){
        // index에 따라 0이면 좌측, 1이면 우측
        Debug.Log("라인체인저 작동");
        int nextLine = selfLineNumber + (index == 1 ? 1 : -1);
        manufactureAdmin.TriggerSwitchLine(nextLine);
    }
}
