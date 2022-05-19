using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystemManager : MonoBehaviour
{
    public static CraftSystemManager sharedInstance;

    RecipesManager recipesManager;
    Recipe[] recipes;

    public Slot[] craftSlots;


    private void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
        else if (sharedInstance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        recipesManager = RecipesManager.sharedInstance;
        recipes = recipesManager.recipes;
    }

    public void Craft()
    {
        Debug.Log("Craft");
        List<Element> elementsToCraft = GetElementsFromSlots();


        foreach (Recipe recipe in recipes)
        {
            bool canBeCrafted = false;
            bool[] necessaryElementsAvailable = new bool[recipe.elements.Length];
            for (var i = 0; i < recipe.elements.Length; i++)
            {
                if (elementsToCraft.Contains(recipe.elements[i]))
                {
                    elementsToCraft.Remove(recipe.elements[i]);
                    necessaryElementsAvailable[i] = true;
                }
            }

            //If every bool is true, means that we have every element to craft the recipe
            canBeCrafted = true;
            foreach (bool boolElement in necessaryElementsAvailable)
            {
                Debug.Log(boolElement);
                if (!boolElement)
                    canBeCrafted = false;

            }
            //Update the slot elements after crafting
            if (canBeCrafted)
            {
                Element[] elementsToCraftArray = new Element[craftSlots.Length];
                int i = 0;
                foreach (var element in elementsToCraft)
                {
                    elementsToCraftArray[i] = element;
                    i++;
                }
                Debug.Log(elementsToCraftArray.Length + "elementsToCraft has");
                UpdateElementsOfSlots(elementsToCraftArray);
            }

            Debug.Log(recipe.name + " Can be crafted = " + canBeCrafted);
        }

    }


    /// <summary>
    /// NO VA!!!!!!!!!!!!!!!!!!!!
    /// </summary>
    /// <param name="elements"></param>
    public void UpdateElementsOfSlots(Element[] elements)
    {
        int slotIndex = 0;
        foreach (Slot slot in craftSlots)
        {
            Debug.Log(slotIndex + " Slot index " + elements.Length + "elements.count");


            slot.SetElement(elements[slotIndex]);

            slotIndex++;
        }
    }

    /// <summary>
    /// Returns a list with every element in the craft slots.
    /// </summary>
    /// <returns></returns>
    public List<Element> GetElementsFromSlots()
    {
        List<Element> elementsToCraft = new List<Element>();
        foreach (Slot slot in craftSlots)
        {
            if (slot.currentElement)
            {
                elementsToCraft.Add(slot.currentElement);
            }

        }
        return elementsToCraft;
    }
}
