using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyCharButton : MonoBehaviour {

	public CharType buttonType;
	private Image buttonImage;

	public void checkAble(int able){

		if (able > 0) {
			buttonImage.color = Color.blue;
		}else{
			buttonImage.color = Color.black;
		}
	}

	public void SetInteractable (bool interactable){

		gameObject.GetComponent<Button> ().interactable = interactable;
	
	}

	// Use this for initialization
	void Start () {
		buttonImage = this.GetComponent<Image>();
	}

}
