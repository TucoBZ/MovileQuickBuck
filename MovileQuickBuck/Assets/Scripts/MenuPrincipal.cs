using UnityEngine;
using System.Collections;

public class MenuPrincipal : MonoBehaviour {

	private GameController controller;

	void Start(){

		//Ajuste do Controller
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		controller.player1Name = null;
		controller.player2Name = null;
		//controller.versusBT = null;
		controller.ResetGame ();

	}

	public void ArcadeMode(){
		controller.mode = GameMode.ARCADE;
		Application.LoadLevel((int)Scenes.CharSelection);
	}

	public void MultiplayerMode(){
		controller.mode = GameMode.MULTIPLAYER;
		Application.LoadLevel((int)Scenes.CharSelection);
	}

	public void Creditos(){
		Application.LoadLevel((int)Scenes.Credits);
	}

	public void Politicos(){
		Application.LoadLevel((int)Scenes.PowliticosInfo);
	}

}
