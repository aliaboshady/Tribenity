using UnityEngine;

namespace RPG.Control
{
	public class PatrolPath : MonoBehaviour
	{
		private void OnDrawGizmos()
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				Vector3 firstPos = GetWaypoint(i);
				int nextIndex = GetNextIndex(i);
				Vector3 secondPos = GetWaypoint(nextIndex);
				Gizmos.DrawSphere(firstPos, 0.2f);
				Gizmos.DrawLine(firstPos, secondPos);
			}
		}

		Vector3 GetWaypoint(int i)
		{
			return transform.GetChild(i).position;
		}

		int GetNextIndex(int i)
		{
			if(i + 1 == transform.childCount)
			{
				return 0;
			}

			return i + 1;
		}
	}
}