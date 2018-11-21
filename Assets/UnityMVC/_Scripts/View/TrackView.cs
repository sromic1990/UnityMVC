using UnityEngine;
using UnityEngine.UI;
using UnityMVC._Scripts.Models;

namespace UnityMVC._Scripts.View
{
	[RequireComponent(typeof(VerticalLayoutGroup), typeof(ContentSizeFitter), typeof(RectTransform))]
	public class TrackView : MonoBehaviour
	{
		[SerializeField]private Track _track;
	}
}
