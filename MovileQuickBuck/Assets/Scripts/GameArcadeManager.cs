using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum GameState{ Default, Ready, OnGame, WinnerTime};

public class GameArcadeManager : MonoBehaviour, TouchObserver {
	
	//Botões dos respectivos players
	public Button _playerDownButton;
	
	//Texto dos botões
	public Text _textButtonDown;
	
	//Texto do centro
	public Text _textCenter;

	//Se computador atirou
	private bool _compHitted = false;
	
	//Se os players já tocaram na tela
	private bool _playerDownTouched = false;

	//Se o player clicou antes do tempo
	private bool _playerDownError = false;
	
	//Se os player já deram ok para começar
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
		//Modo Arcade Não tem playerUP
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
			
			if(_compHitted == false) { ///Fazer escolha do Computador
				_textCenter.text = "Você Ganhou!"; 
				ChangeStateTo(GameState.WinnerTime);
				
			}
		}
	}
	
	public void clickButton(){
		
		switch (_gameState){
		case GameState.Default:
			Ready();
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
	
	private void Ready (){
		
		if (_gameState != GameState.Default)
			return;

		_playerDownButton.gameObject.SetActive (false);
		ChangeStateTo (GameState.Ready);
			
	}
	
	private void ResetRound (){

		_playerDownButton.gameObject.SetActive (false);
		ChangeStateTo (GameState.Default);
		
	}
	
	private void ChangeStateTo(GameState newGameState){
		
		_gameState = newGameState;
		
		switch (newGameState) {
		case GameState.Default:
			
			_textCenter.gameObject.SetActive(false);
			
			_textButtonDown.text = "Começar";
			
			_playerDownButton.gameObject.SetActive(true);
			
			_playerDownReady = false;
			
			break;
			
		case GameState.Ready:

			_playerDownError = false;
			StartCoroutine("GameReady");
			
			break;
		case GameState.OnGame:

			_compHitted = false;
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
		_textCenter.text = "Preparar";
		
		yield return new WaitForSeconds (1f);
		
		_textCenter.text = "Apontar!";
		
		yield return new WaitForSeconds (0.5f);
		
		_textCenter.gameObject.SetActive (false);
		
		float timePow = Random.Range (0.5F, 3.0F);

		yield return new WaitForSeconds (timePow);
		
		_textCenter.gameObject.SetActive (true);
		
		_textCenter.text = "POW!";
		
		ChangeStateTo (GameState.OnGame);

		float timeComp = Random.Range (0.4F, 1F);

		yield return new WaitForSeconds (timeComp);

		CompHit ();
		
	}

	private void CompHit(){

		if (_playerDownTouched)
			return;

		_compHitted = true;
		_textCenter.text = "Você perdeu!"; 
		ChangeStateTo (GameState.WinnerTime);

	}
	
}

