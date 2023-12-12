using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioManagerInstance {get; private set; }

    Slider audioSliderVolume;
    AudioSource audioSourceBG;

    // Start is called before the first frame update

    private void Awake(){
        if(AudioManagerInstance == null){
            AudioManagerInstance = FindObjectOfType<AudioManager>();
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    void Start(){
        SceneManager.sceneLoaded += OnSceneLoad;
        audioSourceBG = GetComponent<AudioSource>();

        if(PlayerPrefs.HasKey("Volume")){
            audioSourceBG.volume = PlayerPrefs.GetFloat("Volume");
        }else{
            audioSourceBG.volume = 0.5f; 
            PlayerPrefs.SetFloat("Volume", audioSourceBG.volume);
        }
    }

    void OnSceneLoad(Scene scene, LoadSceneMode sceneMode){
        if(scene.buildIndex == 1){
            audioSliderVolume = FindObjectOfType<Slider>();
            audioSliderVolume.onValueChanged.AddListener(delegate {OnSliderValueChanged();});
            audioSliderVolume.value = audioSourceBG.volume;
        }
    }

    // void OnSceneLoad(Scene scene, LoadSceneMode sceneMode)
    // {
    //     // Pare a música da cena anterior
    //     if (currentSceneMusic != null)
    //     {
    //         audioSourceBG.Stop();
    //     }

    //     // Configure a nova música da cena
    //     if (scene.buildIndex == 1)
    //     {
    //         audioSliderVolume = FindObjectOfType<Slider>();
    //         audioSliderVolume.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
    //         audioSliderVolume.value = audioSourceBG.volume;
    //     }
    //     else if (scene.buildIndex == 2)
    //     {
    //         // Carregue a música da nova cena
    //         public currentSceneMusic = Resources.Load<AudioClip>("Caminho/Para/Sua/MusicaDaCena2");
    //         audioSourceBG.clip = currentSceneMusic;
    //         audioSourceBG.Play();
    //     }
    // }

    void OnSliderValueChanged(){
        print("Mudou o valor do slider!");
        audioSourceBG.volume = audioSliderVolume.value;
        PlayerPrefs.SetFloat("Volume", audioSliderVolume.value);
    }


}
