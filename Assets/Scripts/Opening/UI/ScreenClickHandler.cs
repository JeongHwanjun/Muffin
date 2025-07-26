using UnityEngine;
using UnityEngine.InputSystem; // InputAction을 사용하기 위해 추가

public class ScreenClickHandler : MonoBehaviour {
    public Camera[] renderCameras; // Render Texture에 연결된 카메라
    public RenderTexture[] renderTextures; // Render Texture
    public RectTransform[] rawImageRects; // Render Texture를 출력하는 Raw Image의 RectTransform

    private int currentScreenNumber = 1;

    public InputAction clickAction; // 임의의 입력을 처리할 InputAction (마우스 클릭, 키보드 등)

    private void OnEnable() {
        clickAction.Enable(); // InputAction 활성화
        clickAction.canceled += OnClick; // 입력 발생 시 이벤트 등록
    }

    private void OnDisable() {
        clickAction.canceled -= OnClick; // 이벤트 해제
        clickAction.Disable(); // InputAction 비활성화
    }

    private void OnClick(InputAction.CallbackContext context) {
        // 입력이 발생하면 클릭 처리 로직 실행
        Vector2 screenPosition = Mouse.current.position.ReadValue(); // 현재 마우스 포인터 위치를 가져옴

        // Raw Image 내에서의 로컬 좌표 계산
        if (RectTransformUtility.RectangleContainsScreenPoint(rawImageRects[currentScreenNumber], screenPosition)) {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rawImageRects[currentScreenNumber], // Raw Image의 RectTransform
                screenPosition, // 스크린 좌표
                null, // UI가 Screen Space Overlay일 경우 null
                out localPoint
            );

            Debug.Log($"클릭한 위치 (Raw Image 내부): {localPoint}");

            // Render Texture의 UV 좌표 계산
            float normalizedX = (localPoint.x / rawImageRects[currentScreenNumber].rect.width) + 0.5f;
            float normalizedY = (localPoint.y / rawImageRects[currentScreenNumber].rect.height) + 0.5f;

            // Render Texture에 연결된 카메라에서 Viewport 좌표로 변환 후 Raycast
            Vector3 worldPosition = renderCameras[currentScreenNumber]
            .ViewportToWorldPoint(new Vector3(normalizedX, normalizedY, renderCameras[currentScreenNumber].nearClipPlane));

            // 2D Raycast 실행
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (hit.collider != null) {
                Debug.Log($"히트된 오브젝트: {hit.collider.gameObject.name}");
                // 히트된 오브젝트에서 상호작용 처리
                hit.collider.GetComponent<ClickableThing>()?.OnClick();
            }
        }
    }

    public void SwapScreen(int targetScreenNumber){
        currentScreenNumber = Mathf.Clamp(targetScreenNumber,0,renderCameras.Length);
    }
}
