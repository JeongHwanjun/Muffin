using Unity.Mathematics;
using UnityEngine;

public class OpeningTimeManager : MonoBehaviour
{
    public GameObject manufactureAdminPrefab;
    private GameObject manufactureAdmin;

    public GameObject salesAdminPrefab;
    private GameObject salesAdmin;
    public int salesX = -15, salesY = 0;

    private CakeManager cakeManager;
    private IngredientManager ingredientManager;

    public int maxLine = 3;
    void Start()
    {
        cakeManager = GetComponentInChildren<CakeManager>();
        ingredientManager = GetComponentInChildren<IngredientManager>();
        Initialize();
    }

    public void Initialize(){
        if(manufactureAdmin == null){
            manufactureAdmin = Instantiate(manufactureAdminPrefab, transform);
            manufactureAdmin.transform.SetLocalPositionAndRotation(Vector3.zero, quaternion.identity);
            manufactureAdmin.GetComponent<ManufactureAdmin>().Initialize(maxLine, this);
        }
        if(salesAdmin == null){
            salesAdmin = Instantiate(salesAdminPrefab, transform);
            salesAdmin.transform.SetLocalPositionAndRotation(new Vector3(salesX, salesY, 0), quaternion.identity);
        }
    }

    public void UpdateIngredient(string ingredientName, int usage){
        // 여기선 변화량만 전달하고, ingredientManager에서 기존의 사용량 + 변화량을 반영한다.
        ingredientManager.UpdateIngredientData(ingredientName, usage);
    }

    public void UpdateCake(string cakeName, int quantityChange, int salesChange){
        // 여기선 변화량만 전달하고, cakeManager에선 기존의 수량, 판매량 + 번화량을 반영한다.
        cakeManager.UpdateCakeData(cakeName, quantityChange, salesChange);
    }

    public void changeScreen(int screenNumber){

    }
}
