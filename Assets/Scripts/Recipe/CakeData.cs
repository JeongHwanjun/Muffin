using System.Collections.Generic;
using System;

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
}
