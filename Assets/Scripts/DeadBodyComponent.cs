using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBodyComponent : MonoBehaviour
{
    public SpriteRenderer deadAstronaut;
    
    public void fixColor(Color color_)
    {
        deadAstronaut.color = color_;
    }
}
