using UnityEngine;

public class OpeningTimeData : MonoBehaviour
{
    public CakeManager cakeManager;
    public IngredientManager ingredientManager;


    void Start()
    {
        cakeManager.UpdateCakeData("Chocolate Cake", 8, 1);
        ingredientManager.UpdateIngredientData("Flour", 13);
    }

    public void UpdateIngredient(string ingredientName, int additionalUsage){
        ingredientManager.UpdateIngredientData(ingredientName, additionalUsage);
    }
}
