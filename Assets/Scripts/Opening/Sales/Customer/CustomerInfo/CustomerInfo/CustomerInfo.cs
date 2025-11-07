using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerInfo", menuName = "Scriptable Objects/Sales/CustomerInfo")]
public class CustomerInfo : ScriptableObject
{
  public int id;
  public string displayName;
  [Tooltip("선호도. 순서대로 Taste, Flavor, Texture, Appearance입니다. 헷갈리지 않게 조심!")] public List<int> preferences = new(4);
  public float maximumWaiting;
}
