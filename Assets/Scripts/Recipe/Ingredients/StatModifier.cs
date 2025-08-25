using System;
using UnityEngine;

// 특정 스탯과 그 스탯의 변화량과 operator를 다루는 클래스
[Serializable]
public struct StatModifier
{
    public Stat stat;
    public int delta;

    public static StatModifier operator +(StatModifier a, StatModifier b)
    {
        if (a.stat.Index != b.stat.Index)
        {
            Debug.LogErrorFormat("Invalid addition : {0} + {1}", a.stat.DisplayName, b.stat.DisplayName);
            throw new Exception();
        }

        return new StatModifier
        {
            stat = a.stat,
            delta = a.delta + b.delta
        };
    }

    public static StatModifier operator -(StatModifier a, StatModifier b)
    {
        if (a.stat.Index != b.stat.Index)
        {
            Debug.LogErrorFormat("Invalid subtraction : {0} + {1}", a.stat.DisplayName, b.stat.DisplayName);
            throw new Exception();
        }

        return new StatModifier
        {
            stat = a.stat,
            delta = a.delta - b.delta
        };
    }

    public static StatModifier operator *(StatModifier a, StatModifier b)
    {
        if (a.stat.Index != b.stat.Index)
        {
            Debug.LogErrorFormat("Invalid multiply : {0} + {1}", a.stat.DisplayName, b.stat.DisplayName);
            throw new Exception();
        }

        return new StatModifier
        {
            stat = a.stat,
            delta = a.delta * b.delta
        };
    }
}