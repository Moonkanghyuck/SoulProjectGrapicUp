using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
	private ItemPool ItemPool
	{
		get
		{
			return _itemPool;
		}
	}

	private ItemPool _itemPool;

	public void Setting()
	{
		gameObject.SetActive(true);
		Invoke("EndEffect", 2f);
	}
	private void EndEffect()
	{
		_itemPool.RegisterObject(this);
		gameObject.SetActive(false);
	}
}
