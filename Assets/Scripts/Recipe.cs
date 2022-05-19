using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public Element[] elements;

    public Element result;

}
