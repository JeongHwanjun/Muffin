using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrows : MonoBehaviour
{
    [SerializeField] private GameObject ArrowImage;
    // 제공받은 recipeArrows에 따라 화살표를 보여주는 것임!
    public void RefreshArrows(List<recipeArrow> arrows)
    {
        for(int i=transform.childCount - 1;i>=0;i--){ Destroy(transform.GetChild(i).gameObject); }
        foreach (recipeArrow arrow in arrows)
        {
            GameObject child = Instantiate(ArrowImage, transform);
            child.transform.rotation = Quaternion.Euler(0, 0, (int)arrow * 90);
        }
    }
}
