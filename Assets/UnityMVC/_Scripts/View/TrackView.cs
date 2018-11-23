using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityMVC._Scripts.Controllers;
using UnityMVC._Scripts.Models;

namespace UnityMVC._Scripts.View
{
	[RequireComponent(typeof(VerticalLayoutGroup), typeof(ContentSizeFitter), typeof(RectTransform))]
	public class TrackView : MonoBehaviour
	{
		public enum Trigger{ Missed, Right, Wrong }
		
		[SerializeField]private RectTransform _left;
		[SerializeField]private RectTransform _right;
		[SerializeField]private RectTransform _down;
		[SerializeField]private RectTransform _up;

		[SerializeField] private RectTransform _empty;

		private RectTransform _rectTransform;
		private List<Image> _beatViews;
		
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

		private float _beatViewSize;
		private float _spacing;

		public void Init(Track track)
		{
			_rectTransform = (RectTransform) transform;
			_position = _rectTransform.anchoredPosition;

			_beatViewSize = _empty.rect.height;
			_spacing = GetComponent<VerticalLayoutGroup>().spacing;
			
			_beatViews = new List<Image>();
			
			foreach (var beat in track.Beats)
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

				Image view = GameObject.Instantiate(g, transform).GetComponent<Image>();
				_beatViews.Add(view);
				
				view.transform.SetAsFirstSibling();
			}
		}

		private void Start()
		{
			Init(GamePlayController.Instance.Track);
		}

		private void Update()
		{
			Position -= (_beatViewSize + _spacing) * Time.deltaTime * GamePlayController.Instance.BeatsPerSecond;
		}

		public void TriggerBeatView(int index, Trigger trigger)
		{
			switch (trigger)
			{
				case Trigger.Missed:
					_beatViews[index].color = Color.gray;
					Debug.Log("<color=red>MISSED</color>");
//					Debug.Break();
					break;
				case Trigger.Right:
					_beatViews[index].color = Color.yellow;
//					Debug.Log("<color=blue>RIGHT</color>");
					break;
				case Trigger.Wrong:
					_beatViews[index].color = Color.cyan;
//					Debug.Log("<color=red>WRONG</color>");
					break;
				default:
					throw new ArgumentOutOfRangeException("trigger", trigger, null);
			}
		}
	}
}
