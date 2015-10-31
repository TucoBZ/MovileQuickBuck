using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public interface TouchObserver {
	
	void OnTouchPlayerUp ();
	void OnTouchPlayerDown ();
}

public class GestureDetectureBang : MonoBehaviour {



	///Instancia do Singleton
	private static GestureDetectureBang sharedInstance;


	///Lista de Ouvintes desta classe, que receberam informações quando ocorrer um determinado toque na tela
	private List<TouchObserver> messageTargets = null;


	void Awake (){
		
		///Inicialização do Singleton
		if (sharedInstance == null) {
			
			sharedInstance = this;
			messageTargets = new List<TouchObserver>();
			
		} else {
			Destroy(gameObject);
		}
	}

	void Update () 
	{

		///Verifico a quantidade de toques na tela
		int nbTouches = Input.touchCount;
		
		///Se for maior q 0, verifico o que será feito com cada um desses toques
		if(nbTouches > 0)
		{
			///Para cada toque
			for (int i = 0; i < nbTouches; i++)
			{
				///Pego o toque de posição i no Array de toques
				Touch touch = Input.GetTouch(i);
				
				///Verifico qual é a fase do toque
				TouchPhase phase = touch.phase;
				
				switch(phase)
				{
				case TouchPhase.Began:

					if(touch.position.y > Screen.height/2){

						SendMessageToTargets (true);

					}else {

						SendMessageToTargets (false);
					
					}

					break;
				}
			}
		}
	}


	///Envio mensagem para os meus Listeners
	private void SendMessageToTargets(bool isPlayerUp) {

		foreach(TouchObserver go in messageTargets){
			if(isPlayerUp){
				go.OnTouchPlayerUp();
			}else{
				go.OnTouchPlayerDown();
			}
		}
	}

	///Devolvo a instância deste Singleton
	public static GestureDetectureBang GetSharedGestureDetector (){
		return sharedInstance;
	}

	///Adiciono um Ouvinte na minha lista
	public void AddListener(TouchObserver go){
		messageTargets.Add (go);
	}
	
	///Removo um Ouvinte especifico da minha lista
	public void RemoveListener(TouchObserver go){
		messageTargets.Remove(go);
	}
}
