using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
	public List<GameObject> projectiles;
	public List<GameObject> bulletPool;
	public List<GameObject> activated;
	public List<GameObject> deactivated;
	public int projectileCount;
	void Awake()
	{
			for (int x = 0; x < projectileCount; x++)
			{
				GameObject enemyCopy = Instantiate(projectiles[GameSettings.skinIndex]);
				bulletPool.Add(enemyCopy);
				enemyCopy.SetActive(false);
			}
	}
}
