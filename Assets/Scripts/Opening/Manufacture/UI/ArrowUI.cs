using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

class ArrowUI : MonoBehaviour
{
  [SerializeField] private GameObject arrowPrefab;
  [SerializeField] private Sprite CommonArrow, FailArrow, CorrectArrow; // 각각 일반, 오답, 정답 화살표 스프라이트.
  List<GameObject> arrows;
  List<Image> arrowImages;
  private CommandData commandData;
  private int correctArrowCount; // 입력 List 중 정답의 개수
  private int currentArrowCount; // 입력 List의 길이
  private int index; // 내가 몇번째 recipe인가?


  public void Initialize(List<recipeArrow> recipe, int index)
  {
    this.index = index;
    arrows = new();
    arrowImages = new();
    foreach (recipeArrow direction in recipe)
    {
      GameObject newArrow = Instantiate(arrowPrefab, transform);
      newArrow.transform.Rotate(Vector3.forward * ((int)direction + 1) * -90);
      arrows.Add(newArrow);
      arrowImages.Add(newArrow.GetComponent<Image>());
    }
  }

  void Start()
  {
    commandData = CommandData.instance;
    commandData.PropertyChanged += SetData;
  }

  private void RefreshUI()
  {
    int count = 0; // 현재 몇번째 화살표를 보고 있는지 기록
    foreach (Image arrowImage in arrowImages)
    {
      if (currentArrowCount != correctArrowCount)
      {
        arrowImage.sprite = FailArrow;
        continue;
      }

      // 현재까지 모두 정답일 경우
      if (count < currentArrowCount) // 입력한 부분까지 정답 처리
      {
        arrowImage.sprite = CorrectArrow;
      }
      else // 다음 부분부터 일반 처리
      {
        arrowImage.sprite = CommonArrow;
      }
      count++;
    }
  }
  
  private void SetData(object sender, PropertyChangedEventArgs e)
  {
    currentArrowCount = commandData.InputCommands.Count;
    correctArrowCount = commandData.RecipeCorrectList[index];

    RefreshUI();
  }
}