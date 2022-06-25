using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion1 : IItem
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
			return "빨간 포션";
		}
	}
	public string Description
	{
		get
		{
			return "영혼의 체력을 조금 회복시켜준다";
		}
	}
	public EItem ItemType
	{
		get
		{
			return EItem.Postion1;
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

	private ItemData _itemData = null;
	private int _price = 10;

	public void UseItem(Player player)
	{
		if(Count > 0)
		{
			player.GetComponent<PlayerStat>().AddHP(10);
			Count--;
			NoticeManager.Notice("체력을 조금 회복하셨습니다");
		}
		else
		{
			NoticeManager.Notice("아이템이 없습니다");
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
