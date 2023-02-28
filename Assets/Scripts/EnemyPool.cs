using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<GameObject> enemyPool;
    public List<GameObject> activated;
    public List<GameObject> deactivated;
    public int enemiesCount;
    // Start is called before the first frame update
    void Awake()
    {
		for (int i = 0; i <enemies.Count; i++)
		{
			for (int x = 0; x < enemiesCount; x++)
			{
                GameObject enemyCopy=Instantiate(enemies[i]);
                enemyPool.Add(enemyCopy);
                enemyCopy.SetActive(false);
			}
		}
    }
}
