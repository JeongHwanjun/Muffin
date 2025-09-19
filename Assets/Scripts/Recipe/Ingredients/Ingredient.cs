using System.Collections.Generic;
using UnityEngine;

/* 실질적으로 재료의 이름과 변화량을 담는 클래스 */
[CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable Objects/Ingredient/Default")]
[System.Serializable]
public class Ingredient : ScriptableObject
{
    public int id;

    [SerializeField] public string displayName;
    [SerializeField] public int tier;
    [SerializeField] public IngredientGroup group;
    [SerializeField] private StatRegistry statRegistry; // 레지스트리 참조(동기화)
    [SerializeField] public List<StatModifier> modifiers = new();

    public List<recipeArrow> recipeArrows;

#if UNITY_EDITOR
    void OnValidate()
    {
        // 레지스트리가 없으면 패스
        if (statRegistry == null || statRegistry.stats == null) return;

        // 레지스트리 인덱스-순서에 맞춰 modifiers를 재구성
        SyncModifiersToRegistryOrder();
    }

    [ContextMenu("Sync Modifiers To StatRegistry Order")]
    private void SyncModifiersToRegistryOrder()
    {
        // 1) 기존 입력을 index 기준으로 모으기(중복 합산)
        var existingByIndex = new Dictionary<int, int>();
        foreach (var m in modifiers)
        {
            if (m.stat == null) continue;
            int idx = m.stat.Index;
            if (idx < 0 || idx >= statRegistry.Count) continue;

            if (existingByIndex.TryGetValue(idx, out var cur))
                existingByIndex[idx] = cur + m.delta;
            else
                existingByIndex[idx] = m.delta;
        }

        // 2) 레지스트리 순서대로 새 리스트 빌드 (누락은 0, Stat는 정확히 채움)
        var rebuilt = new List<StatModifier>(statRegistry.Count);
        for (int i = 0; i < statRegistry.Count; i++)
        {
            var stat = statRegistry.stats[i];
            if (stat == null) continue; // 방어

            existingByIndex.TryGetValue(i, out int delta);
            rebuilt.Add(new StatModifier { stat = stat, delta = delta });
        }

        // 3) 교체
        modifiers = rebuilt;

        // 4) 오브젝트 더티 처리(에디터 저장 반영)
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif

    public virtual IngredientType GetIngredientType()
    {
        return IngredientType.None;
    }
}

public enum IngredientType // 재료가 어떤 단계에 속하는지 표시함
{
    Flour,
    Base,
    Topping,
    None // 아무 타입도 아님. 이 타입이 실제로 감지된다면 오류 발생 권장.
};