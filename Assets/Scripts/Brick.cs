using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;
    
    public int PointValue;

    //change colors
    Color ColorValue;

    void Start()
    {
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();  
        switch (PointValue)
        {
            case 1 :
                block.SetColor("_BaseColor", HexToColor("#E7CBCB"));
                break;
            case 2:
                block.SetColor("_BaseColor", HexToColor("#C88EA7"));
                break;
            case 5:
                block.SetColor("_BaseColor", HexToColor("#643843"));
                break;
            default:
                block.SetColor("_BaseColor", HexToColor("#99627A"));
                break;
        }
        renderer.SetPropertyBlock(block);
    }
    private void OnCollisionEnter(Collision other)
    {
        onDestroyed.Invoke(PointValue);
        
        //slight delay to be sure the ball have time to bounce
        Destroy(gameObject, 0.2f);
    }
    Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

}
