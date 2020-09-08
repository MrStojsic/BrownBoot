using UnityEngine;

namespace UniPedometer {
	[System.Serializable]
	public class NSError {
		[SerializeField] int code = default;
		[SerializeField] string domain = default;
		[SerializeField] string localizedDescription = default;
		[SerializeField] string localizedRecoverySuggestion = default;
		[SerializeField] string localizedFailureReason = default;

		public int Code {
			get {
				return code;
			}
		}

		public string Domain {
			get {
				return domain;
			}
		}

		public string LocalizedDescription {
			get {
				return localizedDescription;
			}
		}

		public string LocalizedRecoverySuggestion {
			get {
				return localizedRecoverySuggestion;
			}
		}

		public string LocalizedFailureReason {
			get {
				return localizedFailureReason;
			}
		}
	}
}
