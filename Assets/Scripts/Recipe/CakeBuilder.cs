using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CakeBuilder : MonoBehaviour
{
    public StatCounter statCounter;
    private RecipeEventManager recipeEventManager;
    public TMP_InputField cakeName;

    private CakeStat cakeStat;

    void Start()
    {
        recipeEventManager = RecipeEventManager.Instance;
        SetInitialCakeName();
    }

    public void SetInitialCakeName()
    {
        PlayerData playerData = PlayerData.Instance;
        cakeName.text = "Cake" + playerData.cakeCounter.ToString();
    }

    string GetCakeName()
    {
        return cakeName.text;
    }

    string GetCakeID()
    {
        PlayerData playerData = PlayerData.Instance;
        playerData.cakeCounter++;
        return playerData.cakeCounter.ToString();
    }

    public CakeData BuildCake()
    {
        // 현재는 임시버전. 최종적으론 시너지와 배수를 모두 고려해 빌드해야 함.
        CakeData newCake = new CakeData
        {
            ID = GetCakeID(),
            displayName = GetCakeName(),
            status = statCounter.GetFinalStat().modifiers,
            recipe = statCounter.GetRecipeArrows(),
            preferences = new List<float>() // 임시로 아무거나 넣음
        };
        return newCake;
    }
}