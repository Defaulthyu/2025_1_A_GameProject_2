using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : InteractableObject
{
    [Header("���� ����")]
    public int coinValue = 10;
    public string questTag = "Coin";                //����Ʈ���� ����� �±�

    protected override void start()
    {
        base.start();
        objectName = "����";
        interactionText = "[E] ���� ȹ��";
        interactionType = InteractionType.item;
    }

    protected override void CollectItem()
    {

        //����Ʈ �Ŵ����� ������ �˸�
        if(QuestManager.instance != null)
        {
            QuestManager.instance.AddCollectProgress(questTag);
        }

        transform.Rotate(Vector3.up * 180f);
        Destroy(gameObject, 0.5f);
    }


}
