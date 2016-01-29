using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class GameMultiplayerManager : MonoBehaviour, TouchObserver {


	//Paineis com os menus
	public GameObject _painelUp;
	public GameObject _painelDown;

	//Botões dos respectivos players
	public Button _playerUpButton;
	public Button _playerDownButton;

	//Texto dos botões
	public Text _textButtonUp;
	public Text _textButtonDown;

	//Texto do centro
	public Text _textCenter;
	public Text _p1NameUp;
	public Text _p2NameUp;
	public Text _p1NameDown;
	public Text _p2NameDown;

	//Personagens
	public Powlitico pow1;
	public Powlitico pow2;

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

	public GameObject[] _scoreUpPlayerUp;
	public GameObject[] _scoreUpPlayerDown;
	public GameObject[] _scoreDownPlayerUp;
	public GameObject[] _scoreDownPlayerDown;

	public Sprite pointBlue;
	public Sprite pointWhite;

	//Pontos dos Players
	private int _upPoints = 0;
	private int _downPoints = 0;

	public GameObject powImage;

	public GameObject pausePainel;
	private GameController controller;
	
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
			controller.player1Name = _p1NameUp;
			controller.player2Name = _p2NameUp;
			controller.pow1 = pow1;
			controller.pow2 = pow2;
			controller.SetName ();
			controller.player1Name = _p1NameDown;
			controller.player2Name = _p2NameDown;
			controller.SetName();
			pow1 = controller.pow1;
			pow2 = controller.pow2;
		}

		_gestureBang = GestureDetectureBang.GetSharedGestureDetector();
		
		_gestureBang.AddListener (this);
		
		ChangeStateTo (GameState.Default);

		ads = new PowliticosADS();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnTouchPlayerUp(){

		if (_playerUpError)
			return;
		
		if (_gameState == GameState.Ready) {
			_playerUpError = true;
			pow2.Fail();
			UpHitPie();

			return;
		}
		
		if (_gameState != GameState.OnGame)
			return;
		
		if (_playerUpTouched == false){
			
			_playerUpTouched = true;
			
			if(_playerDownTouched == false) {
				StartCoroutine("UpPlayerWinner");
			}
		}
		
	}
	
	public void OnTouchPlayerDown(){

		if (_playerDownError)
			return;
		
		if (_gameState == GameState.Ready) {
			_playerDownError = true;
			pow1.Fail();
			DownHitPie ();
			return;
		}
		
		if (_gameState != GameState.OnGame)
			return;
		
		if (_playerDownTouched == false){
			
			_playerDownTouched = true;
			
			if(_playerUpTouched == false) {
				StartCoroutine("DownPlayerWinner");
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

			HideUpMenu();
			_playerUpButton.gameObject.SetActive(false);
			_playerUpReady = true;
			
		} else {
			HideDownMenu();
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

			_textButtonUp.text = "Começar"; 
			_textButtonDown.text = "Começar";

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

			ShowUpMenu();
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
		_textCenter.text = "Preparar!";

		yield return new WaitForSeconds (1f);

		_textCenter.text = "Apontar!";

		yield return new WaitForSeconds (0.5f);
	
		_textCenter.gameObject.SetActive (false);

		float timePow = Random.Range (0.5F, 3.0F);

		Debug.Log (timePow);

		yield return new WaitForSeconds (timePow);

		_textCenter.gameObject.SetActive (false);
	
		//_textCenter.text = "POW!";

		powImage.SetActive (true);
		//_textCenter.color = Color.white;


		if (_playerUpError && _playerDownError) {

			powImage.SetActive (false);
			_textCenter.gameObject.SetActive (true);
			_textCenter.color = Color.black;

			_textCenter.text = "Os dois erraram!";

			ChangeStateTo (GameState.WinnerTime);

		} else {

			ChangeStateTo (GameState.OnGame);

			//yield return new WaitForSeconds (1.5f);
			
//			powImage.SetActive (false);
//			_textCenter.color = Color.black;
//			_textCenter.gameObject.SetActive (false);

		}




	}

	private void ShowUpMenu(){

		//_painelUp.transform.position = Vector3.Lerp (_painelUp.transform.position, _menuUpPosition, 100);
		_painelUp.gameObject.SetActive (true);
	}

	private void HideUpMenu(){

//		Vector3 newPosition = new Vector3 (_menuUpPosition.x, _menuUpPosition.y + 200,  _menuUpPosition.z);
//		_painelUp.transform.position = Vector3.Lerp (_painelUp.transform.position, newPosition, 100);
		_painelUp.gameObject.SetActive (false);

		
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

	private IEnumerator UpPlayerWinner(){

		UpHitPie ();
		
		yield return new WaitForSeconds (1.3f);

		Image myImage = _scoreDownPlayerUp [_upPoints].GetComponent<Image> ();
		myImage.sprite = pointBlue;

		myImage = _scoreUpPlayerUp [_upPoints].GetComponent<Image> ();
		myImage.sprite = pointBlue;

		_upPoints++;

		ChangeStateTo(GameState.WinnerTime);

	}

	private IEnumerator DownPlayerWinner(){

		DownHitPie ();

		yield return new WaitForSeconds (1.3f);

		Image myImage = _scoreDownPlayerDown [_downPoints].GetComponent<Image> ();
		myImage.sprite = pointBlue;
		
		myImage = _scoreUpPlayerDown [_downPoints].GetComponent<Image> ();
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

		foreach (GameObject pointImage in _scoreUpPlayerDown) {
			
			Image imagePoint = pointImage.GetComponent<Image>();
			imagePoint.sprite = pointWhite;
			
		}

		foreach (GameObject pointImage in _scoreUpPlayerUp) {
			
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
		ShowUpMenu ();
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

