using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class OpeningTimeManager : MonoBehaviour
{
    public OpeningTimeData openingTimeData;

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
        openingTimeData = GetComponentInChildren<OpeningTimeData>();
        if(manufactureAdmin == null && manufactureAdminPrefab != null){
            manufactureAdmin = Instantiate(manufactureAdminPrefab, transform);
            manufactureAdmin.transform.SetLocalPositionAndRotation(Vector3.zero, quaternion.identity);
            manufactureAdmin.GetComponent<ManufactureAdmin>().Initialize(maxLine, this);
        }
        if(salesAdmin == null && salesAdminPrefab != null){
            salesAdmin = Instantiate(salesAdminPrefab, transform);
            salesAdmin.transform.SetLocalPositionAndRotation(new Vector3(salesX, salesY, 0), quaternion.identity);
            salesAdmin.GetComponent<SalesAdmin>().Initialize(openingTimeData, this);
        }

        
    }

    public void UpdateIngredient(string ingredientName, int usage){
        // 여기선 변화량만 전달하고, ingredientManager에서 기존의 사용량 + 변화량을 반영한다.
        openingTimeData.UpdateIngredient(ingredientName, usage);
    }

    public void UpdateCake(int cakeIndex, int quantityChange, int salesChange){
        // 여기선 변화량만 전달하고, cakeManager에선 기존의 수량, 판매량 + 번화량을 반영한다.
        openingTimeData.UpdateCakeData(cakeIndex, quantityChange, salesChange);
    }

    public List<Cake> GetCakes(){
        return openingTimeData.GetCakeData();
    }
}
