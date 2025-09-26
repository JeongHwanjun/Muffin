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

    void Start()
    {
        recipeEventManager = RecipeEventManager.Instance;
    }

    public CakeData BuildCake()
    {
        // 현재는 임시버전. 최종적으론 시너지와 배수를 모두 고려해 빌드해야 함.
        CakeData newCake = new CakeData();
        /*{
            cakeID = UnityEngine.Random.Range(0, 5000).ToString(),
            cakeName = GetCakeName(),
            finalTaste = GetTotalTaste(),
            finalFlavor = GetTotalFlavor(),
            finalTexture = GetTotalTexture(),
            finalAppearance = GetTotalAppearance(),
            finalCost = GetTotalCost(),
            recipe = GetFinalRecipe()
        };*/
        return newCake;
    }
}