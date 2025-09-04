using UnityEngine;

public class DefualtSetter : MonoBehaviour
{
    [SerializeField] private Ingredient defaultIngredient; // 인스펙터에 지정

    private void Awake()
    {
        StatMultipliers_SetFallback(defaultIngredient);
    }

    private void StatMultipliers_SetFallback(Ingredient fallback)
    {
        StatMultipliers.SetFallback(fallback);
        CakeStat.SetFallback(fallback);
        Debug.Log("Default Setting Complete");
    }
}
