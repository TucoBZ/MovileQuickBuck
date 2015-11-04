using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectPolitico : MonoBehaviour {


	public Text name;
	public Text partido;
	public Text twiiter;
	public Text descricao;
	public Powlitico powlitico;


	public BuyCharButton[] charButtons;

	public string[] names;
	public string[] partidos;
	public string[] twiiters;
	public string[] descricaos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void back(){
		Application.LoadLevel (0);	
	}

	public void SelectButton(int index){

		CharType typeChar = 0;

		switch (index) {
		case 0:
			typeChar = CharType.JBOLSONARO;
			
			break;
		case 1:
			typeChar = CharType.JWYLLYS;
			
			break;
			
		case 2:
			typeChar = CharType.DILMA;
			
			break;
		default:
			
			break;
			
		}


		CheckButtons ();

		foreach (BuyCharButton bt in charButtons) {
			if (bt.buttonType == typeChar){
				bt.SelectButton();
			}
		}

		//powlitico.SetPowliticoWithType (typeChar);

		SelectCharInController (index);
		
	}

	private void SelectCharInController(int index){

		name.text = names [index];
		partido.text = partidos [index];
		twiiter.text = twiiters [index];
		descricao.text = descricaos [index];


	}

	private void CheckButtons(){
		
		foreach(BuyCharButton bt in charButtons){

			switch (bt.buttonType) {
			case CharType.JWYLLYS:
				//bt.checkAble(0);
				
				break;
			case CharType.JBOLSONARO:
				//bt.checkAble(0);
				
				break;
				
			case CharType.DILMA:
				//bt.checkAble(0);
				
				break;
			default:
				
				break;
				
			}
		}
		

	}


}
