using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum recipeArrow {
  up = 0,
  right = 1,
  down = 2,
  left = 3
}

public class CakeManager : MonoBehaviour
{
  public CakeCollection cakeCollection;
  public List<Cake> cakes = new List<Cake>();// cakes는 직접접근 X - OpeningTimeData를 통해 접근할 것

  // 데이터 변경시 이벤트 발생
  public event Action<int> OnCakeDataChanged;


  private void Awake()
  {
    // Edit - ProjectSettings - Script Execution Order에서 순서를 앞으로 당겨서 먼저 실행될 수 있도록 해야 함.
    InitializeCakes();
  }

  // 케이크 초기화
  private void InitializeCakes() // Cake의 정보는 json파일로 주어짐
  {
    // Cake 정보 불러오기
    CakeCollection cakes_origin = Instantiate(cakeCollection);
    string path = Application.persistentDataPath + "/cakeData.json";
    if (File.Exists(path))
    { // 파일이 있다면 불러옵니다
      string json = File.ReadAllText(path);
      CakeList cakeList = JsonUtility.FromJson<CakeList>(json);
      cakes = cakeList.cakes;
      Debug.Log("파일 읽기 완료 : " + path);
    }
    else
    { // 파일이 없다면 cakes_origin에서 그냥 불러오고, 파일을 생성합니다.
      cakes.Clear();
      cakes = new List<Cake>(cakes_origin.cakes);
      CakeList cakeList = new CakeList { cakes = cakes };
      string json = JsonUtility.ToJson(cakeList, true);
      File.WriteAllText(path, json);
      Debug.Log("파일 저장 완료 : " + path);
    }

    // CommandData 초기화
    List<List<recipeArrow>> recipes = new List<List<recipeArrow>>();
    foreach (Cake C in cakes)
    {
      recipes.Add(C.recipe);
    }
    Debug.Log("CommandData 초기화 : " + recipes);
    CommandData.instance.Recipes = recipes;
    CommandData.instance.RecipeCorrectList = new List<int>(new int[recipes.Count]);

    OnCakeDataChanged?.Invoke(0);
  }
  // 케이크 데이터 변경
  public void UpdateCakeData(int cakeIndex, int quantityChange, int salesChange)
  {
    if (cakeIndex < 0 || cakeIndex >= cakes.Count)
    {
      Debug.Log("Invalid Cake Update Request : " + cakeIndex);
      return;
    }
    var cake = cakes[cakeIndex];
    if (cake != null)
    {
      cake.quantity = cake.quantity + quantityChange;
      cake.sales = cake.sales + salesChange;
    }

    OnCakeDataChanged?.Invoke(cakeIndex);
  }
}
