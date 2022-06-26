using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKUP1 : IItem
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
			return "���� ����";
		}
	}
	public string Description
	{
		get
		{
			return "������ ������ ���ݷ��� ������Ų��";
		}
	}
	public EItem ItemType
	{
		get
		{
			return EItem.ATKUP1;
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
				player.CaptureMonster.AddAtk(1);
				Count--;
				NoticeManager.Notice("���ݷ� ���� 1");
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
