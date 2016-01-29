using UnityEngine;
using System.Collections;

public class AudioControl : MonoBehaviour {

	///Fonte de audio de Background
	public AudioSource bgmusic;
	///Fonte de audio de efeitos
	public AudioSource effect;
	///Instancia do Singleton
	private static AudioControl sharedInstance;
	///Enumerator do som de fundo
	private IEnumerator waitBg;

	void Awake (){
		
		///Inicialização do Singleton
		if (sharedInstance == null) {
			
			sharedInstance = this;
			DontDestroyOnLoad (this);
			waitBg = WaitToUpBgMusic(0);
			
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

	///Toca um som de efeito, possível passar em que posição o som precisa começar a tocar
	public void PlaySpeech(AudioClip clip, float offset){
		StopCoroutine (waitBg);
		effect.clip = clip;
		effect.time = offset;
		effect.Play();
		effect.volume = 1.3f;
		waitBg = WaitToUpBgMusic (effect.clip.length);
		StartCoroutine (waitBg);
	}

	IEnumerator WaitToUpBgMusic(float time) {
		bgmusic.volume = 0.1f;
		yield return new WaitForSeconds(time);
		bgmusic.volume = 0.8f;
	}

	///Para os effects sounds
	public void StopEffects(){
		effect.Stop();
	}

}
