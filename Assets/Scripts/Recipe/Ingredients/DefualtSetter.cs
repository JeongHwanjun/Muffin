using UnityEngine;

public class DefualtSetter : MonoBehaviour
{
    [SerializeField] private IngredientBase defaultIngredient; // 인스펙터에 지정

    private void Awake()
    {
        StatMultipliers_SetFallback(defaultIngredient);
    }

    private void StatMultipliers_SetFallback(IngredientBase fallback)
    {
        StatMultipliers.SetFallback(fallback);
        CakeStat.SetFallback(fallback);
        Debug.Log("Default Setting Complete");
    }
}
