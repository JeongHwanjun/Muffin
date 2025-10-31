using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "StatRegistry", menuName = "Scriptable Objects/StatRegistry")]
public class StatRegistry : ScriptableObject, ISerializationCallbackReceiver
{
    [Tooltip("프로젝트에 존재하는 모든 Stat(중복 이름 없이)")]
    public List<Stat> stats = new();

    public int Count => stats?.Count ?? 0;

    // 이름 기준(표시명/ID 등 원하는 키로 선택) : 대소문자 무시
    Dictionary<string, Stat> _byName;
    // 인덱스 기준
    Dictionary<int, Stat> _byIndex;

    // 트림 + 대소문자 무시
    static string Canon(string s) => string.IsNullOrWhiteSpace(s) ? string.Empty : s.Trim();

    static string KeyOf(Stat s) => Canon(s?.DisplayName);

    void AssignIndices()
    {
        for (int i = 0; i < stats.Count; i++)
            stats[i].__Editor_SetIndex(i);
    }

    void OnValidate()
    {
        AssignIndices();
        BuildCache();
    }

    void OnEnable()
    {
        BuildCache();
    }

    // Json/직렬화 이후에도 캐시 재구축
    public void OnAfterDeserialize()
    {
        // ScriptableObject는 OnAfterDeserialize 직후 OnEnable이 안 올 수도 있어 캐시를 바로 구축
        BuildCache();
    }
    public void OnBeforeSerialize() { /* no-op */ }

    void BuildCache()
    {
        // 안전망
        if (stats == null) stats = new List<Stat>();

        // 중복 이름 감지용
        var dName = new Dictionary<string, Stat>(StringComparer.OrdinalIgnoreCase);
        var dupList = new List<string>();

        foreach (var s in stats.Where(x => x != null))
        {
            var key = KeyOf(s);
            if (string.IsNullOrEmpty(key))
                continue;

            if (dName.ContainsKey(key))
            {
                dupList.Add(key);
                // 정책: 마지막 항목으로 덮어쓰기 원하면 아래 한 줄만 쓰면 됨
                dName[key] = s;
            }
            else
            {
                dName.Add(key, s);
            }
        }

        _byName  = dName;
        _byIndex = stats.Where(s => s != null).ToDictionary(s => s.Index, s => s);

#if UNITY_EDITOR
        if (dupList.Count > 0)
        {
            var msg = $"[StatRegistry] 중복 이름 감지: {string.Join(", ", dupList.Distinct())}";
            Debug.LogWarning(msg, this);
        }
#endif
    }
    // 조회 API

    /// <summary>이름(DisplayName)으로 Stat 찾기(없으면 null)</summary>
    public Stat FindByName(string name)
    {
        if (_byName == null) BuildCache();
        name = Canon(name);
        if (string.IsNullOrEmpty(name)) return null;
        return _byName.TryGetValue(name, out var s) ? s : null;
    }

    /// <summary>이름(DisplayName)으로 Stat 찾기(없으면 예외)</summary>
    public Stat GetByName(string name)
    {
        var s = FindByName(name);
        if (s != null) return s;
        throw new KeyNotFoundException($"Stat '{name}' 를 찾을 수 없습니다. (등록된 수: {Count})");
    }

    /// <summary>이름으로 시도 후 성공/실패 반환</summary>
    public bool TryGetByName(string name, out Stat stat)
    {
        stat = FindByName(name);
        return stat != null;
    }

    /// <summary>인덱스로 Stat 찾기(없으면 null)</summary>
    public Stat FindByIndex(int index)
    {
        if (_byIndex == null) BuildCache();
        return _byIndex.TryGetValue(index, out var s) ? s : null;
    }
}
