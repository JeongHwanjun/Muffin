using System;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class CakeSerializer : MonoBehaviour
{
    public RecipeEventManager recipeEventManager;
    public CakeBuilder cakeBuilder;
    
    public RectTransform targetUI; // 캡처할 UI

    void Start()
    {
        recipeEventManager.OnSaveCake += SaveCake;
    }

    public void SaveCake()
    {
        try
        {
            CakeData newCake = cakeBuilder.BuildCake();
            string json = JsonConvert.SerializeObject(newCake.ToSerializable(), Formatting.Indented);
            Debug.Log("CakeData Serialized : " + json.ToString());
            /*
            string folderPath = Path.Combine(CakeStorageUtil.SavePath);
            string filePath = Path.Combine(CakeStorageUtil.SavePath, newCake.displayName + ".json");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            File.WriteAllText(filePath, json);
            Debug.Log("케이크 데이터 저장 완료: " + CakeStorageUtil.SavePath);

            StartCoroutine(CaptureCakeUI());
            */
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("CakeSerializer : {0}", e);
        }
    }

    public IEnumerator CaptureCakeUI()
    {
        Debug.Log("캡쳐 시작");
        // 렌더링 완료될 때까지 대기
        yield return new WaitForEndOfFrame();
        Debug.Log("대기 종료");

        // 1. UI 영역을 스크린 좌표로 변환
        Vector3[] corners = new Vector3[4];
        targetUI.GetWorldCorners(corners);

        int x = Mathf.RoundToInt(corners[0].x);
        int y = Mathf.RoundToInt(corners[0].y);
        int width = Mathf.RoundToInt(corners[2].x - corners[0].x);
        int height = Mathf.RoundToInt(corners[2].y - corners[0].y);

        // 2. Texture2D 생성 및 영역 복사
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(x, y, width, height), 0, 0);
        tex.Apply();

        // 3. Sprite 변환 (원하면 바로 UI Image에 사용 가능)
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));

        Debug.Log("UI 일부 캡처 완료! 크기: " + width + "x" + height);
    }
}
