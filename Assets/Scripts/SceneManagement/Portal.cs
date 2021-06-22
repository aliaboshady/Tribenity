using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
		[SerializeField] int sceneToLoad = -1;
		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				if(sceneToLoad >= 0)
				{
					SceneManager.LoadScene(sceneToLoad);
				}
				else
				{
					print("No scene to load chosen");
				}
			}
		}
	}
}