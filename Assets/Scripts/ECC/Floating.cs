﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{

	public float degreesPerSecond = 15.0f;
	public float amplitude = 0.5f;
	public float frequency = 1f;
	Vector3 posOffset = new Vector3();
	Vector3 tempPos = new Vector3();

	public bool local;
	// Start is called before the first frame update
	void Start()
	{
		if (!local)
			posOffset = transform.position;
		else
			posOffset = transform.localPosition;
	}

	void Update()
	{
		Float();
	}

	void Float()
	{
		if (!local)
		{
			// Spin object around Y-Axis
			transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

			// Float up/down with a Sin()
			tempPos = posOffset;
			tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

			transform.position = tempPos;
		}
		else {
			// Spin object around Y-Axis
			transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.Self);

			// Float up/down with a Sin()
			tempPos = posOffset;
			tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

			transform.localPosition = tempPos;
		}
	}
}
