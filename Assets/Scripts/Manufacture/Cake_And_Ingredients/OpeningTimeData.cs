using System.Collections.Generic;
using UnityEngine;

public class OpeningTimeData : MonoBehaviour
{
    public CakeManager cakeManager;
    public IngredientManager ingredientManager;


    void Start()
    {
        cakeManager.UpdateCakeData(0, 8, 1);
        ingredientManager.UpdateIngredientData("Flour", 13);
    }

    public void UpdateIngredient(string ingredientName, int additionalUsage){
        ingredientManager.UpdateIngredientData(ingredientName, additionalUsage);
    }

    public void UpdateCakeData(int cakeIndex, int quantityChange = 0, int salesChange = 0){
        cakeManager.UpdateCakeData(cakeIndex, quantityChange, salesChange);
    }

    public List<Cake> GetCakeData(){
        return cakeManager.cakes;
    }
}
