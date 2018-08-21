using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Table : MonoBehaviour
{

	private List<Seat> _seats;

	void Awake()
	{
		_seats = GetComponentsInChildren<Seat>().ToList();
	}

	public bool IsSeatAvailable()
	{
		return _seats.Any(s => !s.IsTaken);
	}

	public int GetAvailableSeats()
	{
		return _seats.Count(s => !s.IsTaken);
	}

	public Seat AvailableSeat()
	{
		if (IsSeatAvailable())
		{
			return _seats.First(s => !s.IsTaken);
		}
		return null;
	}
}
