using System;
using UnityEngine;
using UnityEngine.UI;
using UnityMVC._Scripts.Models;

namespace UnityMVC._Scripts.View
{
	[RequireComponent(typeof(VerticalLayoutGroup), typeof(ContentSizeFitter), typeof(RectTransform))]
	public class TrackView : MonoBehaviour
	{
		[SerializeField]private Track _track;
		[SerializeField]private RectTransform _left;
		[SerializeField]private RectTransform _right;
		[SerializeField]private RectTransform _down;
		[SerializeField]private RectTransform _up;

		[SerializeField] private RectTransform _empty;

		private RectTransform _rectTransform;
		
		private Vector2 _position;
		public float Position
		{
			get { return _position.y; }
			set
			{
				if (Math.Abs(value - _position.y) < 0.01f) return;
				_position.y = value;
				_rectTransform.anchoredPosition = _position;
			}
		}

		public void Init(Track track)
		{
			_rectTransform = (RectTransform) transform;
			_position = _rectTransform.anchoredPosition;
			foreach (int beat in track.Beats)
			{
				GameObject g;
				switch (beat)
				{
					case 0:
						g = _left.gameObject;
						break;
					case 1:
						g = _down.gameObject;
						break;
					case 2:
						g = _up.gameObject;
						break;
					case 3:
						g = _right.gameObject;
						break;
					default:
						g = _empty.gameObject;
						break;
				}

				Transform view = GameObject.Instantiate(g, transform).transform;
				view.SetAsFirstSibling();
				
			}
		}

		private void Start()
		{
			Init(_track);
		}

		void Update()
		{
			Position -= Time.deltaTime * 200f;
		}
	}
}
