using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityMVC._Scripts.Models
{
	[CreateAssetMenu(menuName = "ProjectUtility/Beats/New Track", fileName = "New Beats Track.asset")]
	public class Track : ScriptableObject
	{
		[Header("Playback Settings")]
		[Tooltip("# of beats per minute")]
		[Range(30, 360)]public int Bpm = 120;
		
		//0 = left, 1 = down, 2 = up, 3 = right, -1 = empty Beat
		[HideInInspector]public List<int> Beats;
		
		#region Random Data Generation
		public static int Inputs = 4;
		
		[Header("Random Settings")] 
			[Tooltip("# of preRoll (empty) beats")]
			[Range(0f, 10f)] [SerializeField] private int _preRoll = 10;
			[Tooltip("Minimum # of beats per block")]
			[Range(1, 20)] [SerializeField] private int _minBlock = 2;
			[Tooltip("Maximum # of beats per block")]
			[Range(1, 20)] [SerializeField] private int _maxBlock = 5;
			[Tooltip("Minimum # of empty beats between blocks")]
			[Range(1, 20)] [SerializeField] private int _minInterval = 1;
			[Tooltip("Maximum # of empty beats between blocks")]
			[Range(1, 20)] [SerializeField] private int _maxInterval = 2;
			[Tooltip("# of beats blocks")]
			[Range(1, 20)] [SerializeField] private int _blocks = 10;

		public void Randomize()
		{
			InitializeTrack();
			
			//preRoll
			for (int i = 0; i < _preRoll; ++i)
			{
				Beats.Add(-1);				
			}

			for (int blk = 0; blk < _blocks; ++blk)
			{
				int blockLength = Random.Range(_minBlock, _maxBlock + 1);
				for (int i = 0; i < blockLength; ++i)
				{
					int beat = Random.Range(0, Inputs);
					Beats.Add(beat);
				}

				if (blk == _blocks - 1) break;
				
				int intervalLength = Random.Range(_minInterval, _maxInterval + 1);
				for (int i = 0; i < intervalLength; ++i)
				{
					Beats.Add(-1);
				}
			}
		}

		public void InitializeTrack()
		{
			Beats = new List<int>();
		}

		#endregion

	}
}
