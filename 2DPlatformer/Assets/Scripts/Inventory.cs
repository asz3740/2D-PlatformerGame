using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //private AudioManager theAudio;
    // public string key_Sound;
    // public string enter_Sound;
    // public string cancel_Sound;
    // public string open_Sound;
    // public string beep_Sound;

    private InventorySlot[] slots;
    private List<Item> inventoryItemList;
    private List<Item> inventoryTabList;

    public Text Description_Text;
    public string[] tabDescription;

    public Transform tf;

    public GameObject go;
    public GameObject[] selectedTabImage;

    private int selectedItem;
    private int selectedTab;

    private bool activated;
    private bool tabActivated;
    private bool itemActivated;
    private bool stopKeyInput;
    private bool preventExec;
    
    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    void Start()
    {
       //theAudio = FindObjectOfType<AudioManager>();
       inventoryItemList = new List<Item>();
       inventoryTabList = new List<Item>();
       slots = tf.GetComponentsInChildren<InventorySlot>();
       inventoryItemList.Add(new Item(10001,"주문서", "이이이잉", Item.ItemType.Quest));
       //itemList.Add(new Item(10002,"책", "ㅁㄴ리ㅏㅓㅁ;ㅣ러", Item.ItemType.Quest));
       inventoryItemList.Add(new Item(10003,"빨간 폰션", "체력 50", Item.ItemType.Combat));
       //itemList.Add(new Item(10004,"파란 폰션", "마나 50", Item.ItemType.Combat));
    }

    // 탭 활성화
    public void ShowTab()
    {
        RemoveSlot();
        SelectedTab();
    }
    
    // 인벤토리 슬롯 초기화
    public void RemoveSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    }
    // 선택된 탬을 제외하고 다른 모든 탬의 컬러 알파값 0으로 조정
    public void SelectedTab()
    {
        StopAllCoroutines();
        Color color = selectedTabImage[selectedTab].GetComponent<Image>().color;
        color.a = 0f;
        for (int i = 0; i > selectedTabImage.Length; i++)
        {
            selectedTabImage[i].GetComponent<Image>().color = color;
        }

        Description_Text.text = tabDescription[selectedTab];
        StartCoroutine(SelectedTabEffectCoroutine());
    }
    // 선택된 탬 반짝임 효과
    IEnumerator SelectedTabEffectCoroutine()
    {
        while (tabActivated)
        {
            Color color = selectedTabImage[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                selectedTabImage[0].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                selectedTabImage[0].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            
            yield return new WaitForSeconds(0.3f);
        }
    }

    // 아이템 활성화 (inventoryTabList에 조건에 맞는 아이템들만 넣어주고, 인벤토리 슬롯에 출력)
    public void ShowItem()
    {
        inventoryTabList.Clear();
        RemoveSlot();
        selectedItem = 0;

        
        // 템에 따른 템 분류 후 인벤토리 탭 리스트에 추가
        switch (selectedTab)
        {
            case 0:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Combat == inventoryItemList[i].itemType)
                    {
                        inventoryTabList.Add(inventoryItemList[i]);
                    }
                }

                break;

            case 1:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Quest == inventoryItemList[i].itemType)
                    {
                        inventoryTabList.Add(inventoryItemList[i]);
                    }
                }
                
                break;
        }

        // 인벤토리 탭 리스트의 내용을 인벤토리 슬롯에 추가
        for (int i = 0; i < inventoryTabList.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].Additem(inventoryTabList[i]);
        }

        SelectedItem();
    }
    // 선택된 아이템을 제외하고 다른 모든 탬의 컬러 알파값을 0으로 조정
    public void SelectedItem()
    {
        StopAllCoroutines();
        if (inventoryTabList.Count > 0)
        {
            Color color = slots[0].select_Item.GetComponent<Image>().color;
            color.a = 0f;
            for (int i = 0; i < inventoryTabList.Count; i++)
                slots[i].select_Item.GetComponent<Image>().color = color;
            Description_Text.text = inventoryTabList[selectedItem].itemDescription;
            StartCoroutine(SelectedItemEffectCoroutine());
        }
        else
        {
            Description_Text.text = "아이템을 소유하고 있지 않습니다.";
        }
    }
    // 선택된 아이템 반짝임 효과
    IEnumerator SelectedItemEffectCoroutine()
    {
        while (itemActivated)
        {
            Color color = slots[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                slots[selectedItem].select_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                slots[selectedItem].select_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            
            yield return new WaitForSeconds(0.3f);
        }
    }
    
    void Update()
    {
        if (!stopKeyInput)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                activated = !activated;

                if (activated)
                {
                    //theAudio.Play(open_Sound);
                    go.SetActive(true);
                    selectedTab = 0;
                    tabActivated = true;
                    itemActivated = false;
                    ShowTab();
                }
                else
                {
                    //theAudio.Play(cancel_Sound);
                    StopAllCoroutines();
                    go.SetActive(false);
                    tabActivated = false;
                    itemActivated = false;
                }
            }

            if (activated)
            {
                // 탭 활성화 시 키입력 처리
                if (tabActivated)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (selectedTab < selectedTabImage.Length -1)
                        {
                            selectedTab++;
                        }
                        else
                        {
                            selectedTab = 0;
                            //theAudio.Play(key_sound);
                            SelectedTab();
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedTab > 0)
                        {
                            selectedTab--;
                        }
                        else
                        {
                            selectedTab = selectedTabImage.Length - 1;
                            //theAudio.Play(key_sound);
                            SelectedTab();
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.Z))
                    {
                        Color color = selectedTabImage[selectedTab].GetComponent<Image>().color;
                        color.a = 0.25f;
                        selectedTabImage[selectedTab].GetComponent<Image>().color = color;
                        itemActivated = true;
                        tabActivated = false;
                        preventExec = true;
                        ShowItem();
                    }
                }
                // 아아템 활성화 시 키입력 처리
                else if (itemActivated)
                {
                    if (inventoryTabList.Count > 0)
                    {
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 4)
                                selectedItem += 4;
                            else
                                selectedItem %= 2;
                            //theAudio.Play(key_Sound);
                            SelectedItem();

                        }
                        else if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            if (selectedItem > 1)
                                selectedItem -= 4;
                            else
                                selectedItem = inventoryTabList.Count - 1 - selectedItem;
                            //theAudio.Play(key_Sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 1)
                                selectedItem++;
                            else
                                selectedItem = 0;
                            //theAudio.Play(key_Sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectedItem > 0)
                                selectedItem--;
                            else
                                selectedItem = inventoryTabList.Count - 1;
                            //theAudio.Play(key_Sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.Z) && !preventExec)
                        {
                            if (selectedTab == 0)
                            {

                            }
                            else if (selectedTab == 1)
                            {
                                //theAudio.Play(enter_Sound);
                                stopKeyInput = true;
                                // 선택지 호출
                            }
                            else
                            {
                                //theAudio.Play(beep_sound);
                            }
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        //theAudio.Play(cancel_Sound);
                        StopAllCoroutines();
                        itemActivated = false;
                        tabActivated = true;
                        ShowTab();
                    }
                }
                // 중복 실행 방지
                if (Input.GetKeyUp(KeyCode.Z))
                {
                    preventExec = false;
                }
            }
        }
    }
    
}
