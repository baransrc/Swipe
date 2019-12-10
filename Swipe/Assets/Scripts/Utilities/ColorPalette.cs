using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

public class ColorPalette : MonoBehaviour
{
    public Color blue;
    public Color green;
    public Color red;
    public Color yellow;
    public Color colorless;

    public Color GetColor(ItemColor itemColor, float alpha = 1f)
    {
        var color = colorless;
        
        switch (itemColor)
        {
            case ItemColor.Colorless:
                break;
            
            case ItemColor.Blue:
                color = blue;
                break;
            
            case ItemColor.Green:
                color = green;
                break;
            
            case ItemColor.Red:
                color = red;
                break;
            
            case ItemColor.Yellow:
                color = yellow;
                break;
        }

        color.a = alpha;
        
        return color;
    }
}
