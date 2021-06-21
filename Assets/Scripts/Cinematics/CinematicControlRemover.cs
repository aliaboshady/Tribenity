using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
	public class CinematicControlRemover : MonoBehaviour
	{
		GameObject player;
		private void Start()
		{
			player = GameObject.FindWithTag("Player");
			PlayableDirector playableDirector = GetComponent<PlayableDirector>();
			playableDirector.played += DisableControl;
			playableDirector.stopped += EnableControl;
		}
		void DisableControl(PlayableDirector _)
		{
			
			player.GetComponent<ActionScheduler>().CancelCurrentAction();
			player.GetComponent<PlayerController>().enabled = false;
		}

		void EnableControl(PlayableDirector _)
		{
			player.GetComponent<PlayerController>().enabled = true;
		}
	}
}
