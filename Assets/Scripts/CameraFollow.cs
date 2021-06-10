using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 offsetFromPlayer;

	private void Start()
	{
		offsetFromPlayer = player.position - transform.position;
	}

	private void Update()
	{
		transform.position = player.position - offsetFromPlayer;
	}
}
