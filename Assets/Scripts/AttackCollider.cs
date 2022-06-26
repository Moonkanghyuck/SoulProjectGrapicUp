using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour, IAttack
{
	public bool IsPlayer 
	{
		get
		{
			return _isPlayer;
		}
		set
		{
			_isPlayer = value;
		}
	}
	public int OriginDamage 
	{ 
		get
		{
			return _originDamage;
		}

		set
		{
			_originDamage = value;
		}
	}

	public int AddDamage
	{
		get
		{
			return _addDamage;
		}
		set
		{
			_addDamage = value;
		}
	}

	public int Damage
	{
		get
		{
			return _originDamage + _addDamage;
		}
	}

	public GameObject Attacker
	{
		get
		{
			return _attacker;
		}
		set
		{
			_attacker = value;
		}
	}
	[SerializeField]
	private bool _isPlayer = false; //플레이어의 몬스터인지
	[SerializeField]
	private GameObject _attacker = null; //공격하는 몬스터
	[SerializeField]
	private int _originDamage = 10; //기본데미지
	[SerializeField]
	private int _addDamage = 0; //추가데미지
	private IMonster _monster = null;

	private void Start()
	{
		_monster = GetComponentInParent<IMonster>();
		if(_monster != null)
		{
		_attacker = _monster.GameObject;

		}
	}


}
