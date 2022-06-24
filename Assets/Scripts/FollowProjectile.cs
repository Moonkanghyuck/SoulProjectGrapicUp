using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowProjectile : MonoBehaviour
{
	public Transform _targetTransform;
	public Vector3 _currentDirection = Vector3.zero;
	public float _moveSpeed = 2.0f;
	private bool _isPlayer;

	public void Initialized(bool isPlayer)
	{
		_isPlayer = isPlayer;
	}

	private void Update()
	{
		Move();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Monster"))
		{
			if (_isPlayer != other.gameObject.GetComponent<IMonster>().IsCapture)
			{
				_targetTransform = other.transform;
			}
		}
	}

	/// <summary>
	/// ����ü�� �̵��Լ�
	/// </summary>
	private void Move()
	{
		//��ǥ�� ���� ���
		if(_targetTransform != null)
		{
			Vector3 driection =  _targetTransform.position - transform.position; //��ǥ�� ���ϴ� ����
			transform.Translate(driection * _moveSpeed * Time.deltaTime); //��ǥ�� ���� �̵�

			//float angle = Mathf.Atan2(driection.z, driection.x) * Mathf.Rad2Deg; //������ �ٶ󺸾��� �� ����
			//Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.up); //y���� �������� ȸ��
			//Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, _moveSpeed * Time.deltaTime); //���� �������� ������ ����
			//transform.rotation = rotation; //���� ������ ������ ������ ������ ����
		}
	}
}
