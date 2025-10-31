using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OpeningReadyNextButton : MonoBehaviour
{
    public CakeDataReader cakeDataReader;
    private OpeningReadyEventManager openingReadyEventManager;
    void Start()
    {
        openingReadyEventManager = OpeningReadyEventManager.Instance;
    }
    public void OnButtonClick()
    {
        List<CakeData> cakeDatas = cakeDataReader.ReadCakeDatas();
        // null = 모종의 이유로 읽기에 실패했거나 길이가 <= 0
        if (cakeDatas == null)
        {
            Debug.LogWarning("Failed reading CakeDatas");
            // 실패 이벤트 발생
            openingReadyEventManager.TriggerCakeDataReadingFail();
        }

        // 정보를 정제해 다음 cakeDataManager에게 넘겨주며 다음 씬 시작
        Debug.Log("CakeDatas : " + cakeDatas.Last().displayName);
    }
}
