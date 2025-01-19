using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/*
    반죽을 생성하는 오브젝트의 스크립트

    입력 : 클릭, 특정 키(미정)
    출력 : 재료 소모량 증가, 반죽 생성
*/
public class PasteMachine : ClickableThing
{
    public GameObject sheet_raw;
    private Line selfLine;
    [SerializeField]
    private ManufactureAdmin manufactureAdmin;
    public float operatingTime = 4.0f, coolTime = -1.0f;
    private bool coolDown = false;
    [SerializeField]
    private int usage = 4;
    private int selfLineNumber;

    protected override void Start()
    {
        base.Start();
        selfLine = GetComponentInParent<Line>();
        selfLineNumber = selfLine.LineNumber;
        manufactureAdmin = selfLine.manufactureAdmin;
        manufactureAdmin.ConsumeIngredient += CreateSheet;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(coolDown){
            coolTime -= Time.deltaTime;
            if(coolTime <= 0){
                coolDown = false;
            }
        }
    }

    public override void OnClick()
    {
        // 클릭 이벤트 발생
        if(!coolDown){
            manufactureAdmin.TriggerConsumeIngredient(usage, selfLineNumber);
        }
        
    }

    private void CreateSheet(int printingLineNumber){
        if(printingLineNumber == selfLineNumber){
            Instantiate(sheet_raw, transform.position, Quaternion.identity);
            coolTime = operatingTime;
            coolDown = true;
        }
    }

    protected override void OnDisable() {
        base.OnDisable();
        manufactureAdmin.ConsumeIngredient -= CreateSheet;
    }
}
