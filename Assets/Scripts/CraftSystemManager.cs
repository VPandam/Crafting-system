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

    public void CraftButtonClick()
    {
        Debug.Log("Craft");
        //The elements in the slots
        List<Element> elementsToCraft = GetElementsFromSlots();
        //A copy of the elements in the slots
        List<Element> elementsToCraftCopy = new List<Element>(elementsToCraft);

        Debug.Log(string.Join(" , ", elementsToCraftCopy));


        //Sometimes more than one recipe will be able to be crafted with the elements in the slots
        //We will craft the recipe that uses more elements.
        int lessElementsRemainingAfterCrafting = int.MaxValue;
        Recipe recipeToCraft = null;

        foreach (Recipe recipe in recipes)
        {
            elementsToCraft = new List<Element>(elementsToCraftCopy);
            bool canBeCrafted = CheckIfRecipeCanBeCrafted(recipe, elementsToCraft);

            if (canBeCrafted)
            {
                int ammountOfElementsRemainingAfterCrafting = RemoveUsedElementsFromSlots(recipe, elementsToCraft);
                if (ammountOfElementsRemainingAfterCrafting < lessElementsRemainingAfterCrafting)
                {
                    lessElementsRemainingAfterCrafting = ammountOfElementsRemainingAfterCrafting;
                    recipeToCraft = recipe;
                }
            }

            Debug.Log(recipe.name + " Can be crafted = " + canBeCrafted);
        }

        Craft(recipeToCraft, elementsToCraftCopy);

    }

    void Craft(Recipe recipe, List<Element> elementsToCraft)
    {
        RemoveUsedElementsFromSlots(recipe, elementsToCraft);
        UpdateElementsOfSlots(elementsToCraft);
        Debug.Log("Crafted: " + recipe.name);
        UpdateResultSlot();
    }

    /// <summary>
    /// Update the elements of the slots
    /// </summary>
    /// <param name="newSlotElements"></param>
    public void UpdateElementsOfSlots(List<Element> newSlotElements)
    {
        //To set the elements of the slots we need an array of elements
        //With the size of the number craftslots
        //Thats because when the list has a null item doesnt count as a space in memory.
        //We need to have the space in memory to say it is null 
        Element[] elementsToCraftArray = new Element[craftSlots.Length];

        //Set the elements of the array with the elements of the list
        int i = 0;
        foreach (var element in newSlotElements)
        {
            elementsToCraftArray[i] = element;
            i++;
        }

        //Set the elements of the slots with the elements of the array
        int slotIndex = 0;
        foreach (Slot slot in craftSlots)
        {
            slot.SetElement(elementsToCraftArray[slotIndex]);
            slotIndex++;
        }
    }

    void UpdateResultSlot()
    {

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

    bool CheckIfRecipeCanBeCrafted(Recipe recipe, List<Element> elementsToCraft)
    {
        bool canBeCrafted = true;
        for (var i = 0; i < recipe.elements.Length; i++)
        {
            if (elementsToCraft.Contains(recipe.elements[i]))
            {
                elementsToCraft.Remove(recipe.elements[i]);
            }
            else
            {
                canBeCrafted = false;
            }
        }
        return canBeCrafted;
    }

    /// <summary>
    /// Checks what elements are used in the recipe and remove them from the list.
    /// </summary>
    /// <param name="recipe"></param>
    /// <param name="elementsToCraft"></param>
    /// <returns></returns>
    int RemoveUsedElementsFromSlots(Recipe recipe, List<Element> elementsToCraft)
    {
        for (var i = 0; i < recipe.elements.Length; i++)
        {
            if (elementsToCraft.Contains(recipe.elements[i]))
            {
                elementsToCraft.Remove(recipe.elements[i]);
            }
        }
        return elementsToCraft.Count;
    }
}
