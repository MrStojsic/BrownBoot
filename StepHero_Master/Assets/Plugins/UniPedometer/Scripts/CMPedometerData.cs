using UnityEngine;
using System.Collections;
using System;

namespace UniPedometer
{
	
	[System.Serializable]
	public class CMPedometerData {
		[SerializeField] int startDate = default;
		[SerializeField] int endDate = default;
		[SerializeField] int numberOfSteps = default;
		[SerializeField] float distance = default;
		[SerializeField] bool hasCurrentPace = default;
		[SerializeField] float currentPace = default;
		[SerializeField] bool hasCurrentCadence = default;
		[SerializeField] float currentCadence = default;
		[SerializeField] bool hasFloorsAscended = default;
		[SerializeField] int floorsAscended = default;
		[SerializeField] bool hasFloorsDescended = default;
		[SerializeField] int floorsDescended = default;

		public static DateTime BaseDateTime {
			get {
				return new DateTime (1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			}
		}

		public DateTime StartDate {
			get {
				return BaseDateTime.AddSeconds (startDate).ToLocalTime();
			}
		}

		public DateTime EndDate {
			get {
				return BaseDateTime.AddSeconds (endDate).ToLocalTime();
			}
		}

		public int NumberOfSteps {
			get {
				return numberOfSteps;
			}
		}

		public float Distance {
			get {
				return distance;
			}
		}

		public bool HasCurrentPase {
			get {
				return hasCurrentPace;
			}
		}

		public float CurrentPace {
			get {
				return currentPace;
			}
		}

		public bool HasCurrentCadence {
			get {
				return hasCurrentCadence;
			}
		}

		public float CurrentCadence {
			get {
				return currentCadence;
			}
		}

		public bool HasFloorsAscended {
			get {
				return hasFloorsAscended;
			}
		}

		public int FloorsAscended {
			get {
				return floorsAscended;
			}
		}

		public bool HasFloorsDescended {
			get {
				return hasFloorsDescended;
			}
		}

		public int FloorsDescended {
			get {
				return floorsDescended;
			}
		}
	}

}