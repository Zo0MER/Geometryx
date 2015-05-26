using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

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
        newSlot.GetComponent<EquationSlot>().index = transform.childCount;
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

    public void UpdateFormula()
    {
        string formula = "";

        foreach (var slot in slots)
        {
            if (slot.item)
            {
                formula += slot.item.GetComponentInChildren<Text>().text;   
            }
        }
        FindObjectOfType<GraphicLineGenerator>().formula = formula;
    }

    private List<EquationSlot> FreeSlots()
    {
        return slots.FindAll(slot => slot.item == null);
    }

    public void MoveItemsToRight(EquationSlot startSlot)
    {
        int startSlotPos = startSlot.index;
        for (int i = startSlotPos; i < slots.Count - 1; i++)
        {
            slots[i].item.GetComponent<EquationPiece>().PutOnSlot(slots[i+1].transform);
        }

    }

    public void MoveItemsToLeft(EquationSlot startSlot)
    {
        int startSlotPos = startSlot.index;
        for (int i = startSlotPos; i < slots.Count - 1; i++)
        {
            slots[i+1].item.GetComponent<EquationPiece>().PutOnSlot(slots[i].transform);
        }
    }
}
