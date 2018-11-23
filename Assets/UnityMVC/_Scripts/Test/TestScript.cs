using System;
using UnityEngine;

namespace UnityMVC._Scripts.Test
{
	public abstract class TestScriptBase : MonoBehaviour
	{
		public abstract void Example ();
		public abstract void Example2();
	}

	public class TestScript : TestScriptBase 
	{
		private enum TestEnum
		{
			None,
			Value1,
			Value2,
			Value3,
		}

		private int _column;
		
		// Use this for initialization
		private void Start () 
		{
			InvokeRepeating ("Example", 0f, 1f);
		}
	
		// Update is called once per frame
		public override void Example ()
		{
			TestEnum testEnum = default(TestEnum);
			switch (testEnum)
			{
				case TestEnum.Value1:
					break;
				case TestEnum.Value2:
					break;
				case TestEnum.Value3:
					break;
				case TestEnum.None:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public override void Example2()
		{
			
		}
	}
}
