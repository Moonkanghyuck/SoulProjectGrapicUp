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
	public GameObject Effect 
	{ 
		get
		{
			return _effect;
		}
		set
		{
			_effect = value;
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
	private bool _isPlayer = false; //�÷��̾��� ��������
	[SerializeField]
	private GameObject _effect = null; //����Ʈ
	[SerializeField]
	private GameObject _attacker = null; //�����ϴ� ����
	[SerializeField]
	private int _originDamage = 10; //�⺻������
	[SerializeField]
	private int _addDamage = 0; //�߰�������
	private IMonster _monster = null;

	private void Start()
	{
		_monster = GetComponentInParent<IMonster>();
		_attacker = _monster.GameObject;
	}


}
