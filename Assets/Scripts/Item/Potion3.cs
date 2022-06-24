using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion3 : IItem
{
	public int Price
	{
		get
		{
			return _price;
		}
		set
		{
			_price = value;
		}
	}
	public int Count
	{
		get
		{
			return _itemData._count;
		}
		set
		{
			_itemData._count = value;
		}
	}
	public string Name
	{
		get
		{
			return "Postion3";
		}
	}
	public string Description
	{
		get
		{
			return "����° ����";
		}
	}
	public EItem ItemType
	{
		get
		{
			return EItem.Postion3;
		}
	}
	public ItemData ItemData
	{
		get
		{
			return _itemData;
		}
	}
	private ItemData _itemData;
	private int _price = 0;

	public void UseItem(Player player)
	{
		if(Count > 0)
		{
			--Count;
			Debug.Log("���� 3 ���");
		}
		else
		{
			Debug.Log("������ �����ϴ�");
		}
	}
	public void AddCount(int add)
	{
		Count += add;
	}
	public void SetItemData(ItemData itemData)
	{
		_itemData = itemData;
	}
}
