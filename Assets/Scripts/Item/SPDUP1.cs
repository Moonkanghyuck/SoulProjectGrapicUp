using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPDUP1 : IItem
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
			return "�ӵ� ����";
		}
	}
	public string Description
	{
		get
		{
			return "������ ������ �ӵ��� ������Ų��";
		}
	}
	public EItem ItemType
	{
		get
		{
			return EItem.SPDUP1;
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
	private int _price = 100;

	public void UseItem(Player player)
	{
		if (Count > 0)
		{
			if (player.CaptureMonster != null)
			{
				player.CaptureMonster.AddSpeed(1);
				Count--;
				NoticeManager.Notice("�ӵ� ���� 1");
			}
			else
			{
				NoticeManager.Notice("������ ���Ͱ� �����ϴ�");
			}
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
