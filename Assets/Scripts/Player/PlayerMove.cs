using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holoville.HOTween;

public class PlayerMove : MonoBehaviour
{
	[SerializeField]
	private float _moveSpeed = 0f; //�̵��ӵ�
	[SerializeField]
	private float _gravitySpeed = 0f; //�߷� �޴� �ӵ�
	[SerializeField]
	private float _rotateSpeed = 100.0f; //���� ȸ���ӵ�
	[SerializeField]
	private float _bodyRotateSpeed = 50.0f; //���� ȸ�� �ӵ�
	[Range(0.01f, 1f)]
	public float _velocityChangeSpeed = 0.01f; //���� ������

	//�Ӽ�
	private Vector3 currentVelocitySpeed = Vector3.zero; //�̵��ӵ� �ʱ갪
	private Vector3 _moveDirect = Vector3.zero; //ĳ���� ���� �̵����� �ʱ갪
	private Vector3 _gravityDirect = Vector3.zero; //���� �߷� ũ�� �ʱ갪
	private Player _player; //�÷��̾�

	public void Start()
	{
		_player = GetComponent<Player>();
	}

	public void Update()
	{
		if(_player.IsCantAnything)
		{
			return;
		}

		if(_player.IsCapture)
		{
			MonsterMove();
		}
		else
		{
			Move();
			SetGravity();
			BodyDirectChange();
		}
		
	}


	//���� ��
	/// <summary>
	/// �̵��Լ�
	/// </summary>
	private void Move()
	{
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		Vector3 forward = _player.MainCamera.transform.TransformDirection(Vector3.forward);
		forward.y = 0;
		Vector3 right = new Vector3(forward.z, 0, -forward.x);

		//ĳ���� �̵�����
		Vector3 targetDirect = inputX * right + inputY * forward;

		_moveDirect = Vector3.RotateTowards(_moveDirect, targetDirect, _rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000.0f);
		_moveDirect = _moveDirect.normalized;

		Vector3 amount = (targetDirect * _moveSpeed * Time.deltaTime) + (_gravityDirect * Time.deltaTime);

		_player.CharacterController.Move(amount);
	}

	/// <summary>
	/// ���� ������
	/// </summary>
	private void BodyDirectChange()
	{
		if (GetVelocitySpd() > 0.0f)
		{
			transform.forward = Vector3.Lerp(transform.forward, _moveDirect, _bodyRotateSpeed * Time.deltaTime);
		}
	}

	/// <summary>
	/// �ӵ� ��ȯ
	/// </summary>
	private float GetVelocitySpd()
	{
		if (_player.CharacterController.velocity == Vector3.zero)
		{
			currentVelocitySpeed = Vector3.zero;
		}
		else
		{
			Vector3 retVelocity = _player.CharacterController.velocity;
			retVelocity.y = 0;
			currentVelocitySpeed = Vector3.Lerp(currentVelocitySpeed, retVelocity, _velocityChangeSpeed * Time.fixedDeltaTime);
		}

		return currentVelocitySpeed.magnitude;
	}

	/// <summary>
	/// �߷� ����
	/// </summary>
	private void SetGravity()
	{
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit raycastHit;

		if (Input.GetKey(KeyCode.Space))
		{
			_gravityDirect.y = _gravitySpeed;
		}
		else if (Physics.Raycast(ray, out raycastHit, 1.5f))
		{
			_gravityDirect.y = 0;
		}
		else
		{
			_gravityDirect.y = -_gravitySpeed;
		}
	}

	
	//���� ��

	/// <summary>
	/// ���� ���� ü�� ����
	/// </summary>
	/// <param name="hp"></param>
	public void AddMonsterHP(int hp)
	{
		if(_player.CaptureMonster != null)
		{
			_player.CaptureMonster.HP += hp;
		}
	}

	/// <summary>
	/// ���͸� �̵���Ŵ
	/// </summary>
	private void MonsterMove()
	{
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		transform.position = _player.CaptureMonster.Position;

		Vector3 forward = _player.MainCamera.transform.TransformDirection(Vector3.forward);
		forward.y = 0;
		Vector3 right = new Vector3(forward.z, 0, -forward.x);

		//ĳ���� �̵�����
		Vector3 targetDirect = inputX * right + inputY * forward;

		_moveDirect = Vector3.RotateTowards(_moveDirect, targetDirect, _rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000.0f);
		_moveDirect = _moveDirect.normalized;

		_player.CaptureMonster.MonsterMove(_moveDirect, _player.IsTarggeting);
	}
	

}
