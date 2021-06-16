using UnityEngine;

namespace RPG.Control
{
	public class AIController : MonoBehaviour
	{
		[SerializeField] float chaseDistance = 5f;

		private void Update()
		{
			if(DistanceToPlayer() <= chaseDistance * chaseDistance)
			{
				print(gameObject.name + " should chase");
			}
		}

		private float DistanceToPlayer()
		{
			GameObject player = GameObject.FindWithTag("Player");
			if (player == null) return chaseDistance + 100; //They won't chase

			return Vector3.SqrMagnitude(player.transform.position - transform.position);
		}
	}
}
