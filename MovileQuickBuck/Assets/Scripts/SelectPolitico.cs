using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectPolitico : MonoBehaviour {


	public Text nametx;
	public Text partido;
	public Text twitter;
	public Text descricao;
	public Powlitico powlitico;
	public SelectionPanel selectionPanel;

	private GameController controller;

	public void Start(){

		GameObject gmControl = GameObject.FindGameObjectWithTag ("GameController");
		
		if (gmControl != null) {
			controller = gmControl.GetComponent<GameController>();

			//Seta os botões da tela de seleção
			selectionPanel.SetButtonsWithPowliticosArray(controller.powliticos);
		}

		//Deixo o primeiro botão selecionado
		SelectChar (0);

	}

	public void back(){
		Application.LoadLevel (0);	
	}

	public void SelectChar (int index)
	{
		//Se o indice do botão precionado for diferente de Random então
		if (selectionPanel.buttons [index].buttonType != CharType.RANDOM) {

			//Seta o personagem
			controller.powWinner = powlitico;
			controller.SetPowliticoWinner (selectionPanel.buttons[index].buttonType);
			powlitico = controller.powWinner;
			
			//Seta o Texto referente ao personagem 
			nametx.text = powlitico.info.Nome;
			partido.text = powlitico.info.Partido;
			twitter.text = powlitico.info.Twitter;
			descricao.text = powlitico.info.Description;
			
			//Seleciona o botão
			selectionPanel.UnselectAllButtons();
			selectionPanel.buttons[index].SelectButton();
		}
	}
}
