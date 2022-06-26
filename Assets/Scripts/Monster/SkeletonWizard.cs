using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWizard : MonsterBase
{

	public override void Start()
	{
		base.Start();
		_name = "���̷��� ���ڵ�";
		_description = "���̷������κ��� �Ļ��� ����, ������ �Ӹ��� ��� ������ ������ �Ǿ������� �ǹ�������, �� ���� ������ �θ��� �����̴�";
	}

	public override bool KeyESkill()
	{
		if (CheckCoolTimeE())
		{
			ChangeState(MonsterState.Attack);
			_attackState = AttackState.E;
			EffectManagerObj.SmallFireBallEffect(transform.position + transform.forward * 10);
			EffectManagerObj.SmallFireBallEffect(transform.position + transform.forward * 10 + transform.up * 10);
			EffectManagerObj.SmallFireBallEffect(transform.position + transform.forward * 10 + transform.up * -10);
			EffectManagerObj.SmallFireBallEffect(transform.position + transform.forward * 10 + transform.right * 10);
			EffectManagerObj.SmallFireBallEffect(transform.position + transform.forward * 10 + transform.right * -10);
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
			EffectManagerObj.FireBallEffect(transform.position + transform.forward * 10);
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
