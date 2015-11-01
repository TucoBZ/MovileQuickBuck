using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PurchaseAlert : MonoBehaviour {

	public IEnumerator purchase;
	public Button purchaseBT;
	public Button closeBT;

	// Use this for initialization
	void Start () {
	
	}

	public void BuyItem(){
		purchaseBT.interactable = false;
		closeBT.interactable = false;
		StartCoroutine (purchase);
	}

	public void CloseWindow(){
		
		Destroy (gameObject);
		
	}
}
