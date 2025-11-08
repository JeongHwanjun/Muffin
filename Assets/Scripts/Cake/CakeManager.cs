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
  public static CakeManager Instance { get; private set; }
  public List<Cake> cakes = new List<Cake>();// cakes는 직접접근 X - OpeningTimeData를 통해 접근할 것
  public int baseCost = 100;

  // 데이터 변경시 이벤트 발생
  public event Action<int> OnCakeDataChanged;


  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(this);
      return;
    }
    Instance = this;
  }

  // 케이크 초기화
  public void InitializeCakes(List<CakeData> cakeDatas) // Cake의 정보는 json파일로 주어짐
  {
    // 케이크 리스트에 CakeDatas를 정제해 추가
    cakes.Clear();
    foreach(CakeData cakeData in cakeDatas)
    {
      if (!File.Exists(cakeData.imagePath))
      {
        Debug.LogWarningFormat("File Does not Exist : {0} - Skip to next data", cakeData.imagePath);
        continue;
      }
      // 이미지 로딩
      byte[] imageByte = File.ReadAllBytes(cakeData.imagePath);
      Texture2D texture = new Texture2D(2, 2);
      texture.LoadImage(imageByte);
      Sprite cakeSprite = Sprite.Create(texture,
        new Rect(0, 0, texture.width, texture.height),
        new Vector2(0.5f, 0.5f));

      Cake newCake = new Cake
      {
        name = cakeData.displayName,
        quantity = 3, // 기본 quantity가 필요함
        sales = 0,
        price = cakeData.price,
        cost = cakeData.status[4].delta, // 4번이 cost임
        recipe = cakeData.recipe,
        preferences = cakeData.preferences,
        sprite = cakeSprite
      };
      cakes.Add(newCake);
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
