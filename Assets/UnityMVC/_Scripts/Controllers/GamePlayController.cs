using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityMVC._Scripts.Models;
using UnityMVC._Scripts.View;

namespace UnityMVC._Scripts.Controllers
{
	public class GamePlayController : MonoBehaviour
	{
		[Header("Input")] 
		[SerializeField]private KeyCode _left;
		[SerializeField]private KeyCode _down;
		[SerializeField]private KeyCode _up;
		[SerializeField]private KeyCode _right;


		[Header("Track")]
		[Tooltip("Beats track to play")]
		[SerializeField] private Track _track;
		//<summary>
		//The current track.
		//</summary>
		//<value>The track.</value>
		public Track Track{ get { return _track; } }

		public float BeatsPerSecond { get; private set; }
		public float SecondsPerBeat { get; private set; }

		[SerializeField]private bool _played;
		private bool _completed;

		private TrackView _trackView;

		private WaitForSeconds _waitAndStop;

		private static GamePlayController _instance;
		public static GamePlayController Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = (GamePlayController) GameObject.FindObjectOfType(typeof(GamePlayController));

//					if (_instance == null)
//					{
//						GameObject go = new GameObject("GamePlayController:Singleton");
//						_instance = go.AddComponent<GamePlayController>();
//					}
				}

				return _instance;
			}
			set { _instance = value; }
		}

		#region Monobehavior Methods
		private void OnDestroy()
		{
			_instance = null;
		}

		private void Awake()
		{
			_instance = this;
			
			BeatsPerSecond = _track.Bpm / 60f;
			SecondsPerBeat = 60f / _track.Bpm;
			
			_waitAndStop = new WaitForSeconds(SecondsPerBeat * 2);

			_trackView = FindObjectOfType<TrackView>();
			if(!_trackView)
				Debug.LogWarning("No trackView found in the current scene");
		}
		
		private void Start () 
		{
			InvokeRepeating("NextBeat", 0f, SecondsPerBeat);
		}

		private void Update ()
		{
			if (_played || _completed) return;
			
			if (Input.GetKeyDown(_left))
				PlayBeat(0);
			if (Input.GetKeyDown(_down))
				PlayBeat(1);
			if (Input.GetKeyDown(_up))
				PlayBeat(2);
			if (Input.GetKeyDown(_right))
				PlayBeat(3);
		}
		#endregion
		
		#region Gameplay
		private int _current;
		public int Current
		{
			get { return _current; }
			set
			{
				if (value != _current)
				{
					_current = value;
					if (_current == _track.Beats.Count)
					{
						CancelInvoke("NextBeat");
						_completed = true;
						StartCoroutine(WaitAndStop());
					}
				}
			}
		}
		
		private void PlayBeat(int input)
		{
//			Debug.Log(input);
			_played = true;
			
			if (_track.Beats[Current] == -1)
			{
//				Debug.Log(string.Format("{0} played untimely", input));
			}
			else if (_track.Beats[Current] == input)
			{
				//right combination
//				Debug.Log(string.Format("{0} played right", input));
				_trackView.TriggerBeatView(_current, TrackView.Trigger.Right);
			}
			else
			{
				//wrong combination
//				Debug.Log(string.Format("{0} played, {1} expected", input, _track.Beats[Current]));
				_trackView.TriggerBeatView(_current, TrackView.Trigger.Wrong);
			}
		}

		private void NextBeat()
		{
//			Debug.Log("Tick");

			if (!_played && _track.Beats[Current] != -1)
			{
//				Debug.Log(string.Format("{0} missed", _track.Beats[Current]));
				_trackView.TriggerBeatView(_current, TrackView.Trigger.Missed);
			}
			_played = false;
			
			Current++;
		}

		private IEnumerator WaitAndStop()
		{
			yield return _waitAndStop;
			enabled = false;
		}
		#endregion
	}
}
