using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum GameState{ Default, Ready, OnGame, WinnerTime};

public class GameArcadeManager : MonoBehaviour, TouchObserver {

	//Se os players já tocaram na tela
	private bool _playerUpTouched = false;
	private bool _playerDownTouched = false;

	//Se os player já deram ok para começar
	private bool _playerUpReady = false;
	private bool _playerDownReady = false;

	private GestureDetectureBang _gestureBang;


	private GameState _gameState; 


	// Use this for initialization
	void Start () {

		_gestureBang = GestureDetectureBang.GetSharedGestureDetector();

		_gestureBang.AddListener (this);

		ChangeStateTo (GameState.Default);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTouchPlayerUp(){

		if (_gameState != GameState.OnGame)
			return;

		if (_playerUpTouched == false){

			_playerUpTouched = true;

			if(_playerDownTouched == false) {
				Debug.Log("Winner PlayerUp");

			}
		}

	}

	public void OnTouchPlayerDown(){

		if (_gameState != GameState.OnGame)
			return;

		if (_playerDownTouched == false){
			
			_playerDownTouched = true;
			
			if(_playerUpTouched == false) {
				Debug.Log("Winner PlayerDown");

			}
		}
	}

	public void Ready (bool isPlayerUp){

		if (_gameState != GameState.Default)
			return;

		if (isPlayerUp) {

			_playerUpReady = true;

		} else {

			_playerDownReady = true;

		}

		if (_playerUpReady && _playerDownReady) {
			ChangeStateTo(GameState.Ready);
		}
		
	}

	public void ResetRound (){

		Debug.Log("Reset");

		_playerUpTouched = false;
		_playerDownTouched = false;

	}

	private void ChangeStateTo(GameState newGameState){

		_gameState = newGameState;

		switch (newGameState) {
		case GameState.Default:

			_playerUpReady = false;
			_playerDownReady = false;

			break;
		case GameState.Ready:
			break;
		case GameState.OnGame:

			_playerUpTouched = false;
			_playerDownTouched = false;

			break;
		case GameState.WinnerTime:
			break;


		}

	}


	 
}
