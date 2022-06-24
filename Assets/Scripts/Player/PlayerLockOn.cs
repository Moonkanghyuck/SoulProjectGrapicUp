using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLockOn : MonoBehaviour
{
	private Player _player = null;
	private PlayerStat _playerStat = null;
	private IMonster _informationMonster = null;

	[SerializeField]
	private float _monsterDetectRange = 1000f; //���� ���� ����

	public void Start()
	{
		_player = GetComponent<Player>();
		_playerStat = GetComponent<PlayerStat>();
	}

	public void Update()
	{
		if (_player.IsCantAnything)
		{
			return;
		}

		MonsterInfomationOn();
		//���� ���� ����
		if (_informationMonster != null)
		{
			_player.BattleUICanvas?.SelectMonsterUI(_informationMonster, _playerStat);
		}
		else
		{
			_player.BattleUICanvas?.NoneSelectMonsterUI();
		}

		//�Ͽ� ���
		if (Input.GetKeyDown(KeyCode.T))
		{
			if (_player.MainCameraMove.IsLookOn)
			{
				_player.MainCameraMove.SetLookOff();
				_player.BattleUICanvas?.NoneSetting();
				_player.TargettingMonster = null;
				_player.IsTarggeting = false;
			}
			else
			{
				MonsterDetect();
				if (_player.TargettingMonster != null)
				{
					_player.MainCameraMove.SetLookOn(_player.TargettingMonster.Transform);
					_player.IsTarggeting = true;
				}
			}
		}

		//�Ͽ� ��� UI
		if (_player.MainCameraMove.IsLookOn && _player.TargettingMonster != null)
		{
			_player.BattleUICanvas?.Setting(_player.TargettingMonster, _playerStat);
		}
		else
		{
			_player.BattleUICanvas?.NoneSetting();
		}

		//���� ������ ���Ϳ� Ÿ������ ���Ͱ� ���ٸ� ���� ����
		if(_player.TargettingMonster == _player.CaptureMonster)
		{
			_player.MainCameraMove.SetLookOff();
			_player.BattleUICanvas?.NoneSetting();
			_player.TargettingMonster = null;
			_player.IsTarggeting = false;
		}
	}

	/// <summary>
	/// ���� ����� ���� ã��
	/// </summary>
	private void MonsterDetect()
	{
		var items = GameObject.FindGameObjectsWithTag("Monster");
		if (_player.CaptureMonster != null)
		{
			items = items.Where(x => x != _player.CaptureMonster.GameObject).ToArray();
		}
		items = items.Where(x => IsTargetVisible(Camera.main, x.transform)).ToArray();
		float minRange = float.MaxValue;
		GameObject itemObject = null;
		for (int i = 0; i < items.Length; ++i)
		{
			Vector3 vector = items[i].transform.position - transform.position;
			float currentRange = vector.sqrMagnitude;
			if (minRange > currentRange)
			{
				minRange = currentRange;
				itemObject = items[i];
			}
		}

		if (minRange < _monsterDetectRange)
		{
			_player.TargettingMonster = itemObject.GetComponent<IMonster>();
		}
		else
		{
			_player.TargettingMonster = null;
		}
	}

	/// <summary>
	/// ���� ���� ����
	/// </summary>
	private void MonsterInfomationOn()
	{
		Vector3 startPoint = transform.position;
		Vector3 forward = _player.MainCamera.transform.forward;
		forward.y = 0;
		Ray ray = new Ray(startPoint, forward);
		Debug.DrawRay(startPoint, forward * 100, Color.red);

		RaycastHit raycastHit;
		int layerMask = 1 << LayerMask.NameToLayer("Monster");  // ���� ���̾ �浹 üũ��
		if (Physics.Raycast(ray, out raycastHit, 100, layerMask))
		{
			var monster = raycastHit.collider.gameObject.GetComponent<IMonster>();
			_informationMonster = monster;
		}
		else
		{
			_informationMonster = null;
		}
	}

		/// <summary>
		/// ī�޶� �ȿ� ������� �Ǵ�
		/// </summary>
		/// <param name="_camera"></param>
		/// <param name="_transform"></param>
		/// <returns></returns>
		private bool IsTargetVisible(Camera _camera, Transform _transform)
	{
		var planes = GeometryUtility.CalculateFrustumPlanes(_camera);
		Vector3 viewPos = _player.MainCamera.WorldToViewportPoint(_transform.position);
		if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
		{
			return true;
		}
		return false;
	}

}
