using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Powlitico : MonoBehaviour {

	public Image Head;
	public Image Body;

	public Image FootD;
	public Image FootE;

	public Image HandD;
	public Image HandE;

	public Image RandomImg;

	public void SetPowliticoWithType(CharType type){
		Sprite[] sprites = null;
		switch (type) {
		case CharType.RANDOM:
			AbleImages(false);
			break;
		case CharType.JWYLLYS:
			sprites = Resources.LoadAll<Sprite>("Sprites/jean_wyllys");
			Head.sprite = sprites[0];
			Body.sprite = sprites[2];
			FootD.sprite = sprites[4];
			FootE.sprite = sprites[5];
			HandD.sprite = sprites[1];
			HandE.sprite = sprites[3];
			AbleImages(true);


			break;
		case CharType.JBOLSONARO:
			sprites = Resources.LoadAll<Sprite>("Sprites/QuickBuck_Sprites");
			Head.sprite = sprites[3];
			Body.sprite = sprites[6];
			FootD.sprite = sprites[9];
			FootE.sprite = sprites[10];
			HandD.sprite = sprites[5];
			HandE.sprite = sprites[7];
			AbleImages(true);
			break;

		case CharType.DILMA:
			sprites = Resources.LoadAll<Sprite>("Sprites/sprite_dilma");
			Head.sprite = sprites[0];
			Body.sprite = sprites[2];
			FootD.sprite = sprites[4];
			FootE.sprite = sprites[5];
			HandD.sprite = sprites[1];
			HandE.sprite = sprites[3];
			AbleImages(true);

			break;
		default:
			break;
			
		}
	}

	private void AbleImages(bool able){
		if (able) {
			Color color = Color.white;
			color.a = 0;
			RandomImg.color = color;
		} else {
			Color color = Color.white;
			color.a = 1;
			RandomImg.color = color;
		}

		Head.gameObject.SetActive(able);
		Body.gameObject.SetActive(able);
		FootD.gameObject.SetActive(able);
		FootE.gameObject.SetActive(able);
		HandD.gameObject.SetActive(able);
		HandE.gameObject.SetActive(able);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
