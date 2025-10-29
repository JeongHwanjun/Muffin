using UnityEngine;

public class CakeSlotContainer : MonoBehaviour
{
    public GameObject cakeSlotPrefab;

    private PlayerData playerData;
    void Start()
    {
        // 1. playerData에서 케이크 슬롯 제한을 읽어옴
        playerData = PlayerData.Instance;
        for(int i=0; i < playerData.cakeSlotLimit; i++)
        {
            Instantiate(cakeSlotPrefab, transform);
        }
    }
}
