using System;
using UnityEngine;

public class OpeningReadyEventManager : MonoBehaviour
{
    public static OpeningReadyEventManager Instance { get; private set; }
    public event Action OnSetNewCard;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    
    public void TriggerSetNewCard()
    {
        Debug.Log("Event : OnSetNewCard");
        OnSetNewCard?.Invoke();
    }

}
