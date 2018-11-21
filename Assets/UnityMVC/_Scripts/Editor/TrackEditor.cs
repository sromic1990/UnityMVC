using UnityEngine;
using UnityEditor;
using UnityMVC._Scripts.Models;

namespace UnityMVC._Scripts.Editor
{
	[CustomEditor(typeof(Track))]
	public class TrackEditor : UnityEditor.Editor
	{
		private Track _track;
		private Vector2 _position;
		private bool _displayBeatsData;

		private void OnEnable()
		{
			_track = (Track) target;
			if(_track.Beats == null) _track.InitializeTrack();
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (_track.Beats == null) return;
			
			if (_track.Beats.Count == 0)
			{
				EditorGUILayout.HelpBox("Empty Track", MessageType.Info);
				if (GUILayout.Button("Generate Random track", EditorStyles.miniButton))
				{
					Debug.Log("Random Track generated");
					_track.Randomize();
				}
			}
			else
			{
				if (GUILayout.Button("Update Random track", EditorStyles.miniButton))
				{
					Debug.Log("Random Track generated");
					_track.Randomize();
				}

				_displayBeatsData = EditorGUILayout.Foldout(_displayBeatsData, "Display Beats");
				if (_displayBeatsData)
				{
					_position = EditorGUILayout.BeginScrollView(_position);
					for (int i = 0; i < _track.Beats.Count; i++)
					{
						_track.Beats[i] = EditorGUILayout.IntSlider(_track.Beats[i], -1, Track.Inputs - 1);
					}
					EditorGUILayout.EndScrollView();
				}

				if (GUILayout.Button("Clear track information", EditorStyles.miniButton))
				{
					_track.InitializeTrack();
				}
			}
			EditorUtility.SetDirty(_track);
		}
	}
}
