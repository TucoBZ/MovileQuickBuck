using UnityEngine;
using System.Collections;

public class MenuPrincipal : MonoBehaviour {

	private GameController controller;

	void Awake(){

		LoadSplashScreen ();

	}

	void Start(){

		//Ajuste do Controller
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		controller.player1Name = null;
		controller.player2Name = null;
		controller.versusBT = null;
		controller.ResetGame ();

	}

	///Abre Alert de compra de AD, senão tiver Internet Abre alert de Conexão
	private void LoadSplashScreen(){
		
		GameObject req = Resources.Load<GameObject> ("Prefabs/SplashScreen_Canvas");
		Instantiate (req as GameObject, Vector3.zero, Quaternion.identity);
		
	}

	public void ArcadeMode(){
		controller.mode = GameMode.ARCADE;
		Application.LoadLevel(1);
	}

	public void MultiplayerMode(){
		controller.mode = GameMode.MULTIPLAYER;
		Application.LoadLevel(1);
	}

	public void Creditos(){
		Application.LoadLevel(4);
	}

	public void Politicos(){
		Application.LoadLevel(5);
	}

}
