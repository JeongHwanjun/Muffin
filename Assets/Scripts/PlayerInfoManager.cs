using UnityEngine;

// 게임 시작하자마자 playerData를 읽어와서 초기화해야함 ㅇㅇ
// 또한 저장도 담당함
public class PlayerInfoManager : MonoBehaviour
{
    private PlayerData playerData;
    void Awake()
    {
        playerData = PlayerData.Instance;
        // Initialize;
    }
}
