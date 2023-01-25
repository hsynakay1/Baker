using UnityEngine;
using System.Collections;

namespace StatueGames
{

	public class PlayerPrefs2
	{
		public static void SetBool(string key, bool state)
		{
			PlayerPrefs.SetInt(key, state ? 1 : 0);
			
		}

		public static bool GetBool(string key, bool defaultValue = false)
		{
			int value = PlayerPrefs.GetInt(key, defaultValue ? 1 : 0);
			
			if (value == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}