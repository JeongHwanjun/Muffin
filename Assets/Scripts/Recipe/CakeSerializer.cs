using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CakeSerializer : MonoBehaviour
{
    public RecipeEventManager recipeEventManager;
    public CakeBuilder cakeBuilder;
    public TMP_InputField cakeNameInputfield;

    [SerializeField]
    private int width = 1020, height = 1080;
    private RectTransform targetUI; // 캡처할 UI 루트
    public Camera uiCamera; // 캡처할 화면만 비추는 카메라
    PlayerData playerData = PlayerData.Instance;
    List<CakeMetaData> cakeMetadatas;
    string cakeMetadataPath, cakeImagePath;

    private CakeData newCake;

    void Start()
    {
        recipeEventManager.OnCaptureCake += CaptureCake;
        recipeEventManager.OnSaveCake += SaveCake;
    }

    public void CaptureCake(RectTransform clonedUI)
    {
        cakeMetadataPath = Path.Combine(CakeStorageUtil.MetaDataPath, "Metadata.json");
        // 케이크 관련 메타데이터 획득
        if (!File.Exists(cakeMetadataPath))
        {
            Directory.CreateDirectory(CakeStorageUtil.MetaDataPath);
            List<CakeMetaData> data = new();
            string emptyJson = JsonConvert.SerializeObject(data);
            File.WriteAllText(cakeMetadataPath, emptyJson);
        }
        
        string text = File.ReadAllText(cakeMetadataPath);
        cakeMetadatas = JsonConvert.DeserializeObject<List<CakeMetaData>>(text);
        // 추가 케이크 생성이 가능한지 검사
        if(cakeMetadatas.Count >= playerData.cakeNumLimit)
        {
            Debug.LogWarningFormat("CakeNumLimit exceeded : {0}", cakeMetadatas.Count);
            // 불가능하다면 종료 - 추가적인 연출 필요함
            return;
        }

        
        try
        {
            // 이미지 캡처
            int cakeId = PlayerData.Instance.cakeCounter; // 이 숫자를 건드리는건 build 할때 뿐임
            StartCoroutine(CaptureCakeUI(cakeId, clonedUI));
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("CakeSerializer : {0}", e);
        }

        // 이미지, 이름을 제외한 정보로 케이크 생성
        newCake = cakeBuilder.BuildCake();
    }

    public IEnumerator CaptureCakeUI(int newCakeId, RectTransform clonedUI)
    {
        Debug.Log("캡쳐 시작");
        // 렌더링 완료될 때까지 대기
        yield return new WaitForEndOfFrame();
        Debug.Log("대기 종료");

        targetUI = clonedUI;

        // 1. UI 영역을 스크린 좌표로 변환
        Vector3[] corners = new Vector3[4];
        targetUI.GetWorldCorners(corners);

        //2. RenderTexture 생성 및 렌더링
        RenderTexture rt = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
        uiCamera.targetTexture = rt;

        uiCamera.Render();

        RenderTexture.active = rt;

        // 3. Texture2D 생성 및 영역 복사
        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        tex.Apply();
        // 렌더링된 rt를 GPU에서 CPU로 복사함. 성능 이슈가 있음.

        // 4. png 변환
        byte[] pngData = tex.EncodeToPNG();
        cakeImagePath = Path.Combine(CakeStorageUtil.CakeImagePath, newCakeId + ".png");

        // 5. 저장
        if (!Directory.Exists(CakeStorageUtil.CakeImagePath))
        {
            Directory.CreateDirectory(CakeStorageUtil.CakeImagePath);
        }
        File.WriteAllBytes(cakeImagePath, pngData);
        // 캡처 완료, pngData 이벤트 발생(팝업 표시용)
        recipeEventManager.TriggerCaptureCakeCompleted(pngData);

        Debug.Log("Capture Complete  : " + cakeImagePath);

        uiCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
        Destroy(tex);
    }

    void SaveCake()
    {
        // 6. 저장된 이미지 경로를 전달해 케이크 데이터 저장
        // build시 누락된 부분들 보충
        newCake.imagePath = cakeImagePath;
        newCake.displayName = cakeNameInputfield.text;
        string json = JsonConvert.SerializeObject(newCake.ToSerializable(), Formatting.Indented);
        Debug.Log("CakeData Serialized : " + json.ToString());
        string folderPath = Path.Combine(CakeStorageUtil.CakeRecipePath);
        string filePath = Path.Combine(CakeStorageUtil.CakeRecipePath, "Cake" + newCake.ID + ".json");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        File.WriteAllText(filePath, json);
        Debug.Log("케이크 데이터 저장 완료: " + CakeStorageUtil.CakeRecipePath);

        // 7. 메타데이터 기록
        CakeMetaData newMetadata = new();
        newMetadata.id = newCake.ID;
        newMetadata.imagePath = cakeImagePath;
        newMetadata.cakePath = filePath;
        newMetadata.displayName = newCake.displayName;
        cakeMetadatas.Add(newMetadata);
        string cakeMetadataJson = JsonConvert.SerializeObject(cakeMetadatas, Formatting.Indented);
        File.WriteAllText(cakeMetadataPath, cakeMetadataJson);

        // 8. 메인 화면으로 복귀
        SceneManager.LoadScene("MainScene");
    }
}
