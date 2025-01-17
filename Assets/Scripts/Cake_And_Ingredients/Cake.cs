using System;
using System.Collections.Generic;

[Serializable]
public class Cake{
    public string name;
    public int quantity;
    public int sales;
    public int price;
    public List<int> recipe = new List<int>();

    public event Action<int> OnQuantityChanged;
    public event Action<int> OnSalesChanged;

    public void SetQuantity(int newQuantity){
        if(newQuantity != quantity){
            quantity = newQuantity;
            OnQuantityChanged?.Invoke(quantity);
        }
    }

    public void SetSales(int newSales){
        if(newSales != sales){
            sales = newSales;
            OnSalesChanged?.Invoke(sales);
        }
    }
}