using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using TMPro;


public class Inventory : MonoBehaviour
{
    public ItemSO testItem;
    public ItemSO testItem2;

   public GameObject hotbarObj;
   public GameObject inventorySlotParent;
   public GameObject equipmentSlotParent;
   public GameObject container;
    private List<Slot> hotbarSlots = new List<Slot>();
    private List<Slot> inventorySlots = new List<Slot>();
    private List<Slot> EquipmentSlots = new List<Slot>();
    private List<Slot> allSlots = new List<Slot>();
    public Image dragIcon;

    public GameObject ItemDescriptionParent;
    public Image itemDescriptionImage;
    public TextMeshProUGUI DescriptionItemNameTxt;
    public TextMeshProUGUI itemDescriptionTxt;

    private void Awake()
    {
         container.SetActive(!container.activeSelf);
        inventorySlots.AddRange(inventorySlotParent.GetComponentsInChildren<Slot>());
        hotbarSlots.AddRange(hotbarObj.GetComponentsInChildren<Slot>());
        EquipmentSlots.AddRange(equipmentSlotParent.GetComponentsInChildren<Slot>());
        allSlots.AddRange(inventorySlots);
        allSlots.AddRange(hotbarSlots);
        allSlots.AddRange(EquipmentSlots);
         container.SetActive(!container.activeSelf);
    } 

    void Update()
    {
       
    }

    
    
    
    
}
