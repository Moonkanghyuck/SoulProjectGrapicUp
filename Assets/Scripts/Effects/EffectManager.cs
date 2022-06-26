using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EffectManager : MonoBehaviour
{
	[SerializeField]
	private GameObject _textEffect;
	[SerializeField]
	private GameObject _attackEffect;

	private ItemPool _itemPool;

	public void Start()
	{
		_itemPool = FindObjectOfType<ItemPool>();
	}

	public void SetTextEffect(int damage, Vector3 pos)
	{
		TextAtkEffect textAtkEffect = _itemPool.GetObject<TextAtkEffect>();
		if(textAtkEffect == null)
		{
			textAtkEffect = Instantiate(_textEffect, pos, Quaternion.identity, null).GetComponent<TextAtkEffect>();
		}
		textAtkEffect.transform.position = pos;
		textAtkEffect.Setting(damage);
	}

	public void AttackEffect(Vector3 pos)
	{
		AttackEffect atkEffect = _itemPool.GetObject<AttackEffect>();
		if (atkEffect == null)
		{
			atkEffect = Instantiate(_attackEffect, pos, Quaternion.identity, null).GetComponent<AttackEffect>();
		}
		atkEffect.transform.position = pos;
		atkEffect.Setting();
	}

}
