using UnityEngine;

public class Line : MonoBehaviour
{
    public int LineNumber = 0; // 본 라인의 번호
    public bool isLineReady = false; // 라인의 준비 여부
    public GameObject character; // 캐릭터, 라인의 선택 여부를 사용자에게 표시함
    public PasteMachine pasteMachine; // 반죽기, 반죽 생성 담당
    public ManufactureAdmin manufactureAdmin;
    ManufactureEventManager manufactureEventManager;

    // 초기화
    public void Initialize(int number, ManufactureAdmin _manufactureAdmin, ManufactureEventManager _manufactureEventManager)
    {
        LineNumber = number;
        manufactureAdmin = _manufactureAdmin;
        manufactureEventManager = _manufactureEventManager;
        Debug.Log("Line" + LineNumber + " : Init");
    }

    // 라인변경시 실행됨
    public void LineChange(bool focus)
    {
        Debug.Log("Line" + LineNumber + " : LineChange");
        // 기타 처리들...
        // 캐릭터 활성화/비활성화 등등

        if (focus) // 라인 변경 결과 자신이 선택된 라인이라면
        {
            // 캐릭터 활성화
            character.SetActive(true);
        }
        else // 라인 변경 결과 자신이 선택 해제된 라인이라면
        {
            // 캐릭터 비활성화
            character.SetActive(false);
        }
    }

    public void SwitchLineReady()
    {
        Debug.Log("Line" + LineNumber + " : switchLineReady");
        // 애니메이션 재생, 준비 상태 전환 등 담당
        isLineReady = !isLineReady;

        if (isLineReady)
        {
            // 준비된 상태로 애니메이션 재생 및 스프라이트 변경
        }
        else
        {
            // 준비되지 않은 상태로 애니메이션 재생 및 스프라이트 변경
        }
    }

    // W,S 버튼을 눌렀을 때 반죽 생성
    public void PrintPaste()
    {
        Debug.Log("Line : PrintPaste");
        // 클릭한 것과 같은 효과를 가짐.
        pasteMachine.OnClick();
    }
}