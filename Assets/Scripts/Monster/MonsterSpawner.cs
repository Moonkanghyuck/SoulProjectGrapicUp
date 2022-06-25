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
	[SerializeField]
	private PlayerStat _playerStat;
	private List<IMonster> _monsters = new List<IMonster>();
	private ItemPool _itemPool;
	[SerializeField]
	private GameObject _skeletonPrefeb;
	[SerializeField]
	private GameObject _skeletonMagePrefeb;
	[SerializeField]
	private GameObject _orcPrefeb;
	[SerializeField]
	private GameObject _ghoulPrefeb;
	[SerializeField]
	private GameObject _pumpkinPrefeb;
	[SerializeField]
	private GameObject _dragonPrefeb;
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

			float skeletonPercent = 100 - _playerStat.Level;
			float skeletonMagePercent = _playerStat.Level + 10;
			float goulPercent = _playerStat.Level + 30;
			float orcPercent = _playerStat.Level + 20;
			float pumpkinPercent = _playerStat.Level + 10;
			float dragonPercent = _playerStat.Level;
			float[] probs = new float[6] {skeletonPercent, skeletonMagePercent, goulPercent, orcPercent, pumpkinPercent, dragonPercent };
			switch (Choose(probs))
			{
				case 0:
					SpawnSkeleton(spawnVector);
					break;
				case 1:
					SpawnSkeletonMage(spawnVector);
					break;
				case 2:
					SpawnGhoul(spawnVector);
					break;
				case 3:
					SpawnOrc(spawnVector);
					break;
				case 4:
					SpawnPumpkin(spawnVector);
					break;
				case 5:
					SpawnDragon(spawnVector);
					break;
			}
		}
	}

	private void SpawnSkeleton(Vector3 pos)
	{
		Spawn<Skeleton>(_skeletonPrefeb, pos);
	}

	private void SpawnSkeletonMage(Vector3 pos)
	{
		Spawn<SkeletonWizard>(_skeletonMagePrefeb, pos);
	}

	private void SpawnOrc(Vector3 pos)
	{
		Spawn<Orc>(_orcPrefeb, pos);
	}

	private void SpawnDragon(Vector3 pos)
	{
		Spawn<Dragon>(_dragonPrefeb, pos);
	}

	private void SpawnGhoul(Vector3 pos)
	{
		Spawn<Ghoul>(_ghoulPrefeb, pos);
	}

	private void SpawnPumpkin(Vector3 pos)
	{
		Spawn<Pumpkin>(_pumpkinPrefeb, pos);
	}

	private void Spawn<T>(GameObject prefeb, Vector3 pos) where T : IMonster
	{
		IMonster monster = ItemPool.GetObject<T>();

		if (monster == null)
		{
			monster = Instantiate(prefeb, Vector3.zero, Quaternion.identity, null).GetComponent<T>();
			monster.GameObject.SetActive(false);
		}
		monster.SetPos(pos);
		monster.Init();
		AddCount();
		_monsters.Add(monster);
	}

	private int Choose(float[] probs)
	{

		float total = 0;

		foreach (float elem in probs)
		{
			total += elem;
		}

		float randomPoint = Random.value * total;

		for (int i = 0; i < probs.Length; i++)
		{
			if (randomPoint < probs[i])
			{
				return i;
			}
			else
			{
				randomPoint -= probs[i];
			}
		}
		return probs.Length - 1;
	}

}
