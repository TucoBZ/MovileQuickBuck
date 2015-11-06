using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyCharButton : MonoBehaviour {

	public CharType buttonType;
	private Image buttonImage;

	public Sprite selectedImage;
	public Sprite unselectedImage;


	public void SetInteractable (bool interactable){

		gameObject.GetComponent<Button>().interactable = interactable;
	
	}

	// Use this for initialization
	void Awake () {
		buttonImage = gameObject.GetComponent<Image>();
	}

	public void SelectButton(){
		if (buttonType != CharType.RANDOM) {
			buttonImage.sprite = selectedImage;
		}
	}

	public void UnselectButton(){
		if (buttonType != CharType.RANDOM) {
			buttonImage.sprite = unselectedImage;
		}	
	}
	
}
