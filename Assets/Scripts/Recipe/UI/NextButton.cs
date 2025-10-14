using UnityEngine;

public class NextButton : MonoBehaviour
{
    public GameObject currentStage, NextStage;
    public GameObject origin, targetParent;

    public void MoveToNextStage()
    {
        Debug.LogFormat("MoveStage from {0} to {1}", currentStage.name, NextStage.name);
        currentStage.SetActive(false);
        NextStage.SetActive(true);
    }

    public void MoveToCombo()
    {
        if(origin != null && targetParent != null) Instantiate(origin, targetParent.transform);
    }
}
