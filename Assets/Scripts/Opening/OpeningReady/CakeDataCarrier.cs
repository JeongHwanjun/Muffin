using System.Collections.Generic;
using UnityEngine;

public class CakeDataCarrier : MonoBehaviour
{
    public List<CakeData> Datas;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
