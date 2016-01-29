using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Soomla.Store;
using System;
using UnityEngine.Advertisements;

public class PowliticosStore : MonoBehaviour {

	/// Painel de seleção de Personagens
	public SelectionPanel selectionPanel;

	/// Botão de Aleatório
	public Button randomBT;
	/// Botão de Refazer
	public Button undoBT;
	/// Botão de Confirmar
	public ButtonLabel confirmBT;
	/// Botão de Versus
	public Button versusBT;
	/// Aniamador do Botão de Aleatório
	public Animator versusAnimator;
	///Texto do Nome do Jogador1
	public Text p1Name;
	///Texto do Nome do Jogador2
	public Text p2Name;
	///Texto do Modo de Jogo
	public Text gameMode;

	///Pow do Player1
	public Powlitico pow1;
	///Pow do Player2
	public Powlitico pow2;

	///GameController do Jogo
	private GameController controller;

	///Alerta de Compra que está na tela
	public PurchaseAlert purchase;
	///Som de efeito de urna
	public AudioClip urna;

	private CharType charTypeADS;

	///index de seleção do P1
	private int indexP1;
	///index de seleção do P2
	private int indexP2;



	void Start ()
	{
		//Delete all user prefs, debug only
		//PlayerPrefs.DeleteAll ();

		indexP1 = -1;
		indexP2 = -1;

		//Ajuste do Controller
		GameObject gmControl = GameObject.FindGameObjectWithTag ("GameController");

		if (gmControl != null) {

			//Passo Nomes e Pows
			controller = gmControl.GetComponent<GameController>();
			controller.player1Name = p1Name;
			controller.player2Name = p2Name;
			controller.pow1 = pow1;
			controller.pow2 = pow2;
			//controller.versusBT = versusBT;
			controller.ResetGame ();
			//pow1 = controller.pow1;
			//pow2 = controller.pow2;

			if (controller.isInArcadeMode()) {
				gameMode.text = "Arcade";	
			} else {
				gameMode.text = "Multiplayer";
			}

			//Seta os botões da tela de seleção
			selectionPanel.SetButtonsWithPowliticosArray (controller.powliticos);

		}

		StartStoreEvents();

		ConfirmButtonForControllerState (controller.getSelectionState ());
	}

	void Update(){

	}

	public void ConfirmButtonForControllerState(GameSelection state){
		if (state == GameSelection.READY) {
			SetAllButtonsInteractable (false);
		}
		confirmBT.changeButtonToType(state);
	}

	///Seleciona Visualmente o botão de Seleção
	private void SelectButton(int index){

		switch (controller.getSelectionState()) {
		case GameSelection.P1_CONFIRM:
			indexP1 = index;
			break;
		case GameSelection.P2_CONFIRM:
			indexP2 = index;
			break;
		case GameSelection.READY:

			break;
		default:
			break;
		}

		SelectButtonsForIndexs();
		SetAllButtonsInteractable (true);

		SelectCharInController(selectionPanel.buttons[index].buttonType);

	}

	///Seleciona no Controler o personagem dado o tipo dele
	private void SelectButtonsForIndexs(){

		selectionPanel.UnselectAllButtons();

		if (indexP1 > -1 && indexP2 > -1 && indexP1 == indexP2) {
			selectionPanel.buttons[indexP1].selection = SELECTIONBUTTONTYPE.P1ANDP2SELECTED;
			selectionPanel.buttons[indexP1].SelectButton();
		}

		if (indexP1 > -1 && indexP1 != indexP2) {
			selectionPanel.buttons [indexP1].selection = SELECTIONBUTTONTYPE.P1SELECTED;
			selectionPanel.buttons [indexP1].SelectButton ();
		} 

		if (indexP2 > -1 && indexP1 != indexP2) {
			selectionPanel.buttons[indexP2].selection = SELECTIONBUTTONTYPE.P2SELECTED;
			selectionPanel.buttons[indexP2].SelectButton();
		}
		
	}

	///Seleciona no Controler o personagem dado o tipo dele
	private void SelectCharInController(CharType type){

		controller.changePlayerSelection(type);
	
	}

	///Ação do botão de Aleatório
	public void RandomBT(){

		if (controller.getSelectionState () != GameSelection.READY) {

			switch (controller.getSelectionState()) {
			case GameSelection.P1_CONFIRM:
				indexP1 = -1;
				break;
			case GameSelection.P2_CONFIRM:
				indexP2 = -1;
				break;
			case GameSelection.READY:
				break;
			default:
				break;
			}
			controller.NullConfirm ();
			AudioControl.GetInstance().PlaySoundEffect(urna, 0.6f);
			SelectButtonsForIndexs();	
			ConfirmButtonForControllerState (controller.getSelectionState ());
		}

	}

	///Ação do botão de Corrige
	public void CancelBT(){

		controller.UndoSelection ();

		switch (controller.getSelectionState()) {
		case GameSelection.P1_CONFIRM:
			indexP1 = -1;
			break;
		case GameSelection.P2_CONFIRM:
			indexP2 = -1;
			break;
		case GameSelection.READY:
			break;
		default:
			break;
		}

		SelectButtonsForIndexs();
		SetAllButtonsInteractable(true);
		ConfirmButtonForControllerState (controller.getSelectionState ());
	}

	///Ação do botão de Confirma
	public void ConfirmBT(){

		if (controller.getSelectionState () == GameSelection.READY) {
			StartVS();
		}

		controller.ConfirmSelection();
		//selectionPanel.UnselectAllButtons();
		AudioControl.GetInstance().PlaySoundEffect(urna, 0.6f);

		ConfirmButtonForControllerState (controller.getSelectionState ());

	}

	///Ação do botão de Back
	public void Back(){

		Application.LoadLevel((int)Scenes.MainMenu);

	}

	///Ação do botão de Versus
	public void StartVS(){

		if (controller != null) {
			if(controller.isInArcadeMode()){
				controller.CheckRandom();
				Application.LoadLevel((int)Scenes.Arcade);
			}else{
				controller.CheckRandom();
				Application.LoadLevel((int)Scenes.Multiplayer);
			}
		}
	}

	///Ação dos botões de Seleção
	public void BuyChar (int index)
	{

		SetAllButtonsInteractable (false);

		CharType buttonType = selectionPanel.buttons[index].buttonType; 

		//Caso Diferente de Random verifico se tenho o Pow
		if (buttonType != CharType.RANDOM) {

			//Pego o Pow referente a este indice de botão
			Powlitico powButton = controller.powliticos[index];

			//Verifico se tenho este personagem para compra ou se ele já é desbloqueado
			if (StoreInventory.GetItemBalance (powButton.storeValues.PRODUCT_ID) > 0 || powButton.storeValues.purchaseType == PurchaseType.UNLOCKED) {

				//Seleciono este personagem
				SelectButton(index);
				
			} else {

				if (powButton.storeValues.purchaseType == PurchaseType.ADS){
					//Caso seja desbloqueado por ADS
					LoadADSAlertPurchase(buttonType);

				}else{
					//Senão subo alerta de compra
					LoadAlertPurchase (buttonType);
				}

			}	

		} else {
			SetAllButtonsInteractable (true);
			//SelectButton(index);

		}

		//SelectButton(index);
	}

	///Ação de compra executada pelo botão Comprar
	public IEnumerator buyChar(CharType type){

		if (Application.internetReachability != NetworkReachability.NotReachable) {

			Powlitico pow = controller.powliticoForCharType(type);
			
			try{
				
				//Tento comprar o personagem 
				StoreInventory.BuyItem (pow.storeValues.PRODUCT_ID);
				
			}catch (Exception e) {
				
				if (purchase)
					purchase.CloseWindow();
				
				Debug.Log ("unity/soomla:" + e.Message);
			}
		
		} else {

			purchase.CloseWindow();
			LoadAlertConnection();
			
		}
		yield return null;
	}

	///Ação de compra executada pelo botão ADS
	public IEnumerator ADSChar(CharType type){

		if (Application.internetReachability != NetworkReachability.NotReachable) {
			
			charTypeADS = type;
			ShowRewardedAd ();
			
		} else {

			purchase.CloseWindow();
			LoadAlertConnection();
		}


		yield return null;
	}

	///Seta todos os botões de seleção para ativos ou inativos
	private void SetAllButtonsInteractable (bool interactable)
	{
		selectionPanel.SetAllButtonsInteractable(interactable);
	}

	///Abre Alert de compra, senão tiver Internet Abre alert de Conexão
	private void LoadAlertPurchase(CharType type){

		GameObject req = Resources.Load<GameObject> ("Prefabs/AlertPurchase_Canvas");
		GameObject inst = Instantiate (req as GameObject, Vector3.zero, Quaternion.identity) as GameObject;
		purchase = inst.GetComponent<PurchaseAlert>();
		purchase.purchase = buyChar(type);
		SetAllButtonsInteractable (true);
		
	}

	///Abre Alert de compra de AD, senão tiver Internet Abre alert de Conexão
	private void LoadADSAlertPurchase(CharType type){

		GameObject req = Resources.Load<GameObject> ("Prefabs/AlertADSPurchase_Canvas");
		GameObject inst = Instantiate (req as GameObject, Vector3.zero, Quaternion.identity) as GameObject;
		purchase = inst.GetComponent<PurchaseAlert>();
		purchase.purchase = ADSChar(type);
		SetAllButtonsInteractable (true);
		
	}

	///Abre alert de Conexão
	private void LoadAlertConnection(){

		GameObject req = Resources.Load<GameObject> ("Prefabs/AlertInternet_Canvas");
		Instantiate (req as GameObject, Vector3.zero, Quaternion.identity);
		SetAllButtonsInteractable (true);
	
	}

//ADS------------------------------------------------------------------------------------------------------

	public void ShowAd()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}
	
	public void ShowRewardedAd()
	{
		if (Advertisement.IsReady("rewardedVideo"))
		{
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}
	}
	
	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			//
			// YOUR CODE TO REWARD THE GAMER
			// Give coins etc.
			Powlitico pow = controller.powliticoForCharType(charTypeADS);
			StoreInventory.GiveItem(pow.storeValues.PRODUCT_ID,1);
			purchase.CloseWindow();
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}

//Soomla ------------------------------------------------------------------------------------------------------

	/// Inicializa os Eventos do Soomla e Inicializa o SoomlaStore
	private void StartStoreEvents(){

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
			SoomlaStore.Initialize (new PowliticosStoreAssets(controller.AllVirtualGoods(),controller.powliticos.Length));
		else
			OnSoomlaStoreInitialized ();

	}

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
