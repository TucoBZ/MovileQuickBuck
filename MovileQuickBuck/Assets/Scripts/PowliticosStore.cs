using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Soomla.Store;
using System;

public enum CharType {RANDOM, JWYLLYS, JBOLSONARO, DILMA}

public class PowliticosStore : MonoBehaviour {

	public SelectionPanel selectionPanel;

	public Button randomBT;
	public Button undoBT;
	public Button confirmBT;
	public Button versusBT;
	public Animator versusAnimator;
	public Text p1Name;
	public Text p2Name;
	public Text gameMode;


	public Powlitico pow1;
	public Powlitico pow2;

	public BuyCharButton[] charButtons;
	private GameController controller;

	public PurchaseAlert purchase;
	public AudioClip urna;
	//public AudioClip _mainMusic;
	
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
		

		//PlayerPrefs.DeleteAll ();
		//Ajuste do Controller
		GameObject gmControl = GameObject.FindGameObjectWithTag ("GameController");

		if (gmControl != null) {
			controller = gmControl.GetComponent<GameController>();
			controller.player1Name = p1Name;
			controller.player2Name = p2Name;
			controller.pow1 = pow1;
			controller.pow2 = pow2;
			controller.versusBT = versusBT;
			controller.ResetGame ();

			if (controller.isInArcadeMode()) {
				gameMode.text = "Arcade";	
			} else {
				gameMode.text = "Multiplayer";
			}

			//Seta os botões da tela de seleção
			selectionPanel.SetButtonsWithPowliticosArray (controller.powliticos);

		}



		if (!SoomlaStore.Initialized)
			SoomlaStore.Initialize (new PowliticosStoreAssets ());
		else
			OnSoomlaStoreInitialized ();
		
		//AudioManager.GetInstance ().PlayMusicWithPriority1 (_beachSound, 0.8f);
		//AudioManager.GetInstance ().PlayMusicWithPriority2 (_mainMusic, 0.3f);
		
	}

	void Update(){

		if (controller != null) {
			if (controller.isReady ()) {
				SetAllButtonsInteractable(false);
				versusAnimator.SetBool("ready",true);
			} else if (versusAnimator.GetBool("ready")){
				versusAnimator.SetBool("ready",false);
			}

			
		}
	
	}

	private void SelectButton(int index){

		selectionPanel.UnselectAllButtons();
		selectionPanel.buttons[index].SelectButton();

		SetAllButtonsInteractable (true);

		SelectCharInController(selectionPanel.buttons[index].buttonType);

	}

	private void SelectCharInController(CharType type){

		controller.changePlayerSelection(type);
	
	}


	public void RandomBT(){

		selectionPanel.UnselectAllButtons();
		controller.NullConfirm ();
		if (!controller.isInArcadeMode ()) {
			controller.PlaySoundEffect(urna, 0.6f);
		}
	}

	public void CancelBT(){

		controller.UndoSelection ();
		SetAllButtonsInteractable(true);

	}

	public void ConfirmBT(){

		controller.ConfirmSelection();
		selectionPanel.UnselectAllButtons();
		if (!controller.isInArcadeMode ()) {
			controller.PlaySoundEffect(urna, 0.6f);
		}

	}

	public void Back(){

		Application.LoadLevel(0);

	}

	public void StartVS(){

		if (controller != null) {
			if(controller.isInArcadeMode()){
				controller.CheckRandom();
				Application.LoadLevel(3);
			}else{
				controller.CheckRandom();
				Application.LoadLevel(2);
			}

		}
		
	}

	
	public void BuyChar (int index)
	{

		SetAllButtonsInteractable (false);

		switch (selectionPanel.buttons[index].buttonType) {
		case CharType.RANDOM:

			SelectButton(index);

			break;
		case CharType.JWYLLYS:
			if (StoreInventory.GetItemBalance(PowliticosStoreAssets.CHAR_JEAN_WYLLYS_ID) > 0){
				//Select this char
				SelectButton(index);
				
			}else{
				
				LoadAlertPurchase(CharType.JWYLLYS);
			}							
			break;
		case CharType.JBOLSONARO:
			if (StoreInventory.GetItemBalance(PowliticosStoreAssets.CHAR_JAIR_BOLSONARO_ID) > 0){
				//Select this char
				SelectButton(index);
				
			}else{
				
				LoadAlertPurchase(CharType.JBOLSONARO);
			}
			break;
		case CharType.DILMA:
			if (StoreInventory.GetItemBalance(PowliticosStoreAssets.CHAR_DILMA_ID) > 0){
				//Select this char
				SelectButton(index);
				
			}else{
				
				LoadAlertPurchase(CharType.DILMA);
			}							
			break;
		default:
			
			break;

		}

	}
	
	public IEnumerator buyChar(CharType type){
		try {
			
			switch (type) {
			case CharType.JWYLLYS:

				StoreInventory.BuyItem (PowliticosStoreAssets.CHAR_JEAN_WYLLYS_ID);
				
				break;
			case CharType.JBOLSONARO:

				StoreInventory.BuyItem (PowliticosStoreAssets.CHAR_JAIR_BOLSONARO_ID);

				break;
			case CharType.DILMA:
				
				StoreInventory.BuyItem (PowliticosStoreAssets.CHAR_DILMA_ID);
				
				break;
			default:
				break;
				
			}
		} catch (Exception e) {
			
			//_loadingScreen.DestroyThisLoading();
			Debug.Log ("unity/soomla:" + e.Message);
		}
		yield return null;
	}

	private void SetAllButtonsInteractable (bool interactable)
	{
		selectionPanel.SetAllButtonsInteractable(interactable);
	}

	private void LoadAlertPurchase(CharType type){

		if (Application.internetReachability != NetworkReachability.NotReachable) {

			GameObject req = Resources.Load<GameObject> ("Prefabs/AlertPurchase_Canvas");
			GameObject inst = Instantiate (req as GameObject, Vector3.zero, Quaternion.identity) as GameObject;
			purchase = inst.GetComponent<PurchaseAlert> ();
			purchase.purchase = buyChar (type);
			SetAllButtonsInteractable (true);

		} else {

			LoadAlertConnection();

		}
		
	}
	
	private void LoadAlertConnection(){

		GameObject req = Resources.Load<GameObject> ("Prefabs/AlertInternet_Canvas");
		Instantiate (req as GameObject, Vector3.zero, Quaternion.identity);
		SetAllButtonsInteractable (true);
	
	}


//Soomla ------------------------------------------------------------------------------------------------------

	private void OnSoomlaStoreInitialized ()
	{
		Debug.Log ("OnSoomlaStoreInitialized");
		
		selectionPanel.UnselectAllButtons();
		
	}
	
	public void onCurrencyBalanceChanged (VirtualCurrency virtualCurrency, int balance, int amountAdded)
	{
		Debug.Log ("onCurrencyBalanceChanged");
		
		selectionPanel.UnselectAllButtons();
	}
	
	private void OnItemPurchased (PurchasableVirtualItem pvi, string payload)
	{
		if (purchase)
			purchase.CloseWindow();

		Debug.Log ("OnItemPurchased");
		
		selectionPanel.UnselectAllButtons();
	}
	
	public void onMarketPurchase(PurchasableVirtualItem pvi, string payload,
	                             Dictionary<string, string> extra) {
		
		if (purchase)
			purchase.CloseWindow();
		
		selectionPanel.UnselectAllButtons();
		Debug.Log ("onMarketPurchase");
		
	}
	
	public void onMarketPurchaseCancelled(PurchasableVirtualItem pvi) {
		// pvi - the PurchasableVirtualItem whose purchase operation was cancelled
		
		if (purchase)
			purchase.CloseWindow();
		
		selectionPanel.UnselectAllButtons();
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

}
