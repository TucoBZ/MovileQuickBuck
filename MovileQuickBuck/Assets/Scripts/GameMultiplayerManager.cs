using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameMultiplayerManager : MonoBehaviour, TouchObserver {

	//Botões dos respectivos players
	public Button _playerUpButton;
	public Button _playerDownButton;

	//Texto dos botões
	public Text _textButtonUp;
	public Text _textButtonDown;

	//Texto do centro
	public Text _textCenter;
	
	//Se os players já tocaram na tela
	private bool _playerUpTouched = false;
	private bool _playerDownTouched = false;
	
	//Se os player já deram ok para começar
	private bool _playerUpReady = false;
	private bool _playerDownReady = false;

	//Se o player clicou antes do tempo
	private bool _playerUpError = false;
	private bool _playerDownError = false;
	
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

		if (_playerUpError)
			return;
		
		if (_gameState == GameState.Ready) {
			_playerUpError = true;
			return;
		}
		
		if (_gameState != GameState.OnGame)
			return;
		
		if (_playerUpTouched == false){
			
			_playerUpTouched = true;
			
			if(_playerDownTouched == false) {
				_textCenter.text = "PlayerUp"; 
				ChangeStateTo(GameState.WinnerTime);
				
			}
		}
		
	}
	
	public void OnTouchPlayerDown(){

		if (_playerDownError)
			return;
		
		if (_gameState == GameState.Ready) {
			_playerDownError = true;
			return;
		}
		
		if (_gameState != GameState.OnGame)
			return;
		
		if (_playerDownTouched == false){
			
			_playerDownTouched = true;
			
			if(_playerUpTouched == false) {
				_textCenter.text = "PlayerDown"; 
				ChangeStateTo(GameState.WinnerTime);
				
			}
		}
	}

	public void clickButton(bool isPlayerUp){

		switch (_gameState){
		case GameState.Default:
			Ready(isPlayerUp);
			break;
		case GameState.Ready:
			break;
		case GameState.OnGame:
			break;
		case GameState.WinnerTime:
			ResetRound();
			break;
		}

	}

	private void Ready (bool isPlayerUp){
		
		if (_gameState != GameState.Default)
			return;
		
		if (isPlayerUp) {

			_playerUpButton.gameObject.SetActive(false);
			_playerUpReady = true;
			
		} else {

			_playerDownButton.gameObject.SetActive(false);
			_playerDownReady = true;
			
		}
		
		if (_playerUpReady && _playerDownReady) {
			ChangeStateTo(GameState.Ready);
		}
		
	}
	
	private void ResetRound (){
		
		ChangeStateTo (GameState.Default);
		
	}
	
	private void ChangeStateTo(GameState newGameState){
		
		_gameState = newGameState;
		
		switch (newGameState) {
		case GameState.Default:

			_textCenter.gameObject.SetActive(false);

			_textButtonUp.text = "Preparado?"; 
			_textButtonDown.text = "Preparado?";

			_playerUpButton.gameObject.SetActive(true);
			_playerDownButton.gameObject.SetActive(true);

			_playerUpReady = false;
			_playerDownReady = false;
			
			break;

		case GameState.Ready:

			_playerUpError = false;
			_playerDownError = false;

			StartCoroutine("GameReady");

			break;
		case GameState.OnGame:


			_playerUpTouched = false;
			_playerDownTouched = false;
			
			break;
		case GameState.WinnerTime:

			_textButtonDown.text = "Jogar de novo?";
			_playerDownButton.gameObject.SetActive(true);

			break;
			
			
		}
		
	}
	
	//Co-routine do Game
	IEnumerator GameReady(){

		_textCenter.gameObject.SetActive (true);
		_textCenter.text = "Preparar!";

		yield return new WaitForSeconds (1f);

		_textCenter.text = "Apontar!";

		yield return new WaitForSeconds (0.5f);
	
		_textCenter.gameObject.SetActive (false);

		float timePow = Random.Range (0.5F, 3.0F);

		Debug.Log (timePow);

		yield return new WaitForSeconds (timePow);

		_textCenter.gameObject.SetActive (true);
	
		_textCenter.text = "POW!";

		if (_playerUpError && _playerDownError) {

			_textCenter.text = "Os dois erraram!";

			ChangeStateTo (GameState.WinnerTime);

		} else {

			ChangeStateTo (GameState.OnGame);

		}

	}

}

