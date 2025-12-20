using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CustomerWaitingBar : MonoBehaviour
{
    // 이벤트를 구독해서 손님이 변경된 걸 감지
    // 해당 손님의 정보를 지속적으로 요청해서 남은 대기 시간을 탐지
    public Image customerImage;
    public Image progressBar;
    public CustomerManager customerManager;

    private SalesEventManager salesEventManager;
    private Customer currentCustomer;
    private CanvasGroup canvasGroup;
    private float maximumWaitingTime = 0.0f;
    void Start()
    {
        salesEventManager = SalesEventManager.Instance;
        canvasGroup = GetComponent<CanvasGroup>();

        salesEventManager.OnCustomerCreated += CheckCustomer;
        salesEventManager.OnCustomerDeleted += CheckCustomer;
    }

    void Update()
    {
        if(currentCustomer == null)
        {
            // 손님이 없을 경우 처리
            canvasGroup.alpha = 0;
            return;
        }

        progressBar.fillAmount = currentCustomer.GetWaitingTime() / maximumWaitingTime;
    }

    private void CheckCustomer(Customer newCustomer)
    {
        // customer 생성시 체크
        CheckCustomer();
    }

    private void CheckCustomer()
    {
        // customer 삭제시 체크
        // 첫 번째 customer(체크할 대상)을 가져옴
        List<GameObject> customers = customerManager.customers;
        if (!customers.Any())
        {
            currentCustomer = null;
            maximumWaitingTime = 0.0f;
            return;
        }
        canvasGroup.alpha = 1;
        customerImage.sprite = customers.First().GetComponent<Image>().sprite;
        currentCustomer = customers.First().GetComponent<Customer>();
        maximumWaitingTime = currentCustomer.GetMaximumWaitingTime();
    }
}
