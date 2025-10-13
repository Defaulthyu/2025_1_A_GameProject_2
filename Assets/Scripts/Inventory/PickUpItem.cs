using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : InteractableObject
{
    public ItemData itemData;       //이 오브젝트가 가진 아이템 데이터
    public int amount = 1;

    public override void Interact()
    {
        base.Interact();

        if(InventoryManager.Instance != null)
        {
            bool added = InventoryManager.Instance.AddItem(itemData, amount);

            if(added)
            {
                Destroy(gameObject);
            }
        }
    }
}
