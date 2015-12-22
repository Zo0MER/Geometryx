using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Obsolete("This is an obsolete method")]
public class EquationPanel : MonoBehaviour
{

    public GameObject slotPrefab;
    private List<EquationSlot> slots = new List<EquationSlot>();

    void Start()
    {
        AddSlot();
    }

    public void AddSlot()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform);
        slots.Add(newSlot.GetComponent<EquationSlot>());
    }

    public void ItemDroped()
    {
        foreach (var slot in FreeSlots())
        {
            MoveItemsToLeft(slot);
        }
        if (FreeSlots().Count < 1)
        {
            AddSlot();
        }

        UpdateFormula();
    }

    public void RemoveSlot()
    {
        List<EquationSlot> freeSlots = FreeSlots();
        if (freeSlots.Count > 1)
        {
            freeSlots.RemoveAt(freeSlots.Count - 1);
            foreach (var slot in freeSlots)
            {
                slots.Remove(slot);
                Destroy(slot.gameObject);
            }
        }
        
    }

    public void UpdateFormula()
    {
        string formula = "";

        foreach (var slot in slots)
        {
            if (slot.item)
            {
                formula += slot.item.GetComponent<EquationPiece>().Value;   
            }
        }
        FindObjectOfType<GraphicLineGenerator>().Formula = formula;
    }

    private List<EquationSlot> FreeSlots()
    {
        return slots.FindAll(slot => slot.item == null);
    }

    public void MoveItemsToRight(EquationSlot startSlot)
    {
        int startSlotPos = slots.IndexOf(startSlot);
        for (int i = startSlotPos; i < slots.Count - 1; i++)
        {
            if (slots[i].item)
            {
                slots[i].item.GetComponent<EquationPiece>().PutOnSlot(slots[i + 1].transform);
            }
        }

    }

    public void MoveItemsToLeft(EquationSlot startSlot)
    {
        int startSlotPos = slots.IndexOf(startSlot);
        for (int i = startSlotPos; i < slots.Count - 1; i++)
        {
            if (slots[i + 1].item)
            {
                slots[i + 1].item.GetComponent<EquationPiece>().PutOnSlot(slots[i].transform);
            }
        }
    }
}
