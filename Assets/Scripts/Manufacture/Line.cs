using UnityEngine;

class Line : MonoBehaviour{
    public int LineNumber = 0;
    public ManufactureAdmin manufactureAdmin;
    
    public void Init(int number, ManufactureAdmin _manufactureAdmin){
        LineNumber = number;
        manufactureAdmin = _manufactureAdmin;
    }
}