using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
	public int Price
	{
		get;
		set;
	}
	public int Count
	{
		get;
		set;
	}
	public string Name
	{
		get;
	}
	public EItem ItemType
	{
		get;
	}
	public string Description
	{
		get;
	}
	public ItemData ItemData
	{
		get;
	}
	void SetItemData(ItemData itemData);

	void UseItem(Player player);

	void AddCount(int add);
}
