using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonsterBase
{
	public override void Start()
	{
		base.Start();
		_name = "�巡��";
		_description = "���͵� �� �ܿ��� �ְ��̶� �� �� �ִ� ����, Ŀ�ٶ� ���� ��ī�ο� �̻��� ���� ��ɰ��� ������ ���� �� ����";
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
			EffectManagerObj.FireBallEffect(transform.position + transform.forward * 10);
			_attackState = AttackState.MRB;
			_coolTimeMRB = 0;
			return true;
		}
		return false;
	}
	public override void Delete()
	{
		ItemPoolFind.RegisterObject(this);
		base.Delete();
	}
}
