using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    // UI에서 사용할 스프라이트
    public Sprite Cake, Arrow, CompleteArrow, FailedArrow;
    // UI요소
    private VisualElement root, container;
    // 정답 레시피
    private List<List<recipeArrow>> RecipeDirectionList = new List<List<recipeArrow>>();
    // 항목당 정답 개수 리스트. 현재 커맨드 길이와 비교한다.
    private List<int> RecipeCorrectList = new List<int>(); 
    // 현재 입력된 커맨드의 길이를 기록하는 변수
    private int currentRecipeLength = 0;

    private void Awake()
    {
        // CommandData의 값 변경을 구독함
        CommandData.instance.PropertyChanged += setData;
        // root 찾기
        root = GetComponent<UIDocument>().rootVisualElement;
        root.visible = false;
    }

    public void refreshRecipes()
    {
        Debug.Log("UIHandler : showRecipes");
        try
        {
            container = root.Q<VisualElement>("Recipes");
            // Recipe는 높이가 44px정도 된다. - padding까지 다 포함해서
            container.Clear();

            int recipeNumber = 0; // 레시피 번호를 추적하기 위한 변수. 현재 레시피의 정답 개수를 확인하는데 사용;
            if (RecipeDirectionList.Count <= 0) Debug.LogWarning("레시피가 0개입니다.");
            foreach (var RecipeDirections in RecipeDirectionList)
            {
                // 각 recipe를 생성하고 스타일 설정
                VisualElement recipe = new VisualElement();
                recipe.AddToClassList("recipe-style");

                VisualElement CakeImage = new VisualElement();
                CakeImage.AddToClassList("CakeImage");
                CakeImage.style.backgroundImage = new StyleBackground(Cake);
                recipe.Add(CakeImage);

                VisualElement ArrowsContainer = new VisualElement();
                ArrowsContainer.AddToClassList("Arrows-style");

                for (int i = 0; i < RecipeDirections.Count; i++)
                {
                    // 화살표 이미지를 추가
                    recipeArrow direction = RecipeDirections[i];
                    VisualElement ArrowElement = new VisualElement();
                    ArrowElement.AddToClassList("Arrow-style");
                    // 정답 여부에 따라 다른 화살표 이미지 추가
                    if (currentRecipeLength != RecipeCorrectList[recipeNumber])
                    {
                        // 맞을 가능성이 없는 레시피라면 모두 failed로 표시
                        ArrowElement.style.backgroundImage = new StyleBackground(FailedArrow);
                    }
                    else
                    {
                        // 현재까지의 입력이 모두 정답이라면
                        if (i < currentRecipeLength)
                        {
                            // 현재 화살표가 입력된 상태라면 완료 화살표
                            ArrowElement.style.backgroundImage = new StyleBackground(CompleteArrow);
                        }
                        else
                        {
                            // 아직 입력되지 않았을 경우 일반 화살표
                            ArrowElement.style.backgroundImage = new StyleBackground(Arrow);
                        }
                    }
                    int angle = 90 * ((int)direction + 1); // 0=상, 1=우, 2=하, 3=좌 - 기본 이미지는 좌측 방향임
                    ArrowElement.style.rotate = new StyleRotate(new Rotate(angle));
                    ArrowsContainer.Add(ArrowElement);

                }
                recipe.Add(ArrowsContainer);
                container.Add(recipe);
                recipeNumber++;
            }
            AdjustRecipesHeight();
        }
        catch (Exception e)
        {
            Debug.Log("UI가 아직 활성화되지 않았습니다 : " + e);
        }
    }

    public void showRecipes()
    {
        root.visible = true;
    }

    public void hideRecipes()
    {
        root.visible = false;
    }

    void AdjustRecipesHeight(){
        int recipeCount = RecipeDirectionList.Count;
        int recipeHeight = 44;

        container.style.height = recipeCount * recipeHeight;
    }

    private void setData(){
        RecipeDirectionList = CommandData.instance.Recipes;
        currentRecipeLength = CommandData.instance.InputCommands.Count;
        RecipeCorrectList = CommandData.instance.RecipeCorrectList;

        refreshRecipes();
    }

    private void setData(object sender, System.ComponentModel.PropertyChangedEventArgs e){
        RecipeDirectionList = CommandData.instance.Recipes;
        currentRecipeLength = CommandData.instance.InputCommands.Count;
        RecipeCorrectList = CommandData.instance.RecipeCorrectList;
        refreshRecipes();
    }
}
