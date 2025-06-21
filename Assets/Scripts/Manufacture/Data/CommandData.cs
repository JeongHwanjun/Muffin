using System;
using System.Collections.Generic;
using System.ComponentModel;

/*
    제작 화면에서, 입력받은 커맨드와 UI가 공동으로 사용하는 데이터를 관리하는 클래스
    사전에 작성된 레시피, 입력 커맨드, 정답 개수 등을 보관하여 변경될 때마다 이벤트를 발생시켜 UI를 갱신한다.
    CommandData에서는 데이터만 관리하고 실질적인 처리는 ManufactureCommandHandler에서 한다.
*/

public class CommandData : INotifyPropertyChanged {

    // 싱글톤 패턴으로 관리
    private static CommandData _instance;
    public static CommandData instance {
        get {
            if(_instance == null){
                _instance = new CommandData();
            }
            return _instance;
        }
    }
    // 입력된 커맨드
    private List<recipeArrow> inputCommands = new List<recipeArrow>();
    // 정답 레시피
    private List<List<recipeArrow>> recipes = new List<List<recipeArrow>>();
    // 입력된 커맨드 - 정답 레시피 비교 후 정답의 개수를 저장하는 리스트
    private List<int> recipeCorrectList = new List<int>();

    // 값 변경시 발생하는 이벤트. UIHandler에서 이 이벤트를 구독해 UI변경에 활용함
    public event PropertyChangedEventHandler PropertyChanged;

    public List<recipeArrow> InputCommands {
        get => inputCommands;
        set {
            inputCommands = value;
            OnPropertyChanged(nameof(InputCommands));
        }
    }

    public List<List<recipeArrow>> Recipes {
        get => recipes;
        set {
            recipes = value;
            OnPropertyChanged(nameof(Recipes));
        }
    }

    public List<int> RecipeCorrectList {
        get => recipeCorrectList;
        set {
            recipeCorrectList = value;
            OnPropertyChanged(nameof(RecipeCorrectList));
        }
    }

    private void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
