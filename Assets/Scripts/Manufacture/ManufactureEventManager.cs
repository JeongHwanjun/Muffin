using System;
using UnityEngine;

/*
  생산 화면에서 객체 간의 상호작용을 중계하는 클래스
  구독할 수 있는 이벤트와 작동시킬 트리거를 제공하고, 전달되는 값의 가공도 담당한다.
*/
public class ManufactureEventManager : MonoBehaviour {
  /* Events */
  public event Action OnCommandStart; // 커맨드 입력 시작
  public event Action<recipeArrow> OnCommandInput; // 커맨드 입력
  public event Action OnCommandEnd; // 커맨드 입력 종료
  public event Action<int> OnLineChange; // 라인 전환
  public event Action<ScreenNumber> OnScreenSwap; // 화면 전환
  public event Action OnCommandValid; // 커맨드 판정 후 정답일 시 발생
  public event Action OnCommandFailed; // 커맨드 판정 후 오답일 시 발생

  /* Triggers */
  public void TriggerCommandStart(){
    OnCommandStart?.Invoke();
  }

  public void TriggerCommandInput(recipeArrow recipeArrow){
    OnCommandInput?.Invoke(recipeArrow);
  }

  public void TriggerCommandEnd(){
    OnCommandEnd?.Invoke();
  }

  public void TriggerLineChange(int nextLine){
    // 입력받는 라인을 변경하는 것은 InputManager에서 자체적으로. 여기선 캐릭터 배치 등을 변경한다.
    OnLineChange?.Invoke(nextLine);
  }
  
  public void TriggerScreenSwap(ScreenNumber screenNumber){
    OnScreenSwap?.Invoke(screenNumber);
  }

  public void TriggerCommandValid(){
    OnCommandValid?.Invoke();
  }

  public void TriggerCommandFailed(){
    OnCommandFailed?.Invoke();
  }

}