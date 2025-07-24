using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

public class ManufactureInputManager : MonoBehaviour
{
  public ManufactureAdmin manufactureAdmin;
  [SerializeField] private ManufactureEventManager eventManager;
  /* Input Keys */
  [SerializeField] private InputAction[] commandKeys; // 커맨드 입력 키
  [SerializeField] private InputAction commandEnterKey; // 커맨드 입력 시작 키
  [SerializeField] private InputAction pasteKey; // 반죽 요청 키(현재 라인에 반죽 생성 요청)
  [SerializeField] private InputAction moveLineKey; // 라인 변경 키
  [SerializeField] private InputAction userClick; // 유저 클릭
  [SerializeField] private InputAction moveScreenKey; // 화면 전환 키

  /* Camera */
  [SerializeField] private Camera manufactureCamera;

  // 시작시 키 바인딩
  void Awake()
  {
    SubscribeInput();

    //일단 임시로 메인카메라로 지정함
    manufactureCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

    ScreenSwapper.OnMoveScreenComplete += OnMoveScreenComplete;
  }

  // 종료시 키 바인딩 해제
  void OnDestroy()
  {
    UnSubscribeInput();
  }

  private void SubscribeInput()
  {
    // 각 커맨드 버튼에 함수 할당
    commandKeys[0].performed += ctx => OnCommandPerformed(recipeArrow.up);
    commandKeys[1].performed += ctx => OnCommandPerformed(recipeArrow.right);
    commandKeys[2].performed += ctx => OnCommandPerformed(recipeArrow.down);
    commandKeys[3].performed += ctx => OnCommandPerformed(recipeArrow.left);
    // 나머지 버튼 할당
    commandEnterKey.performed += OnCommandEnter;
    commandEnterKey.canceled += OnCommandExit;
    pasteKey.performed += OnPaste;
    moveLineKey.performed += OnMoveLine;
    moveScreenKey.performed += OnMoveScreen;
    userClick.canceled += OnUserClick;

    EnableKeys();
  }

  private void UnSubscribeInput()
  {
    commandEnterKey.performed -= OnCommandEnter;
    commandEnterKey.canceled -= OnCommandExit;
    pasteKey.performed -= OnPaste;
    moveLineKey.performed -= OnMoveLine;
    userClick.canceled -= OnUserClick;

    DisableCommandKeys();
    DisableKeys();
  }

  private void EnableKeys()
  {
    commandEnterKey.Enable();
    pasteKey.Enable();
    moveLineKey.Enable();
    moveScreenKey.Enable();
    userClick.Enable();
  }
  private void EnableCommandKeys()
  {
    foreach (InputAction commandKey in commandKeys)
    {
      commandKey.Enable();
    }
  }
  private void DisableKeys()
  {
    commandEnterKey.Disable();
    pasteKey.Disable();
    moveLineKey.Disable();
    moveScreenKey.Disable();
    userClick.Disable();
  }
  private void DisableCommandKeys()
  {
    foreach (InputAction commandKey in commandKeys)
    {
      commandKey.Disable();
    }
  }

  // 커맨드 입력시 방향에 따른 커맨드 입력 이벤트 발생
  private void OnCommandPerformed(recipeArrow direction)
  {
    Debug.Log("ManufactureInputManager : 커맨드 입력 - " + direction);
    eventManager?.TriggerCommandInput(direction);
  }

  // 커맨드 입력 시작시 입력 시작 이벤트 발생
  private void OnCommandEnter(InputAction.CallbackContext ctx)
  {
    // 커맨드 키 활성화
    EnableCommandKeys();
    // 그 외 다른 조작은 비활성화, commandEnterKey는 제외(커맨드 입력 상황에서 벗어나려면 필요함)
    pasteKey.Disable();
    moveLineKey.Disable();
    moveScreenKey.Disable();
    userClick.Disable();

    // commandStart 이벤트 발생
    eventManager?.TriggerCommandStart();
  }
  // 커맨드 입력 종료시 종료 이벤트 발생
  private void OnCommandExit(InputAction.CallbackContext ctx)
  {
    // 커맨드 키 비활성화
    DisableCommandKeys();
    pasteKey.Enable();
    moveLineKey.Enable();
    moveScreenKey.Enable();
    userClick.Enable();
    // commandEnd 이벤트 발생
    eventManager?.TriggerCommandEnd();
  }
  //라인 전환키 클릭시 라인 전환 이벤트 발생
  private void OnMoveLine(InputAction.CallbackContext ctx)
  {
    int lineInput = ctx.ReadValue<float>() > 0f ? 1 : -1; // 방향 구하기
    eventManager?.TriggerLineChange(lineInput);
  }

  // 스크린 전환키 클릭시 스크린 전환 이벤트 발생, 대상 스크린 : 판매
  private void OnMoveScreen(InputAction.CallbackContext ctx)
  {
    eventManager?.TriggerMoveScreen(ScreenNumber.Sales);
  }

  //클릭시 대상의 onClick 실행
  private void OnUserClick(InputAction.CallbackContext ctx)
  {
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

  private void OnPaste(InputAction.CallbackContext ctx)
  {
    eventManager?.TriggerPaste();
  }

  private void OnMoveScreenComplete(ScreenNumber _screenNumber)
  {
    if (_screenNumber == ScreenNumber.Manufacture)
    {
      EnableKeys();
    }
    else
    {
      DisableKeys();
      DisableCommandKeys();
    }
  }

}