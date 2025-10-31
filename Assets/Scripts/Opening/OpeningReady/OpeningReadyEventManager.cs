using System;
using UnityEngine;

public class OpeningReadyEventManager : MonoBehaviour
{
    public static OpeningReadyEventManager Instance { get; private set; }
    public event Action<string> OnCloneNewCard;
    public event Action<string> OnEnlistCake;
    public event Action<string> OnDeleteCake;
    public event Action OnCakeDataReadingFail;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void TriggerCloneNewCard(string id)
    {
        Debug.Log("Event : OnUnsetCard");
        OnCloneNewCard?.Invoke(id);
    }

    public void TriggerEnlistCake(string path)
    {
        Debug.Log("Event : OnEnlistCake");
        OnEnlistCake?.Invoke(path);
    }
    public void TriggerDeleteCake(string path)
    {
        Debug.Log("Event : OnDeleteCake");
        OnDeleteCake?.Invoke(path);
    }

    public void TriggerCakeDataReadingFail()
    {
        Debug.Log("Event : OnCakeDataReadingFail");
        OnCakeDataReadingFail?.Invoke();
    }

}
