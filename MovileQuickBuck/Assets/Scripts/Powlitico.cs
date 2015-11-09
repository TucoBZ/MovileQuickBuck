using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Soomla.Store;

//Sprites usadas no personagem
[System.Serializable]
public class PowliticoSprites {
	public Sprite SelectedHead;
	public Sprite UnselectedHead;
	public Sprite Head;
	public Sprite Body;
	public Sprite FootL;
	public Sprite FootR;
	public Sprite HandL;
	public Sprite HandR;
}

//Peças do personagem
[System.Serializable]
public class PowliticoSpriteRenderer {
	public SpriteRenderer Head;
	public SpriteRenderer Body;
	public SpriteRenderer FootL;
	public SpriteRenderer FootR;
	public SpriteRenderer HandL;
	public SpriteRenderer HandR;
}

//Informações de compra do personagem
[System.Serializable]
public class PowliticoStoreValues {
	public string PRODUCT_ID;
	public string Name;
	public string Description;
	public double Value;

	///Produto Virtual deste Personagem, usado no Soomla
	public  VirtualGood PowliticoVirtualProduct (){
		return new LifetimeVG(
			Name, 										// name
			Description,				 				// description
			PRODUCT_ID,									// item id
			new PurchaseWithMarket(PRODUCT_ID,Value));	// the way this virtual good is purchased
	}

}

//Informações do personagem
[System.Serializable]
public class PowliticoInfo {
	public string Nome;
	public string Partido;
	public string Twitter;
	public string Description;
}

//Enum de tipos de personagens
public enum CharType {RANDOM, JWYLLYS, JBOLSONARO, DILMA, ECUNHA}


//Enum de tipos de personagens
public enum PurchaseType {UNLOCKED, ADS, PURCHASE}

[ExecuteInEditMode]
public class Powlitico : MonoBehaviour {

	///O Tipo deste personagem
	public CharType type;
	///Sprites deste personagem
	public PowliticoSprites sprites;
	///Peças deste personagem (não precisa alterar)
	public PowliticoSpriteRenderer renderers;
	///Valores necessário para o Soomla
	public PowliticoStoreValues storeValues;
	///Info deste Personagem
	public PowliticoInfo info;

	public PurchaseType purchaseType;

	public bool updateSkin = false;
	private Animator animator;

	public void setSkin(){
		renderers.Head.sprite = sprites.Head;
		renderers.Body.sprite = sprites.Body;
		renderers.HandL.sprite = sprites.HandL;
		renderers.HandR.sprite = sprites.HandR;
		renderers.FootL.sprite = sprites.FootL;
		renderers.FootR.sprite = sprites.FootR;
	}

	void Start(){
		animator = gameObject.GetComponent<Animator> ();
	}

	#if UNITY_EDITOR
	void Update () 
	{
		if (updateSkin) {
			setSkin();
			updateSkin = false;
		}

	}
	#endif

	//Animation
	public void ThrowPie(){
		animator.SetTrigger("ThrowPie");
	}

	public void Fail(){
		animator.SetBool("Fail",true);
	}

	public void ResetFail(){
		animator.SetBool("Fail",false);
	}

}
