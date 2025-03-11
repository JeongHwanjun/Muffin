using System.Collections.Generic;
using UnityEngine;

// 여러개의 케이크를 한방에 관리하기 위한 ScriptableObject
[CreateAssetMenu(fileName = "CakeCollection", menuName = "Scriptable Objects/CakeCollection")]
public class CakeCollection : ScriptableObject
{
    public List<Cake> cakes = new List<Cake>();
}
