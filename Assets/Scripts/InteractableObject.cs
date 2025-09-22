using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("��ȣ�ۿ� ����")]
    public string objectName = "������";
    public string interactionText = "[E] ��ȣ�ۿ�";
    public InteractionType interactionType = InteractionType.item;          //Ÿ���� �켱 item

    [Header("���̶���Ʈ ����")]
    public Color highlightColor = Color.yellow;
    public float highlightIntensity = 1.5f;

    public Renderer objectRenderer;
    private Color originalColor;
    private bool isHighlighted = false;
    
    public enum InteractionType
    {
        item,                       //������ (����, ���� ��)
        Machine,                    //��� (����, ��ư ��)
        building,                   //�ǹ� (��, ���� ��)
        NPC,                        //NPC
        Collectible                 //����ǰ
    }


    protected virtual void start()
    {
        objectRenderer = GetComponent<Renderer>();
        if(objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;

        }
        gameObject.layer = 8;                   //layer8 = interactable�� ����
    }

    public virtual void OnplayerEnter()
    {
        Debug.Log($"[{objectName}) ������");
        HighlightObject();
    }

    public virtual void OnPlayerExit()
    {
        Debug.Log($"[{objectName}] �������� ���");
        RemoveHighlight();
    }

    public string GetInteractionText()
    {
        return interactionText;
    }


    public virtual void Interact()
    {
        switch(interactionType)
        {
            case InteractionType.item:
                CollectItem();
                break;
            case InteractionType.Machine:
                OperateMachine();
                break;
            case InteractionType.building:
                AccessBuilding();
                break;
            case InteractionType.Collectible:
                CollectItem();
                break;

        }
    }

    protected virtual void HighlightObject()                            //���� �Լ��� ���̶���Ʈ ����
    {
        if (objectRenderer != null && !isHighlighted)
        {
            objectRenderer.material.color = highlightColor;
            objectRenderer.material.SetFloat("_Emission", highlightIntensity);
            isHighlighted = true;
        }
    }

    protected virtual void RemoveHighlight()
    {
        if(objectRenderer != null && isHighlighted)
        {
            objectRenderer.material.color = originalColor;
            objectRenderer.material.SetFloat("_Emission", 0f);
            isHighlighted = false;
        }
    }

    protected virtual void CollectItem()
    {
        Debug.Log($"{objectName}��(��) ȹ���߽��ϴ�!");
        Destroy(gameObject);
    }

    protected virtual void OperateMachine()
    {
        Debug.Log($"{objectName}��(��) �۵����׽��ϴ�");
        if(objectRenderer != null)
        {
            objectRenderer.material.color = Color.green;
        }
    }

    protected virtual void AccessBuilding()
    {
        Debug.Log($"{objectName}��(��) �� �����߽��ϴ�.");
        transform.Rotate(Vector3.up * 90f);
    }

    protected virtual void TalkToNPC()
    {
        Debug.Log($"{objectName}�� ��ȭ�� �����մϴ�");
    }
}
