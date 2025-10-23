using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CakePanel : MonoBehaviour, IDropHandler
{
    public CakeBuilder cakeBuilder;
    public RecipeEventManager recipeEventManager;
    public Image cakeImage;
    public SOBaseImage baseImage;
    private Stack<GameObject> draggableItems; // 추가된 아이템들. Revert이벤트와 관련됨.
    //private Dictionary<IngredientGroup, Sprite> baseImageDict;

    void Start()
    {
        //recipeEventManager = RecipeEventManager.Instance;
        draggableItems = new();
    }

    void OnEnable()
    {
        recipeEventManager = RecipeEventManager.Instance;
        recipeEventManager.OnRemoveLastIngredient += RemoveLastIngredient;
        

        StatCounter statCounter = GameObject.Find("StatCounter").GetComponent<StatCounter>();
        List<Ingredient> ingredients = statCounter.ingredients;
        if (ingredients.Count <= 0) return;

        // cakeImage 설정
        // StatCounter에서 최근 재료 가져오기
        Ingredient lastIngredient = ingredients.Last();
        // 1. Base라면 해당 Base의 IngredientGroup을 key로 하는 image를 적용
        // 이러면 아무 Base도 넣지 않았을 시 문제 발생. Base를 넣지 않으면 통과되지 않게 변경 필요.
        if(lastIngredient.GetIngredientType() == IngredientType.Base)
        {
            cakeImage.sprite = baseImage.BaseImageDict[lastIngredient.group];
        }
        // 2. 아니라면 무시하기
    }
    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem dropped = eventData.pointerDrag?.GetComponent<DraggableItem>();
        if (dropped != null)
        {
            //RecipeEventManager.TriggerIngredientDropped(dropped.GetComponent<Ingredient>());
            draggableItems.Push(dropped.gameObject);
        }
    }
    
    void OnDisable()
    {
        recipeEventManager.OnRemoveLastIngredient -= RemoveLastIngredient;
    }

    void RemoveLastIngredient()
    {
        // 가장 최근의 재료를 파괴함
        GameObject lastItem;
        draggableItems.TryPop(out lastItem);
        Destroy(lastItem);
    }
}
