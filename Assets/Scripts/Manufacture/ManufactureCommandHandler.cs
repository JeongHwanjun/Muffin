using UnityEngine;
using System.Collections.Generic;

public class ManufactureCommandHandler : MonoBehaviour
{
    public ManufactureAdmin manufactureAdmin;
    public ManufactureEventManager manufactureEventManager;
    public UIHandler uiHandler;

    void Awake()
    {
        manufactureEventManager.OnCommandStart += OnCommandStart;
        manufactureEventManager.OnCommandInput += OnCommandInput;
        manufactureEventManager.OnCommandEnd += OnCommandEnd;
    }

    private void OnCommandStart(){
        // 커맨드 입력 활성화 - 라인의 유효성 여부는 EventManager에서 확인함.
        CommandData.instance.InputCommands.Clear();
        // UI 활성화
        uiHandler.gameObject.SetActive(true);
        // CommandData 갱신 -> UI갱신됨
        ValidateCommandArrows();
        // UI갱신을 명시적으로 할 필요도 있을까요?
    }

    private void OnCommandInput(recipeArrow direction){
        // CommandData에 커맨드 추가
        Debug.Log("CommandHandler 입력 인식 : " + direction);
        CommandData.instance.InputCommands.Add(direction);
        ValidateCommandArrows();
    }

    private void OnCommandEnd(){
        // 레시피를 확인 후 성공/실패 이벤트 발생
        Debug.Log("입력된 커맨드 : "+string.Join(", ",CommandData.instance.InputCommands.ToArray()));
            if(CheckRecipe()){
                Debug.Log("정답, 케이크 생산 성공");
                // 성공 이벤트 발생
                manufactureEventManager.TriggerCommandValid();
            }else {
                Debug.Log("오답, 케이크 생산 실패");
                // 실패 이벤트 발생
                manufactureEventManager.TriggerCommandFailed();
            }
            // UI 비활성화
            uiHandler.gameObject.SetActive(false);
            // 커맨드 데이터 초기화
            CommandData.instance.InputCommands.Clear();
    }

    // 화살표 UI에 사용하기 위해 현재 입력과 정답 레시피를 비교함
    // 정답의 개수를 CommandData에 저장 -> CommandData에서 값 변경 -> UIHandler에서 감지 -> UI갱신
    // 실시간으로 비교할 뿐이고 최종적인 정답처리는 따로 있음
    private void ValidateCommandArrows() {
        List<int> RecipeCorrectList = new List<int>();
        List<recipeArrow> InputCommands = CommandData.instance.InputCommands;

        foreach(List<recipeArrow> recipe in CommandData.instance.Recipes){
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

    // 입력 커맨드와 일치하는 레시피가 있는지 확인. OnCommandEnd에서 최종적으로 확인함.
    public bool CheckRecipe(){
        foreach(List<recipeArrow> recipe in CommandData.instance.Recipes){
            if(CompareRecipe(recipe, CommandData.instance.InputCommands)){
                Debug.Log("일치하는 레시피 발견 : "+string.Join(", ",recipe.ToArray()));
                CommandData.instance.InputCommands.Clear();
                return true;
            }
        }
        CommandData.instance.InputCommands.Clear();

        return false;
    }

    // 두 개의 recipeArrow List를 비교하는 함수.
    private bool CompareRecipe(List<recipeArrow> a, List<recipeArrow> b){
        if(a.Count == b.Count){
            for(int i=0;i<a.Count;i++){
                if(a[i] != b[i]) return false;
            }
        }else{
            return false;
        }
        return true;
    }

}
