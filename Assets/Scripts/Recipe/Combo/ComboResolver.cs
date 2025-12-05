using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComboResolver : MonoBehaviour
{
    [SerializeField] private ComboRuleSet ruleSet;
    private List<PreparedRule> _prepared = new(); // 캐싱

    private class PreparedRule
    {
        public ComboRule rule;
        public HashSet<int> RequireAll;
        public Ingredient delta;
    }

    private static int GetId(IngredientGroup group) => group != null ? group.GetInstanceID() : 0;

    private void Awake()
    {
        BuildIndex();
    }

    public void BuildIndex()
    {
        _prepared.Clear();
        if (ruleSet == null) return;

        foreach (var r in ruleSet.rules)
        {
            if (r == null) continue;

            var pr = new PreparedRule
            {
                rule = r,
                RequireAll = new HashSet<int>(r.requireAllGroups.Where(x => x).Select(GetId)),
                delta = r.delta
            };
            _prepared.Add(pr);
        }
    }

    // 현재 재료 목록으로 발동 가능한 모든 콤보 룰 반환
    public IEnumerable<ComboRule> GetMatches(IReadOnlyList<Ingredient> current)
    {
        // 현재 세트 -> 집합화
        var currentSet = new HashSet<int>(current.Where(x=>x && x.group!=null).Select(x=>x.group.GetInstanceID()));

        foreach (var pr in _prepared)
        {
            // ALL: current ⊇ requireAll
            bool allOk = pr.RequireAll.Count == 0 || pr.RequireAll.IsSubsetOf(currentSet);
            // 기존에 OR도 있었으나 삭제됨

            if (allOk)
                yield return pr.rule;
        }
    }
}
