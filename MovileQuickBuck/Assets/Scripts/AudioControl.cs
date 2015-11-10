using UnityEngine;
using System.Collections;

public class AudioControl : MonoBehaviour {

	///Fonte de audio de Background
	public AudioSource bgmusic;
	///Fonte de audio de efeitos
	public AudioSource effect;
	///Instancia do Singleton
	private static AudioControl sharedInstance;
	
	void Awake (){
		
		///Inicialização do Singleton
		if (sharedInstance == null) {
			
			sharedInstance = this;
			DontDestroyOnLoad (this);
			
		} else {
			Destroy(gameObject);
		}
	}

	///Instância atual do GameController
	public static AudioControl GetInstance (){
		return sharedInstance;
	}

	///Toca um som de efeito, possível passar em que posição o som precisa começar a tocar
	public void PlaySoundEffect(AudioClip clip, float offset){
		effect.clip = clip;
		effect.time = offset;
		effect.Play();
	}

	///Para os effects sounds
	public void StopEffects(){
		effect.Stop();
	}

}
