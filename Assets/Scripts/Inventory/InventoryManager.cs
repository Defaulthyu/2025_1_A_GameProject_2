using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;            //싱글톤 패턴

    [Header("Inventory Setting")]
    public int inventorySize = 20;              //인벤토리 슬롯 개수
    public GameObject inventoryUI;              //인벤토리 UI 패널
    public Transform itemSlotParent;            //슬롯들이 들어갈 부모 오브젝트
    public GameObject itemSlotPrefab;           //슬롯 프리펩

    [Header("Input")]
    public KeyCode inventoryKey = KeyCode.I;    //인벤토리 열기 키
    private List<InventorySlot> slots = new List<InventorySlot>();  //모든 슬롯 리스트
    private bool isInventoryOpen = false;               //인벤토리가 열려있는지 확인

    private void Awake()
    {
        if(Instance == null) Instance = this;       //싱글톤 설정
        else Destroy(gameObject);
    }

    private void Start()
    {
        CreateInventorySlots();
        inventoryUI.SetActive(false);           //시작할때 숨김
    }

    private void Update()
    {
        if(Input.GetKeyDown(inventoryKey))
        {
            ToggleInventory();
        }
    }

    //인벤토리 슬롯들을 생성하는 함수
    void CreateInventorySlots()
    {
        for(int i = 0; i< inventorySize; i++)
        {
            //프리펩으로 슬롯 생성
            GameObject slotObj = Instantiate(itemSlotPrefab, itemSlotParent);
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            slots.Add(slot);            //리스트에 추가
        }
    }

    //인벤토리 UI를 열거나 닫는 함수
    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryUI.SetActive(isInventoryOpen);

        if(isInventoryOpen)         //인벤토리가 열리면 커서 보이기
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    //아이템을 인벤토리에 추가 함수
    public bool AddItem(ItemData item, int amount = 1)
    {
        foreach(InventorySlot slot in slots)            //1단계: 이미 있는 아이템에 추가 시도(스택)
        {
            if(slot.item == item && slot.amount < item.maxStack)        //같은 아이템 최대스택보다 적으면
            {
                int spaceLeft = item.maxStack - slot.amount;            //남은 공간 계산
                int amountToAdd = Mathf.Min(amount, spaceLeft);         //추가 할 개수
                slot.AddAmount(amountToAdd);
                amount -= amountToAdd;

                if(amount < 0)
                {
                    return true;
                }
            }
        }

        foreach(InventorySlot slot in slots)            //2단계: 빈 슬롯에 추가
        {
            if(slot.item == null)
            {
                slot.SetItem(item, amount);
                return true;
            }
        }

        Debug.Log("인벤토리가 가득 참");
        return false;
    }

    //아이템을 인벤토리에 제거 함수
    public void RemoveItem(ItemData item, int amount = 1)
    {
        foreach(InventorySlot slot in slots)
        {
            if(slot.item == item)
            {
                slot.RemoveAmount(amount);
                return;
            }
        }
    }

    //특정 아이템의 총 개수를 반환하는 함수
    public int GetItemCount(ItemData item)
    {
        int count = 0;
        foreach(InventorySlot slot in slots)
        {
            if(slot.item == item)
            {
                count += slot.amount;
            }
        }
        return count;
    }
}
