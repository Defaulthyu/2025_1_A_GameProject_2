using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : InteractableObject
{
    [Header("동전 설정")]
    public int coinValue = 10;

    protected override void start()
    {
        base.start();
        objectName = "동전";
        interactionText = "[E] 동전 획득";
        interactionType = InteractionType.item;
    }

    protected override void CollectItem()
    {
        transform.Rotate(Vector3.up * 180f);
        Destroy(gameObject, 0.5f);
    }


}
