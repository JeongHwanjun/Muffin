using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CakeData
{
  public string displayName;
  public string ID;
  public List<StatModifier> status;
  public List<recipeArrow> recipe;
  public List<float> preferences;

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
      recipe = this.recipe,
      preferences = this.preferences
    };
  }
}
