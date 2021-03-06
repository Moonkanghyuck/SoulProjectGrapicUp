using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPUP1 : IItem
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
			return "����ġ ����";
		}
	}
	public string Description
	{
		get
		{
			return "경험치를 소량 증가시킨다";
		}
	}
	public EItem ItemType
	{
		get
		{
			return EItem.EXPUP1;
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
	private int _price = 500;

	public void UseItem(Player player)
	{
		if (Count > 0)
		{
			player.gameObject.GetComponent<PlayerStat>().AddExp(100);
			Count--;
			NoticeManager.Notice("����ġ ���� 100");
		}
		else
		{
			NoticeManager.Notice("�������� �����ϴ�");
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
