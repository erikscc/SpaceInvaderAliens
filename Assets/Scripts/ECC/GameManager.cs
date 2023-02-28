using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject stageReward;
    public List<GameObject> bonusObjects;
    public List<GameObject> enemyLanes;
    public EnemyPool pool;
    public ProjectilePool bullet;
    public EffectPool psPool;
    public GameObject bgSound;

    private int score;
    private float playerHP;
    private float playerXP;
    private float stageXP;
    private float stages;
    private float spawningTimer;
    private float enemySpeed;
    List<IEnumerator> spawnings=new();
    private int spawningID;
    void OnEnable()
    {
        EventManager.OnFinish += EnemyDie;
        EventManager.OnBulletHit += BulletHit;
        EventManager.OnPSEffectDeactivated += PSDeactivated;
        EventManager.OnPSSpawn += PSActivate;
        EventManager.OnEnemyDieTrigger += EnemyDieTrigger;
    }

    void OnDisable()
    {
        EventManager.OnFinish -= EnemyDie;
		EventManager.OnBulletHit -= BulletHit;
        EventManager.OnPSEffectDeactivated -= PSDeactivated;
        EventManager.OnPSSpawn -= PSActivate;
        EventManager.OnEnemyDieTrigger -= EnemyDieTrigger;
    }

	private void Start()
	{
        BackGroundSound();

        playerHP = 100f;
        playerXP = 0;
        stageXP = 10f;
        stages = 1f;
        spawningID = (int)stages;
        spawningTimer = 5f;
        enemySpeed = 1;
        EventManager.OnXPIncrease?.Invoke(playerXP, stageXP);
        EventManager.OnStageIncrease?.Invoke(stages);
        spawnings.Add(SpawnEnemy(enemySpeed,spawningTimer));
        StartCoroutine(spawnings[spawnings.Count-1]);
        StartCoroutine(BonusObject());
	}
	
    public IEnumerator SpawnEnemy(float runSpeed=1,float spawnTime=5f)
	{
		while (true)
		{
            int rnd = Random.Range(0,enemyLanes.Count);
            int rndEnemy = Random.Range(0, pool.enemyPool.Count);
            pool.enemyPool[rndEnemy].transform.position = enemyLanes[rnd].GetComponent<Lane>().spawnPoint.transform.position;
            pool.enemyPool[rndEnemy].SetActive(true);
            pool.enemyPool[rndEnemy].GetComponent<NavMeshAgent>().enabled = true;
            pool.enemyPool[rndEnemy].GetComponent<NavMeshAgent>().SetDestination(enemyLanes[rnd].GetComponent<Lane>().target.transform.position);
            pool.enemyPool[rndEnemy].GetComponent<NavMeshAgent>().speed = runSpeed;
            pool.activated.Add(pool.enemyPool[rndEnemy]);
            pool.enemyPool.Remove(pool.enemyPool[rndEnemy]);
            yield return new WaitForSeconds(spawnTime);
		}
	}
    public IEnumerator BonusObject()
	{
		while (true)
		{
            int rnd = Random.Range(0, enemyLanes.Count);
            int rndBonus = Random.Range(0, bonusObjects.Count);
            bonusObjects[rndBonus].transform.position = enemyLanes[rnd].GetComponent<Lane>().spawnPoint.transform.position;
            bonusObjects[rndBonus].SetActive(true);
            bonusObjects[rndBonus].GetComponent<NavMeshAgent>().enabled = true;
            bonusObjects[rndBonus].GetComponent<NavMeshAgent>().SetDestination(enemyLanes[rnd].GetComponent<Lane>().target.transform.position);
            bonusObjects[rndBonus].GetComponent<NavMeshAgent>().speed = 2f;
            yield return new WaitForSeconds(10f);
        }
	} 
    public void FullHealthBonus()
	{ 
        int rnd = Random.Range(0, enemyLanes.Count);
        stageReward.transform.position = enemyLanes[rnd].GetComponent<Lane>().spawnPoint.transform.position;
        stageReward.SetActive(true);
        stageReward.GetComponent<NavMeshAgent>().enabled = true;
        stageReward.GetComponent<NavMeshAgent>().SetDestination(enemyLanes[rnd].GetComponent<Lane>().target.transform.position);
        stageReward.GetComponent<NavMeshAgent>().speed = 3f;
	}
    private void EnemyDie(GameObject enemyObject)
    {
        enemyObject.SetActive(false);
		if (enemyObject.CompareTag("Enemy"))
		{
            pool.enemyPool.Add(enemyObject);
            pool.activated.Remove(enemyObject);
           
		}
    } 
    private void EnemyDieTrigger(GameObject enemyObject)
    {
        enemyObject.SetActive(false);
		if (enemyObject.CompareTag("Enemy"))
		{
            pool.enemyPool.Add(enemyObject);
            pool.activated.Remove(enemyObject);
            playerHP-=5f;
            Debug.Log(playerHP);
            if (playerHP > 0)
            {
                EventManager.OnPlayerDamage?.Invoke(playerHP);
            }
            else 
            {
                AdManager.instance.ShowRewarded(result=> { GameOver();});
            }
		}
    }
    public void FireButton()
	{
        EventManager.OnShoot?.Invoke();
	}
    public  void FireSpecialAbility()
	{
        EventManager.OnSpecialSound?.Invoke();
        EventManager.OnSpecialAbilityFire?.Invoke();
        EventManager.OnFireRateTwo?.Invoke(2f);

    }
    private void BulletHit(GameObject bullet,GameObject hitObject)
    {
        if (hitObject.CompareTag("Enemy"))
		{
            EventManager.OnEnemyHitSOund?.Invoke();
            playerXP += 1f;
            score += 15;
            EventManager.OnScoreUpdate?.Invoke(score);
            if (playerXP==stageXP)
			{
                //StopCoroutine(spawnings[spw]);
                stageXP += 10f;
                stages++;
                if (stages == 10f || stages ==12f || stages == 14f ||
                    stages == 16f || stages == 18f || stages == 20f ||
                    stages == 22f || stages == 24f || stages == 26f) FullHealthBonus();

                spawningID++;
                playerXP = 0f;
                if (spawningTimer <= 1f) { spawningTimer = 1f; } else { spawningTimer -= 0.45f;};
                enemySpeed += 0.2f;
                EventManager.OnStageIncrease?.Invoke(stages);
                spawnings.Add(SpawnEnemy(enemySpeed, spawningTimer));
                StartCoroutine(spawnings[spawnings.Count-1]);
			}
            EventManager.OnXPIncrease?.Invoke(playerXP,stageXP);
		    EnemyDie(hitObject);
		}
		if (hitObject.CompareTag("BonusWeapon"))
		{
            EventManager.OnBonusHitSound?.Invoke();
            EventManager.OnWeaponBonus?.Invoke();
            EnemyDie(hitObject);
        }
        if (hitObject.CompareTag("BonusHeart"))
		{
            EventManager.OnBonusHitSound?.Invoke();
            playerHP++;
            if (playerHP > 100f) playerHP = 100f;
            EventManager.OnHeartBonus?.Invoke(playerHP);
            EnemyDie(hitObject);
        }
        if (hitObject.CompareTag("BonusEnergy"))
		{
            EventManager.OnBonusHitSound?.Invoke();
            EventManager.OnEnergyBonusPicked?.Invoke();
            EnemyDie(hitObject);
        }
        if (hitObject.CompareTag("BonusFullHP"))
		{
            EventManager.OnBonusHitSound?.Invoke();
            playerHP = 100f;
            EventManager.OnFullHPBonus?.Invoke(playerHP);
            EnemyDie(hitObject);
        }
    }
    private void PSDeactivated(GameObject ps)
    {
        ps.SetActive(false);
        psPool.PSpool.Add(ps);
        psPool.activated.Remove(ps);
    }
    private void PSActivate(Vector3 point)
    {
        psPool.PSpool[0].transform.position = point;
        psPool.PSpool[0].SetActive(true);
        psPool.activated.Add(psPool.PSpool[0]);
        psPool.PSpool.RemoveAt(0);
    }
    public void LoadMenu()
	{
        AdManager.instance.ShowInterstitial(LoadingMenu);
	}
    private void LoadingMenu()
	{
        GameSettings.loadingScene = "MainMenu";
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
    }
    public void BackToGame()
	{
        Time.timeScale = 1f;
	} 
    public void PauseGame()
	{
        Time.timeScale = 0f;
	}
	private void OnDestroy()
	{
        PlayerPrefs.SetInt("HighScore",score);
	}
    public void BackGroundSound()
	{
        if (PlayerPrefs.GetInt("BGMusic", 0) != 0)
        {
            PlayerPrefs.SetInt("BGMusic", 0);
            bgSound.SetActive(true);
            EventManager.OnMuteSound?.Invoke(0);
        } else
		{
            PlayerPrefs.SetInt("BGMusic", 1);
            bgSound.SetActive(false);
            EventManager.OnMuteSound?.Invoke(1);
        }
       
	}
    private void GameOver()
	{
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
