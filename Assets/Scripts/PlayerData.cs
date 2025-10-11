using System;
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
	public int cakeCounter = 0;

	public int recipeLenFlour = 3, recipeLenBase = 3, recipeLenTopping = 5; // 각 단계의 화살표 한도를 정하는 변수
}
