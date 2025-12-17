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
	public List<CustomerInfo> customerInfos;
	[SerializeField] private List<int> pricePerStat; // 스탯당 가격. 일반적으로 Taste-Flavor-Texture-Appearance 순으로 주어짐.

	void Start()
	{
		recipeEventManager = RecipeEventManager.Instance;
	}

	string GetCakeID()
	{
		PlayerData playerData = PlayerData.Instance;
		return playerData.cakeCounter++.ToString();
	}

	int GetPrice()
	{
		int price = 0;
		int index = 0;
		List<StatModifier> modifiers = statCounter.GetFinalStat().modifiers;

		foreach (var coefficient in pricePerStat)
		{
			price += coefficient * modifiers[index].delta;
			Debug.LogFormat("Stat : {0}, coefficient : {1}", modifiers[index].stat.DisplayName, coefficient);
			index++;
		}
		
		return price;
	}

	List<int> GetPreferences()
	{
		// 여기서 customerInfo를 모두 참고해 이 케이크의 선호도를 산출함;
		List<int> preferences = new();
		foreach (var customerInfo in customerInfos)
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
			status = statCounter.GetFinalStat().modifiers,
			price = GetPrice(),
			recipe = statCounter.GetRecipeArrows(),
			preferences = GetPreferences()
		};
		return newCake;
	}
}