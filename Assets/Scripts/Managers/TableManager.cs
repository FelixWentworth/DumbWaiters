using System.Linq;
using UnityEngine;

public class TableManager : MonoBehaviour {

	public static Seat GetNearestAvailableSeat(Vector3 position)
	{
		var tables = Object.FindObjectsOfType<Table>().Where(t => t.GetAvailableSeats() > 0);
		if (!tables.Any())
		{
			return null;
		}

		var closest = tables.First().AvailableSeat();
		var dist = Vector3.Distance(position, closest.transform.position);
		foreach (var table in tables)
		{
			if (Vector3.Distance(position, table.AvailableSeat().transform.position) < dist)
			{
				closest = table.AvailableSeat();
			}
		}
		return closest;
	}

	public static Seat GetRandomAvailableSeat(Vector3 position)
	{
		var tables = Object.FindObjectsOfType<Table>().Where(t => t.GetAvailableSeats() > 0);
		if (!tables.Any())
		{
			return null;
		}

		var rand = UnityEngine.Random.Range(0, tables.Count());
		return tables.ElementAt(rand).AvailableSeat();
	}
}
