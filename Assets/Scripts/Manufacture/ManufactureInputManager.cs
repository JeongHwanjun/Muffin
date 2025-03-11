using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

public class ManufactureInputManager : MonoBehaviour {
  /* Input Keys */
  [SerializeField]
  private ManufactureEventManager eventManager;
  [SerializeField]
  private InputAction commandKeys;
  private Vector2 prevCommandInput;
  [SerializeField]
  private InputAction commandEnterKey;
  [SerializeField]
  private InputAction moveLineKey;
  private int selfLineNumber;
  [SerializeField]
  private InputAction userClick;

  /* Camera */
  [SerializeField]
  private Camera manufactureCamera;

  void Awake()
  {
    SubscribeInput();
  }

  private void SubscribeInput(){
    commandKeys.performed += OnCommandPerformed;
    commandEnterKey.performed += OnCommandEnter;
    moveLineKey.performed += OnMoveLine;
  }

  private void OnCommandPerformed(InputAction.CallbackContext ctx){
    Vector2 currentCommandInput = ctx.ReadValue<Vector2>();
    Vector2 inputDiff = currentCommandInput - prevCommandInput;

    if(inputDiff.magnitude > 0){
      if(prevCommandInput.y <= 0 && currentCommandInput.y > 0) eventManager?.TriggerCommandInput(recipeArrow.up);
      else if(prevCommandInput.y >= 0 && currentCommandInput.y < 0) eventManager?.TriggerCommandInput(recipeArrow.down);
      else if(prevCommandInput.x <= 0 && currentCommandInput.x > 0) eventManager?.TriggerCommandInput(recipeArrow.right);
      else if(prevCommandInput.y >= 0 && currentCommandInput.x < 0) eventManager?.TriggerCommandInput(recipeArrow.left);
    }

    prevCommandInput = currentCommandInput;
  }

  private void OnCommandEnter(InputAction.CallbackContext ctx){
    eventManager?.TriggerCommandStart();
  }

  private void OnMoveLine(InputAction.CallbackContext ctx){
    float lineInput = ctx.ReadValue<Vector2>().x;
    int nextLineNumber = selfLineNumber + lineInput > 0 ? 1 : -1;
    eventManager?.TriggerLineChange(nextLineNumber);
  }

  private void OnUserClick(InputAction.CallbackContext ctx){
    
  }
}