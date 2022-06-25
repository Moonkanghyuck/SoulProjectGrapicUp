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
			return "하얀 포션";
		}
	}

	public string Description
	{
		get
		{
			return "빙의한 몬스터의 체력을 조금 회복합니다";
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

	private NoticeManager NoticeManager
	{
		get
		{
			_noticeManager ??= GameObject.FindObjectOfType<NoticeManager>();
			return _noticeManager;
		}
	}
	private NoticeManager _noticeManager;

	private ItemData _itemData;
	private int _price = 100;

	public void UseItem(Player player)
	{
		if(Count > 0)
		{
			//player.CaptureMonster
			Debug.Log("포션 2 사용");
		}
		else
		{
			Debug.Log("포션이 없습니다");
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
