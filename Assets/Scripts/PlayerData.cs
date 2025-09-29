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
}
