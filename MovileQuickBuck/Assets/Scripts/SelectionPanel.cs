using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectionPanel : MonoBehaviour {

	public BuyCharButton[] buttons;

	/// <summary>
	/// Sets the buttons with powliticos array.
	/// </summary>
	/// <param name="powliticos">Array de Powliticos.</param>
	public void SetButtonsWithPowliticosArray(Powlitico[] powliticos){

		//seta cada botão dado um powlitico
		for (int i = 0; i < powliticos.Length; i++) {

			//caso o Powlitico for Random, quebra a sequência
			if (powliticos[i].type != CharType.RANDOM){

				buttons[i].unselectedImage = powliticos[i].sprites.UnselectedHead;
				buttons[i].selectedImage = powliticos[i].sprites.SelectedHead;
				buttons[i].buttonType = powliticos[i].type;
				buttons[i].UnselectButton();


			} else {
				break;
			}
		}
	} 

	//Deseleciona todos os botões
	public void UnselectAllButtons(){
		foreach (BuyCharButton bt in buttons) {
			if(bt.buttonType != CharType.RANDOM){
				bt.UnselectButton();
			}
		}
	}

	//Deixa os botões interagiveis ou não
	public void SetAllButtonsInteractable (bool interactable)
	{
		foreach (BuyCharButton button in buttons) {
			button.SetInteractable(interactable);
		}
		
	}
}
