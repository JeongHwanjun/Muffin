using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cake {
  public string name;
  public int quantity;
  public int sales;
  public int price;
  public int cost;
  public List<recipeArrow> recipe = new List<recipeArrow>();
  public Sprite sprite;
}