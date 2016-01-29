using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonLabel : MonoBehaviour {

	public Text label;
	public Image backgroundImage;

	public GameSelection type;

	public void changeButtonToType(GameSelection buttonType){
		type = buttonType;
		layoutButton (type);
	}

	private void layoutButton(GameSelection buttonType){
		switch (buttonType) {
		case GameSelection.P1_CONFIRM:
			backgroundImage.sprite =  Resources.LoadAll <Sprite> ("Sprites/select_char_2")[5];
			break;
		case GameSelection.P2_CONFIRM:
			backgroundImage.sprite =  Resources.LoadAll <Sprite> ("Sprites/select_char_2")[2];
			break;
		case GameSelection.READY:
			backgroundImage.sprite =  Resources.LoadAll <Sprite> ("Sprites/select_char_2")[6];
			break;
		default:
			break;
		}
		
	}
}
