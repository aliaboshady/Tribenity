using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
		enum DestinationIdentifier { A, B, C, D, E }

		[SerializeField] DestinationIdentifier destination;
		[SerializeField] int sceneToLoad = -1;
		[SerializeField] Transform spawnPoint;
		[SerializeField] float fadeInTime = 1f;
		[SerializeField] float fadeOutTime = 1f;
		[SerializeField] float fadeWaitTime = 1f;

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

			Fader fader = FindObjectOfType<Fader>();
			yield return fader.FadeOut(fadeOutTime);
			SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
			wrapper.Save();
			yield return SceneManager.LoadSceneAsync(sceneToLoad);
			wrapper.Load();
			Portal otherPortal = GetOtherPortal();
			UpdatePlayer(otherPortal);

			yield return new WaitForSeconds(fadeWaitTime);
			yield return fader.FadeIn(fadeInTime);
			Destroy(gameObject);
		}

		private void UpdatePlayer(Portal otherPortal)
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<NavMeshAgent>().enabled = false;
			player.transform.position = otherPortal.spawnPoint.position;
			player.GetComponent<NavMeshAgent>().enabled = true;
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