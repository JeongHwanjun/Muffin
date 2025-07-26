using UnityEngine;

public class Counter : MonoBehaviour
{
    public ManufactureEventManager manufactureEventManager;
    public SalesEventManager salesEventManager;
    public OpeningTimeManager openingTimeManager;

    /* 통계용 변수들 */
    public int totalSales = 0; // 총 판매 수량
    public int totalCost = 0; // 총 생산 비용
    public int totalGold = 0; // 매출
    public int profit{ get { return totalGold - totalCost; } }
    public int totalFailureManufacture = 0; // 생산 실패 횟수
    public int totalSuccessManufacture = 0; // 생산 성공 횟수
    public int totalFailureSales = 0; // 판매 실패 횟수
    public int totalSuccessSales = 0; // 판매 성공 횟수

    void Start()
    {
        // 이벤트 구독
        manufactureEventManager.OnPaste += CountBaseCost; // 기본 비용을 계산함
        manufactureEventManager.OnCommandValid += CountSuccessManufacture; // 생산 성공 횟수와 케이크당 비용을 계산함
        manufactureEventManager.OnCommandFailed += CountFailureManufacture; // 생산 실패 횟수를 계산함
        salesEventManager.OnServeSuccess += CountSuccessSales; // 판매 성공 횟수 계산
        salesEventManager.OnServeFailed += CountFailureSales; // 판매 실패 횟수 계산
        salesEventManager.OnConsumeCake += CountConsumeCake; // 매출을 계산함

    }

    void CountBaseCost(){ totalCost += openingTimeManager.GetBaseCost(); }

    void CountSuccessManufacture(int cakeIndex)
    {
        totalSuccessManufacture++;
        totalCost += openingTimeManager.GetCakes()[cakeIndex].cost;
    }

    void CountFailureManufacture() { totalFailureManufacture++; }

    void CountSuccessSales() { totalSuccessSales++; }
    void CountFailureSales() { totalFailureSales++; }

    void CountConsumeCake(int cakeIndex, int consumeQuantity)
    {
        totalSales += consumeQuantity;
        totalGold += openingTimeManager.GetCakes()[cakeIndex].price * consumeQuantity;
    }
}
