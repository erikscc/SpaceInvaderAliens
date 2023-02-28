using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private Animator anim;
	public float speed;
	private float lastSpeed;
	private void OnEnable()
	{
		EventManager.OnPlayerSpeedIncrease += PlayerSpeedPick;
		EventManager.OnPlayerSpeedReset += PlayerSpeedReset;
	}
	private void OnDisable()
	{
		EventManager.OnPlayerSpeedIncrease -= PlayerSpeedPick;
		EventManager.OnPlayerSpeedReset -= PlayerSpeedReset;
	}
	private void Start()
	{
		
		gameObject.transform.GetChild(GameSettings.skinIndex).gameObject.SetActive(true);
		anim = gameObject.transform.GetChild(GameSettings.skinIndex)
				.gameObject.GetComponent<Animator>();
	}
	private void Update()
	{
		float h = UltimateJoystick.GetHorizontalAxis("Movement");
		float v = UltimateJoystick.GetVerticalAxis("Movement");
		Vector3 movementDirection = new Vector3(h, 0f, 0f);
#if UNITY_EDITOR
		if (Input.GetKey(KeyCode.A))
		{
			movementDirection = Vector3.left;
		}
		if (Input.GetKey(KeyCode.D))
		{
			movementDirection = Vector3.right;
		}
#endif
		
		anim.SetFloat("Movement", h);
		
		gameObject.transform.position += movementDirection * Time.deltaTime * speed;
		gameObject.transform.position =new Vector3(Mathf.Clamp(gameObject.transform.position.x, -6f, 3f),gameObject.transform.position.y,gameObject.transform.position.z);
	}
	public void PlayerSpeedPick()
	{
		lastSpeed = speed;
		speed = 10f;
	}
	
	private void PlayerSpeedReset()
	{
		speed = lastSpeed;
	}
}
