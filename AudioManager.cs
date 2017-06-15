using UnityEngine;

[System.Serializable]
public class AudioModel {
	public string name;
	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = 0.7f;

	[Range(0.5f, 1.5f)]
	public float pitch = 1f;

	public bool loop = false;

	GameObject element;

	public void SetupSource(){
		element = new GameObject ("Sound_" + name );
		element.AddComponent<AudioSource> ();
		AudioSource hitSound = element.GetComponent<AudioSource> ();
		hitSound.playOnAwake = false;
		hitSound.clip = clip;
		hitSound.volume = volume;
		hitSound.pitch = pitch;
		hitSound.loop = loop;
		GameObject.DontDestroyOnLoad (element);
	}

	public void Play(){
		if (element == null) {
			Debug.Log ("element has been destroyed");
			SetupSource ();
		}
		AudioSource hitSound = element.GetComponent<AudioSource> ();
		hitSound.Play();
	}

	public void Stop()
	{
		AudioSource hitSound = element.GetComponent<AudioSource> ();
		hitSound.Stop ();
	}
}

public class AudioManager : MonoBehaviour {

	public AudioModel[] audioList;

	public static AudioManager instance = null; 

	void Awake()
	{
		if (instance == null) {
			instance = this;
			InitSetup ();
			GameObject.DontDestroyOnLoad(gameObject);
		}
		else if (instance != this) {
			Destroy(gameObject);   
		} 

	}

	void InitSetup(){
		for (int i = 0; i < audioList.Length; i++) {
			audioList [i].SetupSource ();
		}
	}

	public void PlaySound(string _name){
		for (int i = 0; i < audioList.Length; i++) {
			if (audioList [i].name == _name) {
				audioList [i].Play ();
				return;
			}
		}
			
		Debug.LogWarning("can't found audio name: " + _name);
	}

	public void StopSound(string _name){
		for (int i = 0; i < audioList.Length; i++) {
			if (audioList [i].name == _name) {
				audioList [i].Stop ();
				return;
			}
		}

		Debug.LogWarning("can't found audio name: " + _name);
	}

}
