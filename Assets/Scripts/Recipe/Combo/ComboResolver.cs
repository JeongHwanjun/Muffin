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
        public HashSet<int> RequireAny;
        public Ingredient delta;
    }

    private static int GetId(Ingredient ing) => ing != null ? ing.GetInstanceID() : 0;

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
                RequireAll = new HashSet<int>(r.requireAllIngredients.Where(x => x).Select(GetId)),
                RequireAny = new HashSet<int>(r.requireAnyIngredients.Where(x => x).Select(GetId)),
                delta = r.delta
            };
            _prepared.Add(pr);
        }
    }

    // 현재 재료 목록으로 발동 가능한 모든 콤보 룰 반환
    public IEnumerable<ComboRule> GetMatches(IReadOnlyList<Ingredient> current)
    {
        // 현재 세트 -> 집합화
        var currentSet = new HashSet<int>(current.Where(x=>x).Select(GetId));

        foreach (var pr in _prepared)
        {
            // ALL: current ⊇ requireAll
            bool allOk = pr.RequireAll.Count == 0 || pr.RequireAll.IsSubsetOf(currentSet);

            // ANY: requireAny ∩ current ≠ ∅  (리스트 비었으면 패스)
            bool anyOk = pr.RequireAny.Count == 0 || pr.RequireAny.Overlaps(currentSet);

            if (allOk && anyOk)
                yield return pr.rule;
        }
    }
}
