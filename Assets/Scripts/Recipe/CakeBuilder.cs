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
    public List<CustomerInfo> customerInfos;

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

    List<int> GetPreferences()
    {
        // 여기서 customerInfo를 모두 참고해 이 케이크의 선호도를 산출함;
        List<int> preferences = new();
        foreach(var customerInfo in customerInfos)
        {
            int total = 0;
            int index = 0;
            List<StatModifier> modifiers = statCounter.GetFinalStat().modifiers;
            // customerInfo의 선호도 * cake의 스탯 을 총합한 값을 구해야 함.
            foreach (int preference in customerInfo.preferences)
            {
                total += preference * modifiers[index].delta;
                index++;
                // 테스트 및 수정 필요!!
            }
            preferences.Add(total);
            Debug.Log("Customer preference total : " + total);
        }
        return preferences;
    }

    public CakeData BuildCake()
    {
        CakeData newCake = new CakeData
        {
            ID = GetCakeID(),
            displayName = GetCakeName(),
            status = statCounter.GetFinalStat().modifiers,
            recipe = statCounter.GetRecipeArrows(),
            preferences = GetPreferences() // 임시로 아무거나 넣음
        };
        return newCake;
    }
}