using UnityEngine;

public class NextButton : MonoBehaviour
{
    public GameObject currentStage, NextStage;

    public void MoveToNextStage()
    {
        Debug.LogFormat("MoveStage from {0} to {1}", currentStage.name, NextStage.name);
        currentStage.SetActive(false);
        NextStage.SetActive(true);
    }
}
