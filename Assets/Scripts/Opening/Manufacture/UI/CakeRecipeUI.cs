using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CakeRecipeUI : MonoBehaviour
{
    [SerializeField] private Image cakeImage;
    [SerializeField] private ArrowUI arrows;

    public void Initialize(Cake cake, int index)
    {
        cakeImage.sprite = cake.sprite;
        arrows.Initialize(cake.recipe, index);
    }
}
