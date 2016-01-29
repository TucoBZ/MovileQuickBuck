using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum SELECTIONBUTTONTYPE {UNSELECTED, P1SELECTED, P2SELECTED, P1ANDP2SELECTED}

public class BuyCharButton : MonoBehaviour {

	public CharType buttonType;

	public Image background;
	public Image charImg;
	public Image P1;
	public Image P2;

	public SELECTIONBUTTONTYPE selection = SELECTIONBUTTONTYPE.UNSELECTED;

	public Sprite selectedImage;
	public Sprite unselectedImage;

	public Sprite p1bkgImg;
	public Sprite p2bkgImg;
	public Sprite p1andp2bkgImg;

	public void SetInteractable (bool interactable){

		gameObject.GetComponent<Button>().interactable = interactable;
	
	}

	// Use this for initialization
	void Awake () {
		layoutButton (SELECTIONBUTTONTYPE.UNSELECTED);
	}

	public void SelectButton(){
		if (buttonType != CharType.RANDOM) {
			layoutButton (selection);
		}
	}

	public void UnselectButton(){
		selection = SELECTIONBUTTONTYPE.UNSELECTED;
		layoutButton (selection);
	}

	private void layoutButton(SELECTIONBUTTONTYPE selectionType){
		switch (selectionType) {
		case SELECTIONBUTTONTYPE.UNSELECTED:
			charImg.sprite = unselectedImage;
			P1.gameObject.SetActive(false);
			P2.gameObject.SetActive(false);
			background.gameObject.SetActive(false);
			break;
		case SELECTIONBUTTONTYPE.P1SELECTED:
			charImg.sprite = selectedImage;
			P1.gameObject.SetActive(true);
			P2.gameObject.SetActive(false);
			background.sprite = p1bkgImg;
			background.gameObject.SetActive(true);
			break;
		case SELECTIONBUTTONTYPE.P2SELECTED:
			charImg.sprite = selectedImage;
			P1.gameObject.SetActive(false);
			P2.gameObject.SetActive(true);
			background.sprite = p2bkgImg;
			background.gameObject.SetActive(true);
			break;
		case SELECTIONBUTTONTYPE.P1ANDP2SELECTED:
			charImg.sprite = selectedImage;
			P1.gameObject.SetActive(true);
			P2.gameObject.SetActive(true);
			background.sprite = p1andp2bkgImg;
			background.gameObject.SetActive(true);

			break;
		default:
			break;
		}

	}
	
}
