using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoonLibrary.DesignPattern;

public class ItemInventory : MonoBehaviour, IObserver
{
	public Player Player
	{
		get
		{
			if(_playerMove == null)
			{
				_playerMove = FindObjectOfType<Player>();
			}
			return _playerMove;

		}
	}

	private List<ItemBox> _itemBoxs = new List<ItemBox>();
	private Player _playerMove = null;

	//�κ��丮 �г� ����
	[SerializeField]
	private GameObject _itemBoxPrefeb;
	[SerializeField]
	private Transform _itemBoxParent;
	[SerializeField]
	private ItemInfomation _itemInfomation;
	[SerializeField]
	private ItemUsePanel _itemUsePanel;
	[SerializeField]
	private ItemSetSO _items;

	private void Start()
	{
		_items.AddObserver(this);
	}

	public void RemoveItem(ItemBox itembox)
	{
		_items.RemoveItem(itembox.Item.ItemType);
		_itemBoxs.Remove(itembox);
		itembox.gameObject.SetActive(false);
	}

	public void AddItem(IItem iItem)
	{
		_items.AddItem(iItem.ItemType, iItem.Count);
		IItem item = _items.FindItem(iItem.ItemType);
		ItemBox itembox = _itemBoxs.Find(x => x.Item.ItemType == item.ItemType);
		UpdateUI();
	}

	public void UpdateUI()
	{
		_itemBoxs.Clear();
		for (int i = 0; i < _itemBoxParent.childCount; ++i)
		{
			_itemBoxParent.GetChild(i).gameObject.SetActive(false);
		}

		List<IItem> items = _items.GetIItemList();
		int itemCount = items.Count;
		for (int i = 0; i < itemCount; ++i)
		{
			ItemBox itemBox = PoolItemBox();
			itemBox.Setting(items[i], OnItemInfomationAndUsePanel, OnItemInfomation, false);
		}
		_itemInfomation.UpdateUI();
	}

	public void OnItemInfomation(ItemBox itembox)
	{
		_itemInfomation.Setting(itembox);
	}

	private void OnItemInfomationAndUsePanel(ItemBox itembox)
	{
		_itemInfomation.Setting(itembox);
		_itemUsePanel.Setting(itembox);
	}

	private ItemBox PoolItemBox()
	{
		for(int i = 0; i < _itemBoxParent.childCount; ++i)
		{
			if(!_itemBoxParent.GetChild(i).gameObject.activeSelf)
			{
				return _itemBoxParent.GetChild(i).GetComponent<ItemBox>();
			}
		}
		return Instantiate(_itemBoxPrefeb, _itemBoxParent).GetComponent<ItemBox>();
	}

	public void GetNotify()
	{
		UpdateUI();
	}
}
