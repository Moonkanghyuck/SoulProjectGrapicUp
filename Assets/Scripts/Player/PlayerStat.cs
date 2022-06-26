using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
	public int HP
	{
		get
		{
			return _hp;
		}
		set
		{
			_hp = value;
		}
	}
	public int MaxHP
	{
		get
		{
			return _maxHp;
		}
		set
		{
			_maxHp = value;
		}
	}
	public int Level
	{
		get
		{
			return _level;
		}
		set
		{
			_level = value;
		}
	}

	public int SPD
	{
		get
		{
			return _speed;
		}
		set
		{
			_speed = value;
		}
	}

	public int DEF
	{
		get
		{
			return _defense;
		}
		set
		{
			_defense = value;
		}
	}

	public int Exp
	{
		get
		{
			return _exp;
		}
		set
		{
			_exp = value;
		}
	}

	//�ν����Ϳ��� ������ �� �ִ� ������
	[SerializeField]
	private int _hp = 30;
	[SerializeField]
	private int _maxHp = 30;
	[SerializeField]
	private int _speed = 30;
	[SerializeField]
	private int _defense = 30;
	[SerializeField]
	private int _level = 1;
	[SerializeField]
	private int _exp = 0;
	[SerializeField]
	private MoneySO _moneySO = null;

	private Player _player;

	public void Start()
	{
		_player = GetComponent<Player>();
		_player.BattleUICanvas?.SettingSoulInfo(this);
	}

	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.G))
		{
			Die();
		}
	}

	/// <summary>
	/// ü�� ����
	/// </summary>
	/// <param name="hp"></param>
	public void AddHP(int hp)
	{
		_hp += hp;
		if(_hp > _maxHp)
		{
			_hp = _maxHp;
		}
		_player.BattleUICanvas?.SettingSoulInfo(this);
	}

	/// <summary>
	/// ü�� ����
	/// </summary>
	/// <param name="hp"></param>
	public void AddMaxHP(int hp)
	{
		_maxHp += hp;
		_hp += hp;
		_player.BattleUICanvas?.SettingSoulInfo(this);
	}
	/// <summary>
	/// ����ġ ����
	/// </summary>
	/// <param name="exp"></param>
	public void AddExp(int exp)
	{
		_exp += exp;
		if (_exp > _level * 10)
		{
			LevelUP();
		}

		if(_player.CaptureMonster != null)
		{
			_player.CaptureMonster.AddEXP(exp);
		}
		_player.BattleUICanvas?.SettingSoulInfo(this);
	}

	/// <summary>
	/// �� ����
	/// </summary>
	/// <param name="add"></param>
	public void AddMoney(int add)
	{
		_moneySO.AddMoney(add);
	}

	/// <summary>
	/// ������
	/// </summary>
	private void LevelUP()
	{
		++_level;
		_exp = 0;
		_player.BattleUICanvas?.SettingSoulInfo(this);
	}


	/// <summary>
	/// ���ݹ���
	/// </summary>
	/// <param name="iAttack"></param>
	private void Damaged(IAttack iAttack)
	{
		_hp -= iAttack.OriginDamage;

		int damage = (iAttack.Damage - _defense);
		if (damage <= 0)
		{
			damage = 1;
		}
		_hp -= damage;
		_player.EffectManagerObj.SetTextEffect(damage, transform.position);
		_player.EffectManagerObj.AttackEffect(transform.position);
		_player.BattleUICanvas?.SettingSoulInfo(this);
		if (_hp <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		_player.GameOverUIManager.Setting();
		gameObject.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("ATK"))
		{
			if (_player.IsCapture)
			{
				return;
			}

			var iAttack = other.GetComponent<IAttack>();
			if (!iAttack.IsPlayer)
			{
				//���ݹ���
				Damaged(iAttack);
			}
		}
		else if (other.gameObject.CompareTag("Item"))
		{
		}
	}
}
