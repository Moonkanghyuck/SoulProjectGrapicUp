using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopManager : MonoBehaviour
{
	private NoticeManager NoticeManager
	{ 
		get
		{
			_noticeManager ??= FindObjectOfType<NoticeManager>();
			return _noticeManager;
		}
	}

	private List<IItem> _buyItems = new List<IItem>();
	private List<ItemBox> _itemBoxs = new List<ItemBox>();

	[SerializeField]
	private Canvas _shopCanvas = null;
	[SerializeField]
	private GameObject _itemBoxPrefeb;
	[SerializeField]
	private Transform _itemBoxParent;
	[SerializeField]
	private ItemInfomation _itemInfomation;
	[SerializeField]
	private ItemSetSO _sellItems;
	[SerializeField]
	private MoneyInventory _moneyInventory;
	[SerializeField]
	private ItemInventory _itemInventory;
	[SerializeField]
	private GameObject _sellPanel;
	[SerializeField]
	private Button _sellButton;
	[SerializeField]
	private Button _buyButton;
	[SerializeField]
	private MoneySO _moneySO;

	private bool _isActive = false;
	private NoticeManager _noticeManager = null;
	private Player _player = null;
	private Merchant _merchant = null;

	public void Start()
	{
		_sellButton.onClick.AddListener(() => SettingSell());
		_buyButton.onClick.AddListener(() => NoneSettingSell());
	}

	public void Update()
	{
		if(_isActive)
		{
			if (Input.GetKeyDown(KeyCode.F))
			{
				NoneSetting();
			}
		}
	}

	public void Setting(Player player, Merchant merchant, ItemSetSO itemSetSO)
	{
		_moneyInventory.SetActiveMoenyCanvas(true);
		_itemInventory.UpdateUI();
		_player = player;
		_merchant = merchant;
		_buyItems = itemSetSO.GetIItemList();
		_player.IsCantAnything = true;
		Invoke("IsActive", 0.1f);
		SetActiveCanvas(true);
		UpdateUI(true);
	}
	public void NoneSetting()
	{
		_sellPanel.gameObject.SetActive(false);
		_moneyInventory.SetActiveMoenyCanvas(false);
		_merchant.EndTrade();
		_player.IsCantAnything = false;
		_isActive = false;
		SetActiveCanvas(false);
	}
	public void OnBuyTab()
	{
		UpdateUI(true);
	}
	public void OnSellTab()
	{
		UpdateUI(false);
	}
	public void OnItemInfomation(ItemBox itembox)
	{
		_itemInfomation.Setting(itembox);
	}
	public void UpdateUI(bool isBuy)
	{
		//������ �ڽ� �ʱ�ȭ
		int itemCount = 0;
		_itemBoxs.Clear();
		for (int i = 0; i < _itemBoxParent.childCount; ++i)
		{
			_itemBoxParent.GetChild(i).gameObject.SetActive(false);
		}

		if (isBuy) //������ ���Ż���
		{
			//�������� ���� ����
			itemCount = _buyItems.Count;
			for (int i = 0; i < itemCount; ++i)
			{
				ItemBox itembox = PoolItemBox();
				itembox.Setting(_buyItems[i], BuyItem, OnItemInfomation, true);
				_itemBoxs.Add(itembox);
			}
		}
		else //������ �ǸŻ���
		{
			//�������� ���� ����
			List<IItem> items = _sellItems.GetIItemList();
			itemCount = items.Count;

			for (int i = 0; i < itemCount; ++i)
			{
				ItemBox itembox = PoolItemBox();
				itembox.Setting(items[i], SellItem, OnItemInfomation);
				_itemBoxs.Add(itembox);
			}
		}

		
		_itemInfomation.UpdateUI();
	}
	private void SetActiveCanvas(bool isboolean)
	{
		_shopCanvas.gameObject.SetActive(isboolean);
		if (isboolean)
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		else
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
	private void BuyItem(ItemBox item)
	{
		if(item.Item.Price > _moneySO._money)
		{
			NoticeManager.Notice("���� �����մϴ�");
			return;
		}
		_player.AddItem(item.Item);
		_moneySO.AddMoney(-item.Item.Price);
		_itemInventory.UpdateUI();
	}
	private void SellItem(ItemBox item)
	{
		Debug.Log("������ �Ǹ�");
	}
	
	/// <summary>
	/// ���� Ȱ��ȭ ����
	/// </summary>
	private void IsActive()
	{
		_isActive = true;
	}

	/// <summary>
	/// �Ǹ� â�� Ų��
	/// </summary>
	private void SettingSell()
	{
		_sellPanel.gameObject.SetActive(true);
	}

	/// <summary>
	/// �Ǹ�â�� ����
	/// </summary>
	private void NoneSettingSell()
	{
		_sellPanel.gameObject.SetActive(false);
	}

	/// <summary>
	/// ������ �ڽ� Ǯ��
	/// </summary>
	/// <returns></returns>
	private ItemBox PoolItemBox()
	{
		for (int i = 0; i < _itemBoxParent.childCount; ++i)
		{
			if (!_itemBoxParent.GetChild(i).gameObject.activeSelf)
			{
				return _itemBoxParent.GetChild(i).GetComponent<ItemBox>();
			}
		}
		return Instantiate(_itemBoxPrefeb, _itemBoxParent).GetComponent<ItemBox>();
	}
}
