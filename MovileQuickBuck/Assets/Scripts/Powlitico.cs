using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Soomla.Store;

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

[System.Serializable]
public class PowliticoSpriteRenderer {
	public SpriteRenderer Head;
	public SpriteRenderer Body;
	public SpriteRenderer FootL;
	public SpriteRenderer FootR;
	public SpriteRenderer HandL;
	public SpriteRenderer HandR;
}

[System.Serializable]
public class PowliticoStoreValues {
	public string PRODUCT_ID;
	public string Name;
	public string Description;
	public double Value;


	public  VirtualGood PowliticoVirtualProduct (){
		return new LifetimeVG(
			Name, 										// name
			Description,				 				// description
			PRODUCT_ID,									// item id
			new PurchaseWithMarket(PRODUCT_ID,Value));	// the way this virtual good is purchased
	}

}


[System.Serializable]
public class PowliticoInfo {
	public string Nome;
	public string Partido;
	public string Twitter;
	public string Description;
}

[ExecuteInEditMode]
public class Powlitico : MonoBehaviour {

	public CharType type;
	public PowliticoSprites sprites;
	public PowliticoSpriteRenderer renderers;
	public PowliticoStoreValues storeValues;
	public PowliticoInfo info;

	public bool updateSkin = false;
	
	public void setSkin(){
		renderers.Head.sprite = sprites.Head;
		renderers.Body.sprite = sprites.Body;
		renderers.HandL.sprite = sprites.HandL;
		renderers.HandR.sprite = sprites.HandR;
		renderers.FootL.sprite = sprites.FootL;
		renderers.FootR.sprite = sprites.FootR;
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

}
