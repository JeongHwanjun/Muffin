using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
    생산 라인의 이벤트를 관리하는 스크립트
*/
public class LineEventManager : MonoBehaviour
{

    public GameObject ui;
    public GameObject character;
    public event Action OnSheetCollison;

    private void Awake() {

    }

    public void TriggerSheetCollision(){
        OnSheetCollison?.Invoke();
    }
    public void UISetActive(bool active){
        ui.SetActive(active);
    }
    public void CharacterSetActive(bool active){
        character.SetActive(active);
    }
}