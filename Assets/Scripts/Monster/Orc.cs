using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonsterBase
{
	public override void Start()
	{
		base.Start();
		_name = "��ũ";
		_description = "�ΰ����� Ŀ�ٶ� ü�ݰ� �ٷ��� ���� ���ͷ� ��ġ�� ���� ��ø�ϸ� �ʷϻ� �Ǻθ� �̿��Ͽ� Ǯ �ӿ��� ��ɰ��� �ٰ����� ���� ��ٸ���";
	}

	public override bool KeyESkill()
	{
		if (CheckCoolTimeE())
		{
			ChangeState(MonsterState.Attack);
			_attackState = AttackState.E;
			_coolTimeE = 0;
			return true;
		}
		return false;
	}

	public override bool KeyRSkill()
	{
		if (CheckCoolTimeR())
		{
			ChangeState(MonsterState.Attack);
			_attackState = AttackState.R;
			_coolTimeR = 0;
			return true;
		}
		return false;
	}

	public override bool MouseLButtonSkill()
	{
		if (CheckCoolTimeMLB())
		{
			ChangeState(MonsterState.Attack);
			_attackState = AttackState.MLB;
			_coolTimeMLB = 0;
			return true;
		}
		return false;
	}

	public override bool MouseRButtonSkill()
	{
		if (CheckCoolTimeMRB())
		{
			ChangeState(MonsterState.Attack);
			_attackState = AttackState.MRB;
			transform.localScale = Vector3.one * 1.2f;
			_atk += 5;
			_defense += 5;
			_speed += 5;
			_coolTimeMRB = 0;
			Invoke("StatOrigin", 5f);
			return true;
		}
		return false;
	}

	private void StatOrigin()
	{
		transform.localScale = Vector3.one * 1f;
		_atk -= 5;
		_defense -= 5;
		_speed -= 5;
		_coolTimeMRB = 0;
	}

	public override void Delete()
	{
		ItemPoolFind.RegisterObject(this);
		base.Delete();
	}
}
