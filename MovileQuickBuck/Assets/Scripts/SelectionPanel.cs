using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectionPanel : MonoBehaviour {

	public BuyCharButton[] buttons;

	public void SetButtonsWithPowliticosArray(Powlitico[] powliticos){
		for (int i = 0; i < powliticos.Length; i++) {
		
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

	public void UnselectAllButtons(){
		foreach (BuyCharButton bt in buttons) {
			if(bt.buttonType != CharType.RANDOM){
				bt.UnselectButton();
			}
		}
	}

	public void SetAllButtonsInteractable (bool interactable)
	{
		foreach (BuyCharButton button in buttons) {
			button.SetInteractable(interactable);
		}
		
	}
}
