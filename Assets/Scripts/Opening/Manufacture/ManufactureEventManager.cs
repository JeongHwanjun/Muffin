using System;
using UnityEngine;

/*
  생산 화면에서 객체 간의 상호작용을 중계하는 클래스
  구독할 수 있는 이벤트와 작동시킬 트리거를 제공하고, 전달되는 값의 가공도 담당한다.
*/
public class ManufactureEventManager : MonoBehaviour {
  public static ManufactureEventManager Instance;
  /* Events */
  public event Action OnCommandStart; // 커맨드 입력 시작
  public event Action<recipeArrow> OnCommandInput; // 커맨드 입력
  public event Action OnCommandEnd; // 커맨드 입력 종료
  public event Action OnShowRecipe; // 레시피UI 공개
  public event Action OnHideRecipe; // 레시피UI 비공개
  public event Action OnPaste; // Paste 요청
  public event Action<int> OnLineChange; // 라인 전환
  public event Action<ScreenNumber> OnMoveScreen; // 화면 전환
  public event Action<int> OnCommandValid; // 커맨드 판정 후 정답일 시 발생
  public event Action OnCommandFailed; // 커맨드 판정 후 오답일 시 발생

  void Awake()
  {
    Instance = this;
  }

  /* Triggers */
  public void TriggerCommandStart(){
    Debug.Log("ManufactureEventManager : TriggerCommandStart");
    // 유효성 검사는 CommandHandler에서 실시
    OnCommandStart?.Invoke();
  }

  public void TriggerCommandInput(recipeArrow recipeArrow){
    Debug.Log("ManufactureEventManager : TriggerCommandInput " + recipeArrow.ToString());
    OnCommandInput?.Invoke(recipeArrow);
  }

  public void TriggerCommandEnd()
  {
    Debug.Log("ManufactureEventManager : TriggerCommandEnd");
    OnCommandEnd?.Invoke();
  }

  public void TriggerShowRecipe()
  {
    Debug.Log("ManufactureEventManager : TriggerShowRecipe");
    OnShowRecipe?.Invoke();
  }
  
  public void TriggerHideRecipe()
  {
    Debug.Log("ManufactureEventManager : TriggerHideRecipe");
    OnHideRecipe?.Invoke();
  }

  public void TriggerPaste(){
    OnPaste?.Invoke();
  }

  public void TriggerLineChange(int nextLine)
  {
    Debug.Log("ManufactureEventManager : TriggerLineChange " + nextLine.ToString());
    // 다음 라인이 몇번인지 알림
    OnLineChange?.Invoke(nextLine);
  }
  
  public void TriggerMoveScreen(ScreenNumber screenNumber){
    Debug.Log("ManufactureEventManager : TriggerScreenSwap");
    OnMoveScreen?.Invoke(screenNumber);
  }

  public void TriggerCommandValid(int cakeIndex){
    Debug.Log("ManufactureEventManager : TriggerCommandValid");
    OnCommandValid?.Invoke(cakeIndex);
  }

  public void TriggerCommandFailed(){
    Debug.Log("ManufactureEventManager : TriggerCommandFailed");
    OnCommandFailed?.Invoke();
  }

}