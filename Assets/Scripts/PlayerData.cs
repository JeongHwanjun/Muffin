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
	public int cakeCounter = 0; // 지금까지 만든 케이크의 번호
	public int cakeNumLimit = 10; // 케이크 최대 개수
	public int cakeSlotLimit = 3; // 케이크 슬롯 최대 개수
	public int recipeLenFlour = 3, recipeLenBase = 3, recipeLenTopping = 5; // 각 단계의 화살표 한도를 정하는 변수

	public Dictionary<IngredientGroup, int> ingredientTierDict = null; // 각 재료의 티어를 기록함
}
