﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


[System.Serializable]
public class WinnerMenu {
	public GameObject WinnerBackground;
	public GameObject WinnerCanvas;
	public Powlitico WinnerPow;

	public void SetWinnerMenuAble(bool able){
		WinnerBackground.SetActive(able);
		WinnerCanvas.SetActive(able);
	}
}

public enum GameState{ Default, Ready, OnGame, WinnerTime};

public class GameArcadeManager : MonoBehaviour, TouchObserver {

	//Paineis com os menus
	public GameObject _painelDown;

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
	

	public GameObject[] _scoreDownPlayerUp;
	public GameObject[] _scoreDownPlayerDown;

	private GameState _gameState; 
	
	public Sprite pointBlue;
	public Sprite pointWhite;

	//Pontos dos Players
	private int _upPoints = 0;
	private int _downPoints = 0;
	
	public GameObject powImage;

	private GameController controller;

	
	public GameObject pausePainel;
	
	public Text _p1NameDown;
	public Text _p2NameDown;
	
	//Personagens
	public Powlitico pow1;
	public Powlitico pow2;

	public WinnerMenu winnerMenu;

	//Paineis com os menus
	public GameObject gameArcadeCanvas;

	///Classe de Ads
	private PowliticosADS ads;


	// Use this for initialization
	void Start () {

		GameObject gmControl = GameObject.FindGameObjectWithTag ("GameController");
		
		if (gmControl != null) {
			controller = gmControl.GetComponent<GameController> ();
			controller.player1Name = _p1NameDown;
			controller.player2Name = _p2NameDown;
			controller.pow1 = pow1;
			controller.pow2 = pow2;
			controller.SetName ();
			pow1 = controller.pow1;
			pow2 = controller.pow2;
		}

		winnerMenu.SetWinnerMenuAble (false);

		_gestureBang = GestureDetectureBang.GetSharedGestureDetector();
		
		_gestureBang.AddListener (this);
		
		ChangeStateTo (GameState.Default);

		ads = new PowliticosADS();
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
			pow1.Fail();
			DownHitPie();
			return;
		}

		if (_gameState != GameState.OnGame)
			return;
		
		if (_playerDownTouched == false){
			
			_playerDownTouched = true;
			
			if(_compHitted == false) { ///Fazer escolha do Computador
				StartCoroutine("DownPlayerWinner");
				
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

		HideDownMenu ();
		_playerDownButton.gameObject.SetActive (false);
		ChangeStateTo (GameState.Ready);
			
	}
	
	private void ResetRound (){

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

			powImage.SetActive (false);
			_textCenter.color = Color.black;
			_textCenter.gameObject.SetActive (false);
			CheckWinner();
			
			break;
			
			
		}
		
	}
	
	private void CheckWinner(){
		
		if (_upPoints == 3) {
			
			Debug.Log ("Player UP WINNER");

			//powWinner.SetPowliticoWithType(controller.player2);
			controller.powWinner = winnerMenu.WinnerPow;
			controller.SetPowliticoWinner(controller.player2);
			winnerMenu.WinnerPow = controller.powWinner;
			winnerMenu.SetWinnerMenuAble(true);
			AbleGame(false);

			
		} else if (_downPoints == 3) {
			
			Debug.Log ("Player DOWN WINNER");


			//powWinner.SetPowliticoWithType(controller.player1);

			controller.powWinner = winnerMenu.WinnerPow;
			controller.SetPowliticoWinner(controller.player1);
			winnerMenu.WinnerPow = controller.powWinner;

			winnerMenu.SetWinnerMenuAble(true);
			AbleGame(false);


			
		} else {
			
			ShowDownMenu();
			
//			_textButtonDown.text = "Jogar de novo?";
//			_playerDownButton.gameObject.SetActive(true);
			ChangeStateTo(GameState.Default);
		}
		
		
		
	}

	public void AbleGame(bool able){

		pow1.gameObject.SetActive(able);
		pow2.gameObject.SetActive(able);
		gameArcadeCanvas.SetActive(able);

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
		
		_textCenter.gameObject.SetActive (false);
		
		//_textCenter.text = "POW!";
		
		powImage.SetActive (true);
		//_textCenter.color = Color.white;
		
		ChangeStateTo (GameState.OnGame);

		float timeComp = Random.Range (0.25F, 0.55F); //Tempo de resposta do Adversário

		yield return new WaitForSeconds (timeComp);

		CompHit ();
		
	}

	private void CompHit(){

		if (_playerDownTouched)
			return;

		_compHitted = true;
		StartCoroutine ("CompWinner"); 

	}


	private void ShowDownMenu(){
		
		//		_painelDown.transform.position = Vector3.Lerp (_painelDown.transform.position, _menuDownPosition, 100);
		_painelDown.gameObject.SetActive (true);
		
	}
	
	private void HideDownMenu(){
		
		//		Vector3 newPosition = new Vector3 (_menuDownPosition.x, _menuDownPosition.y - 200,  _menuUpPosition.z);
		//		_painelDown.transform.position = Vector3.Lerp (_painelDown.transform.position, newPosition, 100);
		
		_painelDown.gameObject.SetActive (false);
		
	}

	private IEnumerator CompWinner(){


		UpHitPie ();
		
		yield return new WaitForSeconds (1.3f);

		powImage.SetActive (false);
		_textCenter.color = Color.black;
		_textCenter.gameObject.SetActive (true);
		_textCenter.text = "Você perdeu!"; 
		
		Image myImage = _scoreDownPlayerUp [_upPoints].GetComponent<Image> ();
		myImage.sprite = pointBlue;
		
		_upPoints++;
		
		ChangeStateTo(GameState.WinnerTime);
		
	}
	
	private IEnumerator DownPlayerWinner(){

		DownHitPie ();
		
		yield return new WaitForSeconds (1.3f);


		powImage.SetActive (false);
		_textCenter.color = Color.black;
		_textCenter.gameObject.SetActive (true);
		_textCenter.text = "Você ganhou!"; 
		
		Image myImage = _scoreDownPlayerDown [_downPoints].GetComponent<Image> ();
		myImage.sprite = pointBlue;

		
		_downPoints++;
		
		ChangeStateTo(GameState.WinnerTime);
		
	}

	private void ResetScore(){
		
		_upPoints = 0;
		_downPoints = 0;
		
		foreach (GameObject pointImage in _scoreDownPlayerDown) {
			
			Image imagePoint = pointImage.GetComponent<Image>();
			imagePoint.sprite = pointWhite;
			
		}
		
		foreach (GameObject pointImage in _scoreDownPlayerUp) {
			
			Image imagePoint = pointImage.GetComponent<Image>();
			imagePoint.sprite = pointWhite;
			
		}

		
	}

	public void PauseGame(){
		
		pausePainel.SetActive (true);
		
	}
	
	public void ContinueGame(){
		
		pausePainel.SetActive (false);
		
	}
	
	public void ResetGame(){
		
		pausePainel.SetActive (false);
		winnerMenu.SetWinnerMenuAble(false);
		AbleGame(true);
		_upPoints = 0;
		_downPoints = 0;
		ResetScore ();
		ShowDownMenu ();
		ChangeStateTo (GameState.Default);
			
	}

	public void PlayAgain(){

		ads.ShowAd();
		ResetGame();
	}

	
	public void MainMenu(){

		Application.LoadLevel ((int)Scenes.MainMenu);
	}

	private void UpHitPie(){
		pow2.gameObject.GetComponent<Animator>().SetTrigger("Pie");
	}
	
	private void DownHitPie(){
		pow1.gameObject.GetComponent<Animator>().SetTrigger("Pie");
	}
	
}

