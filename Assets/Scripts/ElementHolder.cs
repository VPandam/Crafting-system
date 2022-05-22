using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ElementHolder : MonoBehaviour
{
    public Element element;

    Image _image;
    public GameObject movingIcon;
    public GameObject movingIconInstance;
    Canvas canvas;

    bool movingElement;

    [SerializeField]
    Slot[] craftSlots;

    [SerializeField]
    float minimumDistanceToUpdateSlotWhenDrop = 0.5f;
    CraftSystemManager craftSystemManager;

    private void Awake()
    {
        _image = GetComponent<Image>();
        canvas = GetComponentInParent<Canvas>();
        if (element)
            _image.sprite = element.icon;
    }

    private void Start()
    {
        craftSystemManager = CraftSystemManager.sharedInstance;
        craftSlots = craftSystemManager.craftSlots;
    }

    private void LateUpdate()
    {
        if (movingElement)
        {
            movingIconInstance.transform.position = Input.mousePosition;
        }
    }

    public void OnCLickElement()
    {
        //Instantiate a new icon of the element in the original position,
        //Move the icon in the update
        movingIconInstance = Instantiate(movingIcon, this.transform.position,
         movingIcon.transform.rotation, canvas.transform);
        movingIconInstance.GetComponent<Image>().sprite = element.icon;
        movingElement = true;
    }
    public void OnMouseUpElement()
    {
        //Stop moving the icon in the update and destroy it.
        movingElement = false;

        float shortestDistanceToSlot = float.MaxValue;
        Slot closerSlot = null;

        foreach (var slot in craftSlots)
        {
            float distanceToSlot = Vector3.Distance(slot.transform.position, movingIconInstance.transform.position);
            if (distanceToSlot < shortestDistanceToSlot)
            {
                shortestDistanceToSlot = distanceToSlot;
                if (shortestDistanceToSlot < minimumDistanceToUpdateSlotWhenDrop)
                    closerSlot = slot;
            }

        }
        if (closerSlot)
        {
            closerSlot.SetElement(element);
        }
        Destroy(movingIconInstance);
    }

    public void SetElement(Element newElement)
    {
        element = newElement;
        _image.sprite = element.icon;
    }



}
