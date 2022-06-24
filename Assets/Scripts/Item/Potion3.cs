using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion3 : IItem
{
	public int Count
	{
		get
		{
			return _count;
		}
		set
		{
			_count = value;
		}
	}
	private int _count;

	public void UseItem(PlayerMove player)
	{
		if(_count > 0)
		{
			--_count;
			Debug.Log("���� 3 ���");
		}
		else
		{
			Debug.Log("������ �����ϴ�");
		}
	}
}
