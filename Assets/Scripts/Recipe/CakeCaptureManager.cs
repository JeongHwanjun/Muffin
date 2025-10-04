using UnityEngine;

public class CakeCaptureManager : MonoBehaviour
{
    public RectTransform sourceUI;      // 원본 UI (Overlay에 있는 것)
    public Canvas captureCanvas;        // 캡처 전용 캔버스
    public Camera captureCamera;        // 캡처 전용 카메라

    private RectTransform clonedUI;
    private RecipeEventManager recipeEventManager;

    void Start()
    {
        recipeEventManager = RecipeEventManager.Instance;
        recipeEventManager.OnCloneCakePanel += CloneUIForCapture;
    }

    public void CloneUIForCapture()
    {
        if (clonedUI != null) Destroy(clonedUI.gameObject);

        // 1. 원본 UI 복제
        clonedUI = Instantiate(sourceUI, captureCanvas.transform);

        // 2. 위치/스케일 초기화
        clonedUI.anchoredPosition3D = sourceUI.anchoredPosition3D;
        clonedUI.sizeDelta = sourceUI.sizeDelta;
        clonedUI.localScale = sourceUI.localScale;

        // 3. RectTransform Pivot, Anchor도 복사 필요시 직접 맞춤
        clonedUI.anchorMin = sourceUI.anchorMin;
        clonedUI.anchorMax = sourceUI.anchorMax;
        clonedUI.pivot = sourceUI.pivot;

        // 4. Layer 설정 (카메라에 잡히도록)
        SetLayerRecursively(clonedUI.gameObject, LayerMask.NameToLayer("TargetUI"));

        Debug.Log("✅ UI 복제 완료 (캡처 전용 Canvas에 세팅됨)");

        recipeEventManager.TriggerSaveCake(clonedUI);
    }

    private void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
            SetLayerRecursively(child.gameObject, layer);
    }
}