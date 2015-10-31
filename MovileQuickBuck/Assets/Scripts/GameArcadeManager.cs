using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameArcadeManager : MonoBehaviour, TouchObserver {


	private bool _playerUpTouched = false;
	private bool _playerDownTouched = false;

	private GestureDetectureBang _gestureBang;


	// Use this for initialization
	void Start () {

		_gestureBang = GestureDetectureBang.GetSharedGestureDetector();

		_gestureBang.AddListener (this);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTouchPlayerUp(){

		if (_playerUpTouched == false){

			_playerUpTouched = true;

			if(_playerDownTouched == false) {
				Debug.Log("Winner PlayerUp");

			}
		}

	}

	public void OnTouchPlayerDown(){

		if (_playerDownTouched == false){
			
			_playerDownTouched = true;
			
			if(_playerUpTouched == false) {
				Debug.Log("Winner PlayerDown");

			}
		}
	}

	public void ResetRound (){

		Debug.Log("Reset");

		_playerUpTouched = false;
		_playerDownTouched = false;

	}

	 
}
