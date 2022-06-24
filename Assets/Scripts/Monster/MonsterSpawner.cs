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
		if(_monsterCount < 3)
		{
			SpawnMonster();
		}
	}

	public void AddCount()
	{
		_monsterCount++;
	}
	public void RemoveCount()
	{
		_monsterCount--;
	}

	private void SpawnMonster()
	{
		float randomX = Random.Range(-1, 1);
		float randomZ = Random.Range(-1, 1);

		if(randomX + randomZ == 0)
		{
			return;
		}

		float randomPosX = _player.transform.position.x + randomX * 100;
		float randomPosZ = _player.transform.position.z + randomZ * 100;
		Vector3 spawnVector = new Vector3(randomPosX,0,randomPosZ);
		Ray ray = new Ray(new Vector3(randomPosX, 1000, randomPosZ), Vector3.down);
		RaycastHit raycastHit;
		if(Physics.Raycast(ray, out raycastHit))
		{
			spawnVector.y = raycastHit.point.y + 1.5f;

			IMonster monster = ItemPool.GetObject<Skeleton>();

			if (monster == null)
			{
				monster = Instantiate(_skeletonPrefeb, spawnVector, Quaternion.identity, null).GetComponent<Skeleton>();
			}
			monster.Init();
			monster.Transform.position = spawnVector;
			AddCount();
			_monsters.Add(monster);
		}
	}

}
