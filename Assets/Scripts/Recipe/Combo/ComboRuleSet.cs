using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboRuleSet", menuName = "Scriptable Objects/ComboRuleSet")]
public class ComboRuleSet : ScriptableObject
{
  public List<ComboRule> rules = new();
}
