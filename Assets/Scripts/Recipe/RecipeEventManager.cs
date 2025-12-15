using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeEventManager : MonoBehaviour
{
    public static RecipeEventManager Instance { get; private set; }
    public event Action OnRefreshUI;
    public event Action OnPanelExpand;
    public event Action<Ingredient> OnIngredientAdd; // 재료 추가 이벤트. 추가에 따라 UI 갱신과 스탯 갱신이 이루어짐
    public event Action<IngredientType> OnIngredientSub; // 재료를 빼는 이벤트. 가장 최근에 추가한 재료 하나가 빠지게 된다.
    public event Action OnRemoveLastIngredient;
    public event Action<Ingredient> OnIngredientClick;
    public event Action<Ingredient> OnIngredientHover;
    public event Action OnIngredientHoverExit;
    public event Action<RectTransform> OnSaveCake;
    public event Action OnCloneCakePanel;
    public event Action<IngredientType> OnClickNextButton;
    public event Action OnMoveToNextStage;
    public event Action OnMoveToPrevStage;
    public event Action<Stack<ComboRule>> OnPrintComboScript;
    public event Action OnScriptPlayCompleted;
    public static event Action<Ingredient> OnIngredientDropped;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    public void TriggerIngredientAdd(Ingredient newIngredient)
    {
        Debug.LogFormat("Event : OnIngredientAdd {0}", newIngredient);
        OnIngredientAdd?.Invoke(newIngredient);
    }
    public void TriggerIngredientSub(IngredientType Stage)
    {
        Debug.LogFormat("Event : OnIngredientSub");
        OnIngredientSub?.Invoke(Stage);
    }
    public void TriggerRemoveLastIngredient()
    {
        Debug.LogFormat("Event : OnRemoveLastIngredient");
        OnRemoveLastIngredient?.Invoke();
    }

    public void TriggerRefreshUI()
    {
        Debug.LogFormat("Event : OnRefreshUI");
        OnRefreshUI?.Invoke();
    }

    public void TriggerPanelExpand()
    {
        Debug.LogFormat("Event : OnPanelExpand");
        OnPanelExpand?.Invoke();
    }

    public void TriggerSaveCake(RectTransform clonedUI)
    {
        Debug.LogFormat("Event : OnSaveCake");
        OnSaveCake?.Invoke(clonedUI);
    }
    public void TriggerCloneCakePanel()
    {
        Debug.LogFormat("Event : OnCloneCakePanel");
        OnCloneCakePanel?.Invoke();
    }

    public static void TriggerIngredientDropped(Ingredient droppedIngredient)
    {
        Debug.LogFormat("Event : OnIngredientDropped {0}", droppedIngredient);
        OnIngredientDropped?.Invoke(droppedIngredient);
    }

    public void TriggerIngredientClick(Ingredient clickedIngredient)
    {
        Debug.LogFormat("Event : OnIngredientClick");
        OnIngredientClick?.Invoke(clickedIngredient);
    }

    public void TriggerIngredientHover(Ingredient hoverIngredient)
    {
        Debug.LogFormat("Event : OnIngredientHover");
        OnIngredientHover?.Invoke(hoverIngredient);
    }

    public void TriggerClickNextButton(IngredientType Stage)
    {
        Debug.LogFormat("Event : OnClickNextButton");
        OnClickNextButton?.Invoke(Stage);
    }
    public void TriggerMoveToNextStage()
    {
        Debug.LogFormat("Event : OnMoveToNextStage");
        OnMoveToNextStage?.Invoke();
    }

    public void TriggerMoveToPrevStage()
    {
        Debug.LogFormat("Event : OnMoveToPrevStage");
        OnMoveToPrevStage?.Invoke();
    }

    public void TriggerPrintComboScript(Stack<ComboRule> comboRules)
    {
        Debug.LogFormat("Event : OnPrintComboScript");
        OnPrintComboScript?.Invoke(comboRules);
    }

    public void TriggerScriptPlayCompleted()
    {
        Debug.LogFormat("Event : OnScriptPlayCompleted");
        OnScriptPlayCompleted?.Invoke();
    }

    public void TriggerIngredientHoverExit()
    {
        Debug.LogFormat("Event : OnIngredientHoverExit");
        OnIngredientHoverExit?.Invoke();
    }
}
