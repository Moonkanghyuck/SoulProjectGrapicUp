using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
	public ItemPool ItemPool
	{
		get
		{
			_itemPool ??= FindObjectOfType<ItemPool>();
			return _itemPool;
		}
		set
		{
			_itemPool = value;
		}
	}

	[SerializeField]
	private Transform _player;
	private List<IMonster> _monsters = new List<IMonster>();
	private ItemPool _itemPool;
	[SerializeField]
	private GameObject _skeletonPrefeb;
	private int _monsterCount = 0;

	public void Update()
	{
		if(_monsterCount < 10)
		{
			SpawnMonster();
		}

		DistanceDeleteMonster();
	}

	public void AddCount()
	{
		_monsterCount++;
	}
	public void RemoveCount()
	{
		_monsterCount--;
	}

	/// <summary>
	/// 거리에 따른 몬스터 삭제
	/// </summary>
	private void DistanceDeleteMonster()
	{
		for(int i = 0; i < _monsters.Count; )
		{
			IMonster monster = _monsters[i];
			Vector3 distance = monster.Transform.position - _player.position;
			if (distance.magnitude > 200)
			{
				monster.Delete();
				_monsters.Remove(monster);
			}
			else
			{
				++i;
			}
		}
	}

	private void SpawnMonster()
	{
		float random = Random.Range(0f, 6.28f);
		float randomX = Mathf.Cos(random) * 80;
		float randomZ = Mathf.Sin(random) * 80;

		float randomPosX =  _player.transform.position.x + randomX ;
		float randomPosZ = _player.transform.position.z + randomZ;
		Vector3 spawnVector = new Vector3(randomPosX,0,randomPosZ);
		Ray ray = new Ray(new Vector3(randomPosX, 1000, randomPosZ), Vector3.down);
		RaycastHit raycastHit;
		if(Physics.Raycast(ray, out raycastHit))
		{
			spawnVector.y = raycastHit.point.y + 1.5f;

			IMonster monster = ItemPool.GetObject<Skeleton>();

			if (monster == null)
			{
				monster = Instantiate(_skeletonPrefeb, Vector3.zero, Quaternion.identity, null).GetComponent<Skeleton>();
				monster.GameObject.SetActive(false);
			}
			monster.SetPos(spawnVector);
			monster.Init();
			AddCount();
			_monsters.Add(monster);
		}
	}

}
