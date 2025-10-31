using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

[Serializable]
public class CakeData
{
  public string displayName;
  public string ID;
  public List<StatModifier> status;
  public int price;
  public List<recipeArrow> recipe;
  public List<int> preferences;
  public string imagePath;

  // json변환용 직렬화 코드
  public object ToSerializable()
  {
    var statusDict = new Dictionary<string, int>();
    foreach (var s in status)
    {
      statusDict[s.stat.DisplayName] = s.delta;
    }

    return new
    {
      displayName = this.displayName,
      ID = this.ID,
      status = statusDict,
      price = this.price,
      recipe = this.recipe,
      preferences = this.preferences,
      imagePath = this.imagePath
    };
  }

  public static CakeData FromSerializable(JObject obj, StatRegistry registry) //registry는 사용처에서 넘겨줌
  {
    if (registry == null) throw new ArgumentNullException(nameof(registry));

    var cake = new CakeData
    {
      displayName = (string)obj["displayName"],
      ID          = (string)obj["ID"],
      price       = (int)obj["price"],
      recipe      = obj["recipe"].ToObject<List<recipeArrow>>(),
      preferences = obj["preferences"].ToObject<List<int>>(),
      imagePath   = (string)obj["imagePath"]
    };

    var statusDict = obj["status"].ToObject<Dictionary<string, int>>();
    cake.status = new List<StatModifier>();

    foreach (var kv in statusDict)
    {
      var stat = registry.FindByName(kv.Key); // 앞서 만든 조회 API 사용
      if (stat == null)
      {
        Debug.LogWarning($"[CakeData] 등록되지 않은 Stat 이름: '{kv.Key}'");
        continue; // 혹은 예외 throw
      }

      cake.status.Add(new StatModifier
      {
        stat  = stat,
        delta = kv.Value
      });
    }

    return cake;
  }
}
