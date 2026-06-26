using UnityEngine;

public class Item : MonoBehaviour
{
   public ItemSO item;
   public int amount = 1;

      private void OnTriggerEnter(Collider other)
      {
         if (other.CompareTag("Player"))
         {
               Inventory playerInventory = FindFirstObjectByType<Inventory>();
             
         }
      }
}
