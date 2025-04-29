using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/*
    반죽을 생성하는 오브젝트의 스크립트

    입력 : 클릭, 특정 키(미정)
    출력 : 반죽 생성
*/
public class PasteMachine : ClickableThing
{
    public GameObject sheet_raw;
    private Line selfLine;
    [SerializeField]
    private ManufactureAdmin manufactureAdmin;
    public float operatingTime = 4.0f, coolTime = -1.0f;
    private bool coolDown = false;
    private int selfLineNumber;

    protected override void Start()
    {
        base.Start();
        selfLine = GetComponentInParent<Line>();
        selfLineNumber = selfLine.LineNumber;
        manufactureAdmin = selfLine.manufactureAdmin;
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
        if(!coolDown) CreateSheet();
    }

    private void CreateSheet(){
        Instantiate(sheet_raw, transform.position, Quaternion.identity);
        coolTime = operatingTime;
        coolDown = true;
    }

    protected override void OnDisable() {
        base.OnDisable();
    }
}
