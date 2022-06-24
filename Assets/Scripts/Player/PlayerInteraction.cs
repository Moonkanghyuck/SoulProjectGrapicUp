using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	private Player _player = null;

	[SerializeField]
	private float _itemDetectRange = 3f; // ������ ���� ����

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

		//��ȣ�ۿ�
		if (_player.SelectObject != null)
		{
			_player.OverWorldUIManager?.Setting(_player.SelectObject);
			if (Input.GetKeyDown(KeyCode.F))
			{
				_player.SelectObject.Interaction(_player);
				_player.OverWorldUIManager?.NoneSetting();
			}
		}
		else
		{
			_player.OverWorldUIManager?.NoneSetting();
		}

		InteractionDetect();
	}

	/// <summary>
	/// ���� ����� ��ȣ�ۿ� ã��
	/// </summary>
	private void InteractionDetect()
	{
		var items = GameObject.FindGameObjectsWithTag("Item");
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

		if (minRange < _itemDetectRange)
		{
			_player.SelectObject = itemObject.GetComponent<IInteraction>();
		}
		else
		{
			_player.SelectObject = null;
		}
	}
}
