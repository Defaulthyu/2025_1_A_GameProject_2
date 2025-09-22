using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : InteractableObject
{
    [Header("���� ����")]
    public int coinValue = 10;

    protected override void start()
    {
        base.start();
        objectName = "����";
        interactionText = "[E] ���� ȹ��";
        interactionType = InteractionType.item;
    }

    protected override void CollectItem()
    {
        transform.Rotate(Vector3.up * 180f);
        Destroy(gameObject, 0.5f);
    }


}
