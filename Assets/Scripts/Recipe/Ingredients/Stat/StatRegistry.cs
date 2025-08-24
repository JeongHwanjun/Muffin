using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StatRegistry", menuName = "Scriptable Objects/StatRegistry")]
public class StatRegistry : ScriptableObject
{
	public List<Stat> stats = new(); // 프로젝트에 존재하는 모든 Trait
	public int Count => stats.Count;

	// 에디터에서 호출(메뉴 버튼/OnValidate 등)
	public void AssignIndices()
	{
		for (int i = 0; i < stats.Count; i++)
			stats[i].__Editor_SetIndex(i);
	}

	void OnValidate()
	{
		Debug.Log("StatRegistry : Onvalidate");
		AssignIndices();
	}
}
