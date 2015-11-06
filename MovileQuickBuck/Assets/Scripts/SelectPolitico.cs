using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectPolitico : MonoBehaviour {

	///Texto de Nome
	public Text nametx;
	///Texto do Partido
	public Text partido;
	///Texto do Twitter
	public Text twitter;
	///Texto de Descrição
	public Text descricao;

	///Powlitico apresentado
	public Powlitico powlitico;
	///Painel de seleção de personagens
	public SelectionPanel selectionPanel;
	///Game controller
	private GameController controller;

	public void Start(){

		//Pego o gameController
		GameObject gmControl = GameObject.FindGameObjectWithTag ("GameController");
		
		if (gmControl != null) {
			controller = gmControl.GetComponent<GameController>();

			//Seta os botões da tela de seleção
			selectionPanel.SetButtonsWithPowliticosArray(controller.powliticos);
		}

		//Deixo o primeiro botão selecionado
		SelectChar (0);

	}

	///Ação do Botão de voltar
	public void back(){
		Application.LoadLevel (0);	
	}

	///Seleciono o Char dado o índice dele
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
