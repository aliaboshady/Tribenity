using UnityEngine;
using RPG.Combat;

namespace RPG.Control
{
	public class AIController : MonoBehaviour
	{
		[SerializeField] float chaseDistance = 5f;

		Fighter fighter;
		GameObject player;

		private void Start()
		{
			fighter = GetComponent<Fighter>();
			player = GameObject.FindWithTag("Player");
		}

		private void Update()
		{
			if (InAttackRangeOfPlayer()  && fighter.CanAttack(player))
			{
				fighter.Attack(player);
			}
			else
			{
				fighter.Cancel();
			}
		}

		private bool InAttackRangeOfPlayer()
		{
			if (player == null) return false;
			return Vector3.SqrMagnitude(player.transform.position - transform.position) <= chaseDistance * chaseDistance;
		}
	}
}
