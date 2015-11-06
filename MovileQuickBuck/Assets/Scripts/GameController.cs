using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Soomla.Store;

public enum GameMode {ARCADE, MULTIPLAYER}
public enum GameSelection {P1_CONFIRM, P2_CONFIRM, READY}

public class GameController : MonoBehaviour{

	///Modo de Jogo
	public GameMode mode = GameMode.ARCADE;

	///Tipo do personagem do Player1
	public CharType player1 = CharType.RANDOM; 
	///Tipo do personagem do Player2
	public CharType player2 = CharType.RANDOM; 

	///Array de todos os Powliticos
	public Powlitico[] powliticos;

	///Texto que será apresentado o Nome do Personagem do P1
	public Text player1Name;
	///Texto que será apresentado o Nome do Personagem do P2
	public Text player2Name;

	///Objeto que representa o P1
	public Powlitico pow1;
	///Objeto que representa o P2
	public Powlitico pow2;
	///Objeto que representa o Jogador Vencedor, usado tbm na tela de Informações
	public Powlitico powWinner;

	///Fonte de audio de Background
	public AudioSource bgmusic;
	///Fonte de audio de efeitos
	public AudioSource effect;

	///Botão de Versus (Rever isso aqui)
	public Button versusBT;

	///Máquina de estados de seleção de personagem
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

	///Instância atual do GameController
	public GameController GetInstance (){
		return sharedInstance;
	}

	///Reseta as escolhas e a Máq. de Estados do Jogo
	public void ResetGame(){

		player1 = CharType.RANDOM; 
		player2 = CharType.RANDOM; 

		SetName();

		if (versusBT != null) {
			versusBT.interactable = false;
		}

		selectStatus = GameSelection.P1_CONFIRM;
	}

	///Toca um som de efeito, possível passar em que posição o som precisa começar a tocar
	public void PlaySoundEffect(AudioClip clip, float offset){
		effect.clip = clip;
		effect.time = offset;
		effect.Play();
	}

	///Substitui Pow1 e Pow2 pelos tipos indicados em player1 e player2.
	///OBS: Ao usar essa função você perderá a referência dos Powliticos de Pow1 e Pow2 da sua classe
	/// , favor pegar a referência novamente de pow1 e pow2 desta classe.
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

	///Substitui PowWinner pelo tipo indicado.
	///OBS: Ao usar essa função você perderá a referência do Powlitico de da sua classe
	/// , favor pegar a referência novamente de powWinner desta classe.
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

	///Verifica se player1 e player2 são do tipo Random, tocando por personagens jogáveis diferentes
	public void CheckRandom(){

		//Escolho o Player1 caso Random
		if (player1 == CharType.RANDOM) {
			player1 = RandomCharType();
		}

		//Escolho o player2 caso Random ou igual ao player1 e verifico novamente
		if (player2 == CharType.RANDOM || player1 == player2) {
			player2 = RandomCharType();
			CheckRandom();
		}
	}

	///Devolve um personagem jogável aleatório
	public CharType RandomCharType(){
		
		int range = Random.Range(0, 20);
		range = range % (powliticos.Length-1);
		
		return powliticos[range].type;
	}

	///Seta o texto dos nomes, e relouda os Powliticos
	public void SetName(){
	
		if ((player1Name != null) && (player2Name != null)) {
			player1Name.text = CharName(player1);
			player2Name.text = CharName(player2);
		}
		SetPowlitico();

	}

	///Troca a Seleção do tipo do personagem, verificando o estado atual da máquina
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



	/// Volta um Estado da máquina, desfazendo a seleção do personagem
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

	///Confirma a Seleção de Personagem Aleatório, passando para o próximo passo da Máquina
	public void NullConfirm(){
		changePlayerSelection (CharType.RANDOM);
		ConfirmSelection();
	}

	///Confirma a Seleção de Personagem, passando para o próximo passo da Máquina
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

	///Verifica se o jogo está no modo Arcade
	public bool isInArcadeMode(){
		return (mode == GameMode.ARCADE) ? true : false;
	}

	///Verifica se o jogo está em Ready
	public bool isReady(){
		return (selectStatus == GameSelection.READY) ? true : false;
	}

	///Devolve o Nome Real do Personagem tirando da Info do Powlitico
	public string CharName(CharType type){
		return powliticoForCharType(type).info.Nome;
	}

	///Devolve um Powlitico dado o tipo dele
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

	public VirtualGood[] AllVirtualGoods()
	{
		VirtualGood[] goods = new VirtualGood[powliticos.Length-1];

		for (int i = 0; i < powliticos.Length-1; i++) {
			goods[i] = powliticos[i].storeValues.PowliticoVirtualProduct();
		}

		return goods;
	}

//	public VirtualGood[] AllPowliticosGoods{
//		return new VirtualGood[]{powliticos.};
//	}


	
}
