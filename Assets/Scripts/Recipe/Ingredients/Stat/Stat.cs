using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Scriptable Objects/Stat")]
public class Stat : ScriptableObject
{
    [SerializeField] private string displayName;
    [SerializeField] private int index = -1;
    public string DisplayName => displayName;
    public int Index => index;

    public void __Editor_SetIndex(int i) => index = i;
}
