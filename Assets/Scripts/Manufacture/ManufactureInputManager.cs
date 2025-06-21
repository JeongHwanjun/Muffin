using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

public class ManufactureInputManager : MonoBehaviour {
  public ManufactureAdmin manufactureAdmin;
  /* Input Keys */
  [SerializeField]
  private ManufactureEventManager eventManager;
  [SerializeField]
  private InputAction[] commandKeys;
  [SerializeField]
  private InputAction commandEnterKey;
  [SerializeField]
  private InputAction moveLineKey;
  [SerializeField]
  private InputAction userClick;
  [SerializeField]
  private InputAction moveScreenKey;  

  /* Camera */
  [SerializeField]
  private Camera manufactureCamera;

  // 시작시 키 바인딩
  void Awake()
  {
    SubscribeInput();

    //일단 임시로 메인카메라로 지정함
    manufactureCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
  }

  // 종료시 키 바인딩 해제
  void OnDestroy()
  {
    UnSubscribeInput();
  }

  private void SubscribeInput(){
    // 각 커맨드 버튼에 함수 할당
    commandKeys[0].performed += ctx => OnCommandPerformed(recipeArrow.up);
    commandKeys[1].performed += ctx => OnCommandPerformed(recipeArrow.right);
    commandKeys[2].performed += ctx => OnCommandPerformed(recipeArrow.down);
    commandKeys[3].performed += ctx => OnCommandPerformed(recipeArrow.left);
    // 나머지 버튼 할당
    commandEnterKey.performed += OnCommandEnter;
    commandEnterKey.canceled += OnCommandExit;
    moveLineKey.performed += OnMoveLine;
    moveScreenKey.performed += OnMoveScreen;
    userClick.canceled += OnUserClick;

    commandEnterKey.Enable();
    moveLineKey.Enable();
    moveScreenKey.Enable();
    userClick.Enable();
  }

  private void UnSubscribeInput(){
    commandEnterKey.performed -= OnCommandEnter;
    commandEnterKey.canceled -= OnCommandExit;
    moveLineKey.performed -= OnMoveLine;
    userClick.canceled -= OnUserClick;

    for(int i=0;i<commandKeys.Length;i++) commandKeys[i].Disable();
    commandEnterKey.Disable();
    moveLineKey.Disable();
    moveScreenKey.Disable();
    userClick.Disable();
  }

  // 커맨드 입력시 방향에 따른 커맨드 입력 이벤트 발생
  private void OnCommandPerformed(recipeArrow direction){
    eventManager?.TriggerCommandInput(direction);
  }

  // 커맨드 입력 시작시 입력 시작 이벤트 발생
  private void OnCommandEnter(InputAction.CallbackContext ctx){
    for(int i=0;i<commandKeys.Length;i++) commandKeys[i].Enable();
    moveLineKey.Disable();
    moveScreenKey.Disable();
    eventManager?.TriggerCommandStart();
  }
  private void OnCommandExit(InputAction.CallbackContext ctx){
    for(int i=0;i<commandKeys.Length;i++) commandKeys[i].Disable();
    moveLineKey.Enable();
    moveScreenKey.Enable();
    eventManager?.TriggerCommandEnd();
  }

  //라인 전환키 클릭시 라인 전환 이벤트 발생
  private void OnMoveLine(InputAction.CallbackContext ctx){
    int lineInput = ctx.ReadValue<float>() > 0f ? 1 : -1; // 방향 구하기
    eventManager?.TriggerLineChange(lineInput);
  }

  // 스크린 전환키 클릭시 스크린 전환 이벤트 발생, 대상 스크린 : 판매
  private void OnMoveScreen(InputAction.CallbackContext ctx){
    eventManager?.TriggerMoveScreen(ScreenNumber.Sales);
  }

  //클릭시 대상의 onClick 실행
  private void OnUserClick(InputAction.CallbackContext ctx){
    Vector2 mousePosition = manufactureCamera.ScreenToWorldPoint(Input.mousePosition);
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
}