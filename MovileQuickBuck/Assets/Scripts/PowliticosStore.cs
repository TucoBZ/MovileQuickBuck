using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Soomla.Store;
using System;

public enum CharType {JWYLLYS, JBOLSONARO}

public class PowliticosStore : MonoBehaviour {
	

	public BuyCharButton[] buttons;

	//public AudioClip _beachSound;
	//public AudioClip _mainMusic;
	
	
	//private Loading _loadingScreen;
	
	void Start ()
	{
		
		//ShowStore (0);
		
		StoreEvents.OnSoomlaStoreInitialized 		+= OnSoomlaStoreInitialized;
		StoreEvents.OnItemPurchased 				+= OnItemPurchased;
		StoreEvents.OnCurrencyBalanceChanged 		+= onCurrencyBalanceChanged;
		StoreEvents.OnMarketPurchase 				+= onMarketPurchase;
		StoreEvents.OnMarketPurchaseCancelled 		+= onMarketPurchaseCancelled;
		
		//Callbacks para teste
		StoreEvents.OnMarketPurchaseStarted 		+= onMarketPurchaseStarted;
		StoreEvents.OnGoodBalanceChanged 			+= onGoodBalanceChanged;
		StoreEvents.OnMarketRefund 					+= onMarketRefund;
		StoreEvents.OnMarketItemsRefreshStarted 	+= onMarketItemsRefreshStarted;
		StoreEvents.OnMarketItemsRefreshFinished 	+= onMarketItemsRefreshFinished;
		StoreEvents.OnRestoreTransactionsStarted 	+= onRestoreTransactionsStarted;
		StoreEvents.OnRestoreTransactionsFinished	+= onRestoreTransactionsFinished;
		StoreEvents.OnItemPurchaseStarted			+= onItemPurchaseStarted;
		StoreEvents.OnGoodEquipped 					+= onGoodEquipped;
		StoreEvents.OnGoodUnEquipped 				+= onGoodUnequipped;
		StoreEvents.OnGoodUpgrade 					+= onGoodUpgrade;
		StoreEvents.OnBillingSupported 				+= onBillingSupported;
		StoreEvents.OnBillingNotSupported 			+= onBillingNotSupported;
		
		
		
		
		
		
		if (!SoomlaStore.Initialized)
			SoomlaStore.Initialize (new PowliticosStoreAssets ());
		else
			OnSoomlaStoreInitialized ();
		
		//AudioManager.GetInstance ().PlayMusicWithPriority1 (_beachSound, 0.8f);
		//AudioManager.GetInstance ().PlayMusicWithPriority2 (_mainMusic, 0.3f);
		
	}

	private void CheckButtons(){

		foreach(BuyCharButton bt in buttons){
			int balance;

			switch (bt.buttonType) {
			case CharType.JWYLLYS:
				balance = StoreInventory.GetItemBalance(PowliticosStoreAssets.CHAR_JEAN_WYLLYS_ID);
				bt.checkAble(balance);
		
				break;
			case CharType.JBOLSONARO:
				balance = StoreInventory.GetItemBalance(PowliticosStoreAssets.CHAR_JAIR_BOLSONARO_ID);
				bt.checkAble(balance);

				break;
			default:
				break;
				
			}
		}

		SetAllButtonsInteractable (true);

	}

	private void OnSoomlaStoreInitialized ()
	{
		Debug.Log ("OnSoomlaStoreInitialized");


		CheckButtons ();
		//Verifico os valores atuais do banco
		//amountOfPearlText.text = "" + StoreInventory.GetItemBalance (CrossSeaStoreAssets.CURRENCY1_ID);
		//amountOfCoinsText.text = "" + StoreInventory.GetItemBalance (CrossSeaStoreAssets.CURRENCY2_ID);
		//magnetsText.text = "" + StoreInventory.GetItemBalance (CrossSeaStoreAssets.MAGNET_ID);
		//shieldsText.text = "" + StoreInventory.GetItemBalance (CrossSeaStoreAssets.SHIELD_ID);
		//thundersText.text = "" + StoreInventory.GetItemBalance (CrossSeaStoreAssets.THUNDER_ID);
		

	}
	
	public void onCurrencyBalanceChanged (VirtualCurrency virtualCurrency, int balance, int amountAdded)
	{
		Debug.Log ("onCurrencyBalanceChanged");
		
		
//		if (amountOfCoinsText != null && amountOfPearlText != null) {
//			
//			amountOfPearlText.text = "" + StoreInventory.GetItemBalance (CrossSeaStoreAssets.CURRENCY1_ID);
//			amountOfCoinsText.text = "" + StoreInventory.GetItemBalance (CrossSeaStoreAssets.CURRENCY2_ID);
//			SetAllButtonsInteractable (true);
//		}
		CheckButtons ();
	}
	
	private void OnItemPurchased (PurchasableVirtualItem pvi, string payload)
	{
		
		Debug.Log ("OnItemPurchased");
		
		///magnetsText.text = "" + StoreInventory.GetItemBalance (CrossSeaStoreAssets.MAGNET_ID);
		//shieldsText.text = "" + StoreInventory.GetItemBalance (CrossSeaStoreAssets.SHIELD_ID);
		//thundersText.text = "" + StoreInventory.GetItemBalance (CrossSeaStoreAssets.THUNDER_ID);

		CheckButtons ();
	}
	
	public void onMarketPurchase(PurchasableVirtualItem pvi, string payload,
	                             Dictionary<string, string> extra) {
		// pvi - the PurchasableVirtualItem that was just purchased
		// payload - a text that you can give when you initiate the purchase operation and
		//    you want to receive back upon completion
		// extra - contains platform specific information about the market purchase
		//    Android: The "extra" dictionary will contain: 'token', 'orderId', 'originalJson', 'signature', 'userId'
		//    iOS: The "extra" dictionary will contain: 'receiptUrl', 'transactionIdentifier', 'receiptBase64', 'transactionDate', 'originalTransactionDate', 'originalTransactionIdentifier'
		
		//if (_loadingScreen)
			//_loadingScreen.DestroyThisLoading ();

		CheckButtons ();

		Debug.Log ("onMarketPurchase");
		
	}
	
	
	public void onMarketPurchaseCancelled(PurchasableVirtualItem pvi) {
		// pvi - the PurchasableVirtualItem whose purchase operation was cancelled
		
		//if (_loadingScreen)
			//_loadingScreen.DestroyThisLoading ();

		CheckButtons ();
	
		Debug.Log ("onMarketPurchaseCancelled");
		
	}
	
	//Eventos para test 
	//*******************************************************************************************************************
	public void onGoodBalanceChanged(VirtualGood good, int balance, int amountAdded) {
		// virtualCurrency - the virtual good whose balance was changed
		// balance - the balance of the currency after the change
		// amountAdded - the amount that was added to the currency balance
		//    (in case the number of currencies was removed this will be a negative value)
		
		Debug.Log ("onGoodBalanceChanged");
	}
	
	public void onMarketPurchaseStarted(PurchasableVirtualItem pvi) {
		// your game specific implementation here
		Debug.Log ("onMarketPurchaseStarted");
	}
	
	
	
	
	public void onMarketRefund(PurchasableVirtualItem pvi) {
		// pvi - the PurchasableVirtualItem to refund
		
		Debug.Log ("onMarketRefund");
	}
	
	
	public void onMarketItemsRefreshStarted() {
		Debug.Log ("onMarketItemsRefreshStarted");
	}
	
	public void onMarketItemsRefreshFinished(List<MarketItem> items) {
		// items - the list of Market items that was fetched from the Market
		
		Debug.Log ("onMarketItemsRefreshFinished");
	}
	
	public void onRestoreTransactionsStarted() {
		Debug.Log ("onRestoreTransactionsStarted");
	}
	
	public void onRestoreTransactionsFinished(bool success) {
		// success - true if the restore transactions operation has succeeded
		
		Debug.Log ("onRestoreTransactionsFinished");
	}
	
	public void onItemPurchaseStarted(PurchasableVirtualItem pvi) {
		// pvi - the PurchasableVirtualItem whose purchase operation has just started
		
		Debug.Log ("onItemPurchaseStarted");
	}
	
	public void onGoodEquipped(EquippableVG good) {
		// good - the virtual good that was just equipped
		
		Debug.Log ("onGoodEquipped");
	}
	
	public void onGoodUnequipped(EquippableVG good) {
		// good - the virtual good that was just unequipped
		
		Debug.Log ("onGoodUnequipped");
	}
	
	public void onGoodUpgrade(VirtualGood good, UpgradeVG currentUpgrade) {
		// good - the virtual good that was just upgraded
		// currentUpgrade - the upgrade after the operation completed
		
		Debug.Log ("onGoodUpgrade");
	}
	
	public void onBillingSupported() {
		Debug.Log ("onBillingSupported");
	}
	
	public void onBillingNotSupported() {
		Debug.Log ("onBillingNotSupported");
	}
	
	
	//*******************************************************************************************************************
	

	
	
	public void BuyChar (int type)
	{
		
		//LoadLoading ();

		SetAllButtonsInteractable (false);

		if (Application.internetReachability != NetworkReachability.NotReachable) {
			
			try {
				
				switch (type) {
				case (int)CharType.JWYLLYS:
					StoreInventory.BuyItem (PowliticosStoreAssets.CHAR_JEAN_WYLLYS_ID);
					break;
				case (int)CharType.JBOLSONARO:
					StoreInventory.BuyItem (PowliticosStoreAssets.CHAR_JAIR_BOLSONARO_ID);
					break;
				default:
					break;
					
				}
			} catch (Exception e) {
				
				//_loadingScreen.DestroyThisLoading();
				Debug.Log ("unity/soomla:" + e.Message);
			}
			
		} else {
			
//			StartCoroutine(LoadAlert(
//				"Internet Conection",
//				"You aren't connect to the internet! To access this content try to Reconnect.",
//				"Reconnect",
//				amount,
//				PowerUP.NONE,
//				RewardType.NONE,
//				0,
//				LayoutType.INFO
//				));
//			
//			_loadingScreen.DestroyThisLoading();
			
		}
	}
	

	private void SetAllButtonsInteractable (bool interactable)
	{
		
		foreach (BuyCharButton button in buttons) {
			button.SetInteractable(interactable);
		}
	}
	

	
//	
//	///Load do Loading
//	private void LoadLoading()
//	{
//		
//		GameObject req = Resources.Load<GameObject> ("prefabs/Outros/Canvas_Loading");
//		
//		GameObject go = Instantiate (req as GameObject, Vector3.zero, Quaternion.identity) as GameObject;
//		
//		_loadingScreen = go.GetComponent<Loading> ();
//		
//		_loadingScreen.UpdateInfoText ();
//		
//	}


}
