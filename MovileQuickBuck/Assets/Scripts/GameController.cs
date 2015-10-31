using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameMode {ARCADE, MULTIPLAYER}
public enum GameSelection {P1_CONFIRM, P2_CONFIRM, READY}

public class GameController : MonoBehaviour {

	public GameMode mode = GameMode.ARCADE;

	private CharType player1 = CharType.RANDOM; 
	private CharType player2 = CharType.RANDOM; 

	public Text player1Name;
	public Text player2Name;

	public Button versusBT;

	private GameSelection selectStatus = GameSelection.P1_CONFIRM;


	/// <summary>
	/// Changes the player selection.
	/// </summary>
	/// <param name="player"> 0 = Player1 / 1 = Player2.</param>
	/// <param name="type">Type.</param>
	
	public void ResetGame(){

		player1 = CharType.RANDOM; 
		player2 = CharType.RANDOM; 

		SetName();

		versusBT.interactable = false;

		selectStatus = GameSelection.P1_CONFIRM;
	}

	public void SetName(){
	
		player1Name.text = CharName(player1);
		player2Name.text = CharName(player2);
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
		string name = "";

		switch (type) {

		case CharType.RANDOM:
			name = "Aleatório";
			break;
		case CharType.JWYLLYS:
			name = "Jean Wyllys";
			break;
		case CharType.JBOLSONARO:
			name = "Jair Bolsonaro";
			break;
		default:
			break;
		}

		return name;
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
}
