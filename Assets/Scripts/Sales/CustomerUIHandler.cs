using UnityEngine;
using UnityEngine.UIElements;

// 고객이 원하는 상품을 표기하는 UI 컨트롤러
public class CustomerUIHandler : MonoBehaviour{
    public GameObject target; // 말풍선이 따라다닐 GameObject
    public Sprite bubbleSprite; // 말풍선의 9-sliced 스프라이트
    public Sprite icon; // 말풍선 내부의 이미지
    public Camera targetCamera;
    public float offsetX = -1;
    
    private VisualElement speechBubble;
    private VisualElement imageContainer;

    private void OnEnable()
    {
        // UIDocument로부터 루트 VisualElement 가져오기
        var root = GetComponent<UIDocument>().rootVisualElement;

        // 말풍선 요소 가져오기
        speechBubble = root.Q<VisualElement>("SpeechBubble");
        imageContainer = speechBubble.Q<VisualElement>("CakeImage");

        // 9-sliced 스프라이트 설정
        var styleBackground = speechBubble.style;
        //styleBackground.backgroundImage = new StyleBackground(bubbleSprite);


    }

    private void Update()
    {
        if (target == null) return;
        if (targetCamera == null) {
            targetCamera = GameObject.FindGameObjectWithTag("SalesCamera").GetComponent<Camera>();
        }

        // 타겟의 월드 좌표를 화면 좌표로 변환
        Vector3 screenPosition = targetCamera.WorldToScreenPoint(target.transform.position);

        // RenderTexture 크기를 기준으로 스크린 좌표 변환
        float rtWidth = targetCamera.targetTexture.width;
        float rtHeight = targetCamera.targetTexture.height;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // 화면 경계 체크
        Vector3 viewportPosition = targetCamera.WorldToViewportPoint(target.transform.position);
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            speechBubble.style.display = DisplayStyle.None; // 화면 밖이면 숨김
            return;
        }

        // 화면 안에 있으면 표시
        speechBubble.style.display = DisplayStyle.Flex;

        // 좌표 변환
        speechBubble.style.left = (screenPosition.x / rtWidth) * screenWidth;
        speechBubble.style.top = screenHeight - (screenPosition.y / rtHeight) * screenHeight;
    }
}