using UnityEngine;
using System.Collections.Generic;
using Soomla.Store;


public class PowliticosStoreAssets : IStoreAssets {                                                                           

	public int GetVersion() {                                                                                                           
		return 2;
	}
	
	public VirtualCurrency[] GetCurrencies() {                                                                              
		return new VirtualCurrency[]{};
	}
	
	public VirtualGood[] GetGoods() {                                                                                               
		return new VirtualGood[]{CHAR_JEAN_WYLLYS_PRODUCT_ID, CHAR_JAIR_BOLSONARO_PRODUCT_ID, CHAR_DILMA_PRODUCT_ID};
	}
	
	public VirtualCurrencyPack[] GetCurrencyPacks() {                                                        
		return new VirtualCurrencyPack[]{};
	}
	
	public VirtualCategory[] GetCategories() {                                              
		return new VirtualCategory[]{};
	}
	
	///itunesconnect`s ids
	public const string CHAR_JEAN_WYLLYS_ID	= "char_jwyllys";
	public const string CHAR_JAIR_BOLSONARO_ID	= "char_jbolsonaro";
	public const string CHAR_DILMA_ID	= "char_dilma";

	///VIRTUAL GOODS

	/** LifeTimeVGs **/
	// Note: create non-consumable items using LifeTimeVG with PuchaseType of PurchaseWithMarket

	public static VirtualGood CHAR_JEAN_WYLLYS_PRODUCT_ID = new LifetimeVG(
		"Jean Wyllys", 														// name
		"Desbloquei Jean Wyllys",				 							// description
		CHAR_JEAN_WYLLYS_ID,												// item id
		new PurchaseWithMarket(CHAR_JEAN_WYLLYS_ID, 0.99));	// the way this virtual good is purchased

	public static VirtualGood CHAR_JAIR_BOLSONARO_PRODUCT_ID = new LifetimeVG(
		"Jair Bolsonaro", 														// name
		"Desbloquei Jair Bolsonaro",				 							// description
		CHAR_JAIR_BOLSONARO_ID,												// item id
		new PurchaseWithMarket(CHAR_JAIR_BOLSONARO_ID, 0.99));	// the way this virtual good is purchased

	public static VirtualGood CHAR_DILMA_PRODUCT_ID = new LifetimeVG(
		"Dilma", 														// name
		"Desbloquei Dilma",				 							// description
		CHAR_DILMA_ID,												// item id
		new PurchaseWithMarket(CHAR_DILMA_ID, 0.99));	// the way this virtual good is purchased

}	




