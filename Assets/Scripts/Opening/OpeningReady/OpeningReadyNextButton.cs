using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningReadyNextButton : MonoBehaviour
{
    public CakeDataReader cakeDataReader;
    private CakeManager cakeManager;
    private OpeningReadyEventManager openingReadyEventManager;
    [SerializeField] private string nextSceneName;
    void Start()
    {
        openingReadyEventManager = OpeningReadyEventManager.Instance;
        cakeManager = CakeManager.Instance;
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

        // 운반자 생성
        var carrierGO = new GameObject("CakeDataCarrier");
        var carrier = carrierGO.AddComponent<CakeDataCarrier>();
        carrier.Datas = cakeDatas;

        // 씬 로드 후 초기화
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        var carrier = FindFirstObjectByType<CakeDataCarrier>();
        var cakeManager = CakeManager.Instance;

        if (carrier != null && cakeManager != null && carrier.Datas != null && carrier.Datas.Count > 0)
        {
            cakeManager.InitializeCakes(carrier.Datas);
            Destroy(carrier.gameObject); // 사용 후 정리
        }
        else
        {
            Debug.LogWarning("Carrier or CakeManager missing; fallback init.");
        }
    }
}
