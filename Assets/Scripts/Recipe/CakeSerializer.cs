using System;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class CakeSerializer : MonoBehaviour
{
    public RecipeEventManager recipeEventManager;
    public CakeBuilder cakeBuilder;

    [SerializeField]
    private int width = 1020, height = 1080;
    private RectTransform targetUI; // 캡처할 UI 루트
    public Camera uiCamera; // 캡처할 화면만 비추는 카메라

    private CakeData newCake;

    void Start()
    {
        recipeEventManager.OnSaveCake += SaveCake;
    }

    public void SaveCake(RectTransform clonedUI)
    {
        try
        {
            // 이미지를 제외한 정보로 케이크 생성
            newCake = cakeBuilder.BuildCake();
            // 이미지 캡처 후 저장 시작
            StartCoroutine(CaptureCakeUIandSave(newCake, clonedUI)); // 케이크 UI 캡쳐
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("CakeSerializer : {0}", e);
        }
    }

    public IEnumerator CaptureCakeUIandSave(CakeData newCake, RectTransform clonedUI)
    {
        Debug.Log("캡쳐 시작");
        // 렌더링 완료될 때까지 대기
        yield return new WaitForEndOfFrame();
        Debug.Log("대기 종료");

        targetUI = clonedUI;

        // 1. UI 영역을 스크린 좌표로 변환
        Vector3[] corners = new Vector3[4];
        targetUI.GetWorldCorners(corners);

        Debug.LogFormat("UI size : ({0}, {1}, {2}, {3})", corners[0], corners[1], corners[2], corners[3]);

        //int x = Mathf.RoundToInt(corners[0].x);
        //int y = Mathf.RoundToInt(corners[0].y);
        //int width = Mathf.RoundToInt(corners[2].x - corners[0].x);
        //int height = Mathf.RoundToInt(corners[2].y - corners[0].y);

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
        string imagePath = Path.Combine(CakeStorageUtil.CakeImagePath, newCake.ID + ".png");

        // 5. 저장
        if (!Directory.Exists(CakeStorageUtil.CakeImagePath))
        {
            Directory.CreateDirectory(CakeStorageUtil.CakeImagePath);
        }
        File.WriteAllBytes(imagePath, pngData);

        Debug.Log("Capture Complete  : " + imagePath);

        // 6. 저장된 이미지 경로를 전달해 케이크 데이터 저장

        newCake.imagePath = imagePath;
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

        uiCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
        Destroy(tex);
    }
}
