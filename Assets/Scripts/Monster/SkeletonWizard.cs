using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWizard : MonsterBase
{

	public override void Start()
	{
		base.Start();
		_name = "스켈레톤 위자드";
		_description = "스켈레톤으로부터 파생된 몬스터, 뇌없는 머리로 어떻게 마법을 익히게 되었는지는 의문이지만, 몇 없는 마법을 부리는 몬스터이다";
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
