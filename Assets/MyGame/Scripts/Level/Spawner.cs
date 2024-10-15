using UnityEngine;

public class Spawner : MonoSingleton<Spawner>
{
    [Header("Settings")]

    private float _spawnTimer;
    private int _enemiesSpawned;

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = GetSpawnDelay();
            if (_enemiesSpawned < LevelModel.Instance.TotalEnemies)
            {
                GameObject prefab = Game.Instance.ObjectPool.Spawn(PrefabEnum.Monster.M_blue);
                prefab.GetComponent<Enemy>().Reset();
                _enemiesSpawned++;
            }
        }
    }

    private float GetSpawnDelay()
    {
        return Random.Range(.5f, 2);
    }
}
