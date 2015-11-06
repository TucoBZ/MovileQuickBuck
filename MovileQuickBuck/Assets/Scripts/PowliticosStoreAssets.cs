using UnityEngine;
using System.Collections.Generic;
using Soomla.Store;

public class PowliticosStoreAssets : IStoreAssets {                                                                           

	private VirtualGood[] myGoods;
	private int myVersion;

	public PowliticosStoreAssets(VirtualGood[] goods, int version){
		myGoods = goods;
		myVersion = version;
	}

	public int GetVersion() {                                                                                                           
		return myVersion;
	}
	
	public VirtualCurrency[] GetCurrencies() {                                                                              
		return new VirtualCurrency[]{};
	}
	
	public VirtualGood[] GetGoods() {    
		return myGoods;
	}
	
	public VirtualCurrencyPack[] GetCurrencyPacks() {                                                        
		return new VirtualCurrencyPack[]{};
	}
	
	public VirtualCategory[] GetCategories() {                                              
		return new VirtualCategory[]{};
	}

}	




