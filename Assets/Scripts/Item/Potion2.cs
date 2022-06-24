using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion2 : IItem
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
			return "Postion2";
		}
	}

	public string Description
	{
		get
		{
			return "�ι�° ����";
		}
	}

	public EItem ItemType
	{
		get
		{
			return EItem.Postion2;
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
	private int _price = 100;

	public void UseItem(Player player)
	{
		if(Count > 0)
		{
			Debug.Log("���� 2 ���");
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
