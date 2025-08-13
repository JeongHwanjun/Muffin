// 스탯 저장용 클래스
using System;

public class CakeStat
{
    public int taste = 0; // 맛
    public int flavor = 0; // 풍미
    public int texture = 0; // 식감
    public int appearance = 0; // 외관
    public int cost = 0; // 비용

    // init
    public CakeStat() { }
    public CakeStat(IngredientBase stat)
    {
        taste = stat.taste;
        flavor = stat.flavor;
        texture = stat.texture;
        appearance = stat.appearance;
        cost = stat.cost;
    }
    
    // +
    public static CakeStat operator +(CakeStat a, CakeStat b)
    {
        return new CakeStat
        {
            taste = a.taste + b.taste,
            flavor = a.flavor + b.flavor,
            texture = a.texture + b.texture,
            appearance = a.appearance + b.appearance,
            cost = a.cost + b.cost
        };
    }

    // -
    public static CakeStat operator -(CakeStat a, CakeStat b)
    {
        return new CakeStat
        {
            taste = a.taste - b.taste,
            flavor = a.flavor - b.flavor,
            texture = a.texture - b.texture,
            appearance = a.appearance - b.appearance,
            cost = a.cost - b.cost
        };
    }

    // * - 배수 적용
    public static CakeStat operator *(CakeStat a, StatMultipliers m)
    {
        return new CakeStat
        {
            taste = (int)MathF.Round(a.taste * m.taste),
            flavor = (int)MathF.Round(a.flavor * m.flavor),
            texture = (int)MathF.Round(a.texture * m.texture),
            appearance = (int)MathF.Round(a.appearance * m.appearance),
            cost =(int)MathF.Round(a.cost * m.cost),
        };
    }
}

// 배율 저장용 클래스
public class StatMultipliers
{
    public float taste = 1;
    public float flavor = 1;
    public float texture = 1;
    public float appearance = 1;
    public float cost = 1;

    public static StatMultipliers operator +(StatMultipliers a, StatMultipliers b)
    {
        return new StatMultipliers
        {
            taste = a.taste + b.taste,
            flavor = a.flavor + b.flavor,
            texture = a.texture + b.texture,
            appearance = a.appearance + b.appearance,
            cost = a.cost + b.cost,
        };
    }
}