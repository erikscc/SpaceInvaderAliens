using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbility : MonoBehaviour
{
    public float speed = 30f;
    
    private void OnEnable()
    {
        speed = 30f;
       
    }

    void Update()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    void OnCollisionEnter(Collision co)
    {
		if (co.collider.CompareTag("Enemy"))
		{
            EventManager.OnBulletHit?.Invoke(gameObject, co.gameObject);
            EventManager.OnPSSpawn?.Invoke(gameObject.transform.position);
		}
		if (co.collider.CompareTag("Wall"))
		{
            gameObject.SetActive(false);
		}
    }
}
