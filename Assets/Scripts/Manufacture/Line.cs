using UnityEngine;

public class Line : MonoBehaviour{
    public int LineNumber = 0;
    public bool isLineReady;
    public ManufactureAdmin manufactureAdmin;
    
    public void Init(int number, ManufactureAdmin _manufactureAdmin){
        LineNumber = number;
        manufactureAdmin = _manufactureAdmin;
    }

    public void lineReady(){
        isLineReady = true;
    }
}