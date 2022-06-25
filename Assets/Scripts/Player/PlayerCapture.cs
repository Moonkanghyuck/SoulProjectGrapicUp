using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holoville.HOTween;

public class PlayerCapture : MonoBehaviour
{
	private Player _player;
	private PlayerStat _playerStat;

	private void Start()
	{
		_player = GetComponent<Player>();
		_playerStat = GetComponent<PlayerStat>();
	}

	public void Update()
	{
		if(_player.IsCantAnything)
		{
			return;
		}

		if(_player.IsCapture)
		{
			if(_player.CaptureMonster.HP <= 0)
			{
				OutCaptureMonster();
				return;
			}
			_player.BattleUICanvas?.SettingSkillUI(_player.CaptureMonster);
			_player.BattleUICanvas?.SettingMonsterInfo(_player.CaptureMonster);
			SkillE();
			SkillR();
			SkillMLB();
			SkillMRB();

			if (Input.GetKeyDown(KeyCode.Q))
			{
				OutCaptureMonster();
			}
		}
		else
		{
			_player.BattleUICanvas?.NoneSettingSkillUI();
			_player.BattleUICanvas?.NoneSettingMonsterInfo();
			Capture();
		}
	}

	/// <summary>
	/// ���� �õ�
	/// </summary>
	private void Capture()
	{
		Vector3 startPoint = transform.position;
		Vector3 forward = _player.MainCamera.transform.TransformDirection(Vector3.forward);
		forward.y = 0;
		Vector3 forwardcamera = _player.MainCamera.transform.forward;
		Ray ray = new Ray(startPoint, forward);
		Ray ray2 = new Ray(startPoint, forwardcamera);
		Debug.DrawRay(startPoint, forward * 100, Color.red);

		RaycastHit raycastHit;

		int layerMask = 1 << LayerMask.NameToLayer("Monster");  // ���� ���̾ �浹 üũ��
		if (Physics.Raycast(ray, out raycastHit, 100) || Physics.Raycast(ray2, out raycastHit, 100))
		{
			var monster = raycastHit.collider.gameObject.GetComponent<IMonster>();
			if (_player.SelectMonster != null & monster != _player.SelectMonster)
			{
				_player.SelectMonster.UnSelectMonster();
			}
			_player.SelectMonster = monster;
			_player.SelectMonster?.SelectMonster();

			if (Input.GetMouseButtonDown(0))
			{
				if (_player.SelectMonster != null)
				{
					if (_player.SelectMonster.CheckCapture(1))
					{
						_player.IsCantAnything = true;
						_player.CaptureMonster = _player.SelectMonster;
						_player.CaptureMonster.GameObject.layer = LayerMask.NameToLayer("Player");
						_player.CaptureMonster.Capture();
						_player.CharacterController.enabled = false;
						_player.IsCapture = true;
						HOTween.Init(true, true, true);
						HOTween.To(transform, 0.5f, new TweenParms().Prop("position", _player.CaptureMonster.Position).OnComplete(() => _player.IsCantAnything = false));
					}
				}
			}
		}
		else
		{
			_player.SelectMonster?.UnSelectMonster();
			_player.SelectMonster = null;
		}
	}

	/// <summary>
	/// ��������
	/// </summary>
	public void OutCaptureMonster()
	{
		_player.CaptureMonster.GameObject.layer = LayerMask.NameToLayer("Monster"); ;
		_player.IsCapture = false;
		_player.CaptureMonster.UnCapture();
		_player.CaptureMonster = null;
		_player.SelectMonster = null;
		_player.CharacterController.enabled = true;
	}

	/// <summary>
	/// E ��ų
	/// </summary>
	private void SkillE()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			_player.CaptureMonster.KeyESkill();
		}
	}

	/// <summary>
	/// R ��ų
	/// </summary>
	private void SkillR()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			_player.CaptureMonster.KeyRSkill();
		}
	}

	/// <summary>
	/// ���콺 ���� ��ư ��ų
	/// </summary>
	private void SkillMLB()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_player.CaptureMonster.MouseLButtonSkill();
		}
	}

	/// <summary>
	/// ���콺 ������ ��ư ��ų
	/// </summary>
	private void SkillMRB()
	{
		if (Input.GetMouseButtonDown(1))
		{
			_player.CaptureMonster.MouseRButtonSkill();
		}
	}
}
