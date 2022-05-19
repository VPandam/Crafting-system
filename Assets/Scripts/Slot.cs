using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{
    [SerializeField]
    public Image icon;

    public Element currentElement;


    public void SetElement(Element element)
    {
        Debug.Log(element + " setElement of slot " + this.gameObject.name);
        if (element != null)
        {

            icon.sprite = element.icon;
            icon.enabled = true;
            currentElement = element;
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false;
            currentElement = null;
        }

    }

    public void OnClickCraftSlot()
    {
        if (tag == "CraftSlot")
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }
}
