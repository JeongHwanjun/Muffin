using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

[Serializable]
public class AnimatedText
{
  public TextMeshProUGUI text;
  [HideInInspector] public int currentValue = -1; // 처음 시작시 -1로 초기화
  [HideInInspector] public Tweener tween;
}