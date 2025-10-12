using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
	private static PlayerData _playerData = null;
	public static PlayerData Instance {
		get
		{
			if (_playerData == null) _playerData = new();
			return _playerData;
		}
	}
	public int cakeCounter = 0; // 지금까지 만든 케이크의 개수

	public int recipeLenFlour = 3, recipeLenBase = 3, recipeLenTopping = 5; // 각 단계의 화살표 한도를 정하는 변수

	public Dictionary<IngredientGroup, int> ingredientTierDict = null;
}
