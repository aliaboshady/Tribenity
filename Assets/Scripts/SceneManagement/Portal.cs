using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Collections.Generic;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
		enum DestinationIdentifier { A, B, C, D, E }

		[SerializeField] DestinationIdentifier destination;
		[SerializeField] int sceneToLoad = -1;
		[SerializeField] Transform spawnPoint;

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				if(sceneToLoad >= 0)
				{
					StartCoroutine(Transition());
				}
				else
				{
					print("No scene to load chosen");
				}
			}
		}

		private IEnumerator Transition()
		{
			if(sceneToLoad < 0)
			{
				Debug.LogError("Scene to load not set");
				yield break;
			}
			DontDestroyOnLoad(gameObject);
			yield return SceneManager.LoadSceneAsync(sceneToLoad);
			Portal otherPortal = GetOtherPortal();
			UpdatePlayer(otherPortal);
			Destroy(gameObject);
		}

		private void UpdatePlayer(Portal otherPortal)
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.transform.position = otherPortal.spawnPoint.position;
			print(otherPortal.spawnPoint.position);
		}

		private Portal GetOtherPortal()
		{
			Portal[] portals = GameObject.FindObjectsOfType<Portal>();
			for (int i = 0; i < portals.Length; i++)
			{
				if (portals[i] == this) continue;
				if (portals[i].destination != destination) continue;

				return portals[i];
			}
			return null;
		}
	}
}