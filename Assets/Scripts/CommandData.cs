using System;
using System.Collections.Generic;
using System.ComponentModel;

/*
    제작 화면에서, 입력받은 커맨드와 UI가 공동으로 사용하는 데이터를 관리하는 클래스
    사전에 작성된 레시피, 입력 커맨드, 정답 개수 등을 보관하여 변경될 때마다 이벤트를 발생시켜 UI를 갱신한다.
*/

public class CommandData : INotifyPropertyChanged {
    private static CommandData _instance;
    public static CommandData instance {
        get {
            if(_instance == null){
                _instance = new CommandData();
            }
            return _instance;
        }
    }
    private List<int> inputCommands = new List<int>();
    private List<List<int>> recipes = new List<List<int>>();
    private List<int> recipeCorrectList = new List<int>();

    public event PropertyChangedEventHandler PropertyChanged;

    public List<int> InputCommands {
        get => inputCommands;
        set {
            inputCommands = value;
            OnPropertyChanged(nameof(InputCommands));
        }
    }

    public List<List<int>> Recipes {
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
