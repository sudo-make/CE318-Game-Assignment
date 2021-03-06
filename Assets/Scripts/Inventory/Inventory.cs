﻿using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    /// <summary>
    ///     Original script based on the work by "GameDevChef" https://www.youtube.com/watch?v=aS7OqRuwzlk
    ///     Adjusted by 1806094
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<InventoryItemWrapper> items = new List<InventoryItemWrapper>();

        public InventoryUI inventoryUI;

        private readonly Dictionary<InventoryItem, int> itemToCountMap = new Dictionary<InventoryItem, int>();
        private          PlayerEquipmentController      playerEquipment;

        public void InitInventory(PlayerEquipmentController playerEquipmentController)
        {
            playerEquipment = playerEquipmentController;
            foreach (var t in items) itemToCountMap.Add(t.GetItem(), t.GetItemCount());
        }

        public void OpenInventoryUI()
        {
            inventoryUI.gameObject.SetActive(true);
            inventoryUI.InitInventory(this);
        }

        public void AssignItem(InventoryItem item)
        {
            item.AssignItemToPlayer(playerEquipment);
        }

        public Dictionary<InventoryItem, int> GetAllItemsMap()
        {
            return itemToCountMap;
        }

        public void AddItem(InventoryItem item, int count)
        {
            if (itemToCountMap.TryGetValue(item, out var currentItemCount))
                itemToCountMap[item] = currentItemCount + count;
            else
                itemToCountMap.Add(item, count);

            inventoryUI.CreateOrUpdateSlot(this, item, count);
        }

        public void RemoveItem(InventoryItem item, int count)
        {
            if (itemToCountMap.TryGetValue(item, out var currentItemCount))
            {
                itemToCountMap[item] = currentItemCount - count;
                if (currentItemCount - count <= 0)
                    inventoryUI.DestroySlot(item);
                else
                    inventoryUI.UpdateSlot(item, currentItemCount - count);
            }
            else
            {
                Debug.Log($"Cannot remove {item}. This item is not in the inventory!");
            }
        }
    }
}