using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum GameMode {ARCADE, MULTIPLAYER}
public enum GameSelection {P1_CONFIRM, P2_CONFIRM, READY}

public class GameController : MonoBehaviour {

	public GameMode mode = GameMode.ARCADE;

	public CharType player1 = CharType.RANDOM; 
	public CharType player2 = CharType.RANDOM; 

	public Powlitico[] powliticos;
	public Text player1Name;
	public Text player2Name;

	public Powlitico pow1;
	public Powlitico pow2;
	public Powlitico powWinner;


	public AudioSource bgmusic;
	public AudioSource effect;

	public Button versusBT;

	private GameSelection selectStatus = GameSelection.P1_CONFIRM;

	///Instancia do Singleton
	private static GameController sharedInstance;

	void Awake (){
		
		///Inicialização do Singleton
		if (sharedInstance == null) {
			
			sharedInstance = this;
			DontDestroyOnLoad (this);
			
		} else {
			Destroy(gameObject);
		}
	}

	public GameController GetInstance (){
		return sharedInstance;
	}


	public void ResetGame(){

		player1 = CharType.RANDOM; 
		player2 = CharType.RANDOM; 

		SetName();

		if (versusBT != null) {
			versusBT.interactable = false;
		}

		selectStatus = GameSelection.P1_CONFIRM;
	}

	public void PlaySoundEffect(AudioClip clip, float offset){
		effect.clip = clip;
		effect.time = offset;
		effect.Play();
	}

	public void SetPowlitico(){
		
		if ((pow1 != null) && (pow2 != null)) {

			//Seta Player 1 na tela
			GameObject newPow1 = Instantiate(powliticoForCharType(player1).gameObject,pow1.gameObject.transform.position,Quaternion.identity) as GameObject;
			newPow1.transform.localScale = pow1.transform.localScale;
			newPow1.transform.rotation = pow1.transform.rotation;
			Destroy(pow1.gameObject);
			pow1 = newPow1.GetComponent<Powlitico>();

			//Seta Player 2 na tela
			GameObject newPow2 = Instantiate(powliticoForCharType(player2).gameObject,pow2.gameObject.transform.position,Quaternion.identity) as GameObject;
			newPow2.transform.localScale = pow2.transform.localScale;
			newPow2.transform.rotation = pow2.transform.rotation;
			Destroy(pow2.gameObject);
			pow2 = newPow2.GetComponent<Powlitico>();

		}
		
	}

	public void SetPowliticoWinner(CharType type){
		
		if (powWinner != null) {
			
			//Set o Ganhador dado o tipo dele
			GameObject newPow = Instantiate(powliticoForCharType(type).gameObject,powWinner.gameObject.transform.position,Quaternion.identity) as GameObject;
			newPow.transform.localScale = powWinner.transform.localScale;
			newPow.transform.rotation = powWinner.transform.rotation;
			newPow.transform.parent = powWinner.transform.parent;
			Destroy(powWinner.gameObject);
			powWinner = newPow.GetComponent<Powlitico>();

		}
		
	}
	public CharType RandomCharType(){

		int range = Random.Range(0, 20);
		range = range % 3;
		range++;	
		Debug.Log (range);
		CharType character = CharType.RANDOM;

		switch (range) {
		case (int)CharType.JWYLLYS:
			character = CharType.JWYLLYS;
			break;
		case (int)CharType.JBOLSONARO:
			character = CharType.JBOLSONARO;
			break;
		case (int)CharType.DILMA:
			character = CharType.DILMA;
			break;
		default:
			break;
		}

		return character;
	}
	
	public void CheckRandom(){
		if (player1 == CharType.RANDOM) {
			player1 = RandomCharType();
		}
		if (player2 == CharType.RANDOM) {
			player2 = RandomCharType();

		}
	}

	public void SetName(){
	
		if ((player1Name != null) && (player2Name != null)) {
			player1Name.text = CharName(player1);
			player2Name.text = CharName(player2);
		}
		SetPowlitico();

	}

	public void changePlayerSelection(CharType type){
		if (isInArcadeMode()) {
			switch (selectStatus) {
			case GameSelection.P1_CONFIRM:
				player1 = type;
				SetName();
				break;
			case GameSelection.P2_CONFIRM:
				//
				break;
			case GameSelection.READY:
				
				break;
			default:
				break;
			}
		} else {
			switch (selectStatus) {
			case GameSelection.P1_CONFIRM:
				player1 = type;
				SetName();
				break;
			case GameSelection.P2_CONFIRM:
				player2 = type;
				SetName();
				break;
			case GameSelection.READY:
				
				break;
			default:
				break;
			}
		}
	}

	public string CharName(CharType type){
		return powliticoForCharType(type).info.Nome;
	}

	public void UndoSelection(){

		if (isInArcadeMode ()) {
			switch (selectStatus) {
			case GameSelection.READY:
				selectStatus = GameSelection.P1_CONFIRM;
				versusBT.interactable = false;
				break;
			default:
				break;
			}
			
		} else {
			switch (selectStatus) {
			case GameSelection.P2_CONFIRM:
				selectStatus = GameSelection.P1_CONFIRM;
				break;
			case GameSelection.READY:
				selectStatus = GameSelection.P2_CONFIRM;
				versusBT.interactable = false;
				break;
			default:
				break;
			}
		}


	}

	public void NullConfirm(){
		changePlayerSelection (CharType.RANDOM);
		ConfirmSelection();
	}

	public void ConfirmSelection(){

		if (isInArcadeMode()) {
			switch (selectStatus) {
			case GameSelection.P1_CONFIRM:
				selectStatus = GameSelection.READY;
				versusBT.interactable = true;
				break;
			default:
				break;
			}

		} else {

			switch (selectStatus) {
			case GameSelection.P1_CONFIRM:
				selectStatus = GameSelection.P2_CONFIRM;
				break;
			case GameSelection.P2_CONFIRM:
				selectStatus = GameSelection.READY;
				versusBT.interactable = true;
				break;
			default:
				break;
			}
		}
	}

	public bool isInArcadeMode(){
		return (mode == GameMode.ARCADE) ? true : false;
	}

	public bool isReady(){
		return (selectStatus == GameSelection.READY) ? true : false;
	}

	public Powlitico powliticoForCharType(CharType type){

		Powlitico powlitico = null;

		foreach(Powlitico pow in powliticos){

			if(type == pow.type){
				powlitico = pow;
				break;
			}
		}

		return powlitico;
	}
}
