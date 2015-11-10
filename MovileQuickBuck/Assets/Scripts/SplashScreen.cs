using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Scenes {SplashScreen, MainMenu, CharSelection, Multiplayer, Arcade, Credits, PowliticosInfo}

public class SplashScreen : MonoBehaviour {
	
	void Start () {
		StartCoroutine (WaitToDestroy (2.5f));
	}
	
	IEnumerator WaitToDestroy(float time) {
		yield return new WaitForSeconds(time);
		Application.LoadLevel((int)Scenes.MainMenu);
	}

}
