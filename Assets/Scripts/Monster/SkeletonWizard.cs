using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWizard : MonsterBase
{

	public override bool KeyESkill()
	{
		Debug.Log("��ų�� �������� ����");
		return base.KeyESkill();
	}

	public override bool KeyRSkill()
	{
		Debug.Log("��ų�� �������� ����");
		return base.KeyRSkill();
	}

	public override bool MouseLButtonSkill()
	{
		if (base.KeyESkill())
		{
			ChangeState(MonsterState.Attack);
			_attackState = AttackState.MLB;
			return true;
		}
		else
		{
			ChangeState(MonsterState.Attack);
			_attackState = AttackState.MLB;
			return false;
		}
	}

	public override bool MouseRButtonSkill()
	{
		Debug.Log("��ų�� �������� ����");
		return base.MouseRButtonSkill();
	}
}
