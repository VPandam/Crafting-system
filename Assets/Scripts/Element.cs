using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Element", fileName = "newElement")]
public class Element : ScriptableObject
{
    public Sprite icon;
    public string _name;

    public Element GetCopy()
    {
        return Instantiate(this);
    }
}
