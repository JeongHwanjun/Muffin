using UnityEngine;

public class CakeInfoPanel : MonoBehaviour
{
    CakeManager cakeManager;
    [SerializeField] GameObject CakeCardPrefab;

    void Start()
    {
        cakeManager = CakeManager.Instance;

        Initialize();
    }

    void Initialize()
    {
        foreach(Cake cake in cakeManager.cakes)
        {
            GameObject newCakeCard = Instantiate(CakeCardPrefab, transform);
            CakeInfoCard newCakeInfoCard = newCakeCard.GetComponent<CakeInfoCard>();
            newCakeInfoCard.Initialize(cake);
        }
    }
}
