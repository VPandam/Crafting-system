using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    CraftSystemManager craftSystemManager;

    private void Start()
    {
        craftSystemManager = CraftSystemManager.sharedInstance;
    }
    public void Craft()
    {
        craftSystemManager.CraftButtonClick();
    }
}
