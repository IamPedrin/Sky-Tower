using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioManagerInstance { get; private set; }

    Slider audioSliderVolume;
    AudioSource audioSourceBG;

    // Adicione uma variável para armazenar a música da cena
    public AudioClip[] sceneMusic;

    private void Awake()
    {
        if (AudioManagerInstance == null)
        {
            AudioManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        audioSourceBG = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("Volume"))
        {
            audioSourceBG.volume = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            audioSourceBG.volume = 0.5f;
            PlayerPrefs.SetFloat("Volume", audioSourceBG.volume);
        }
    }

    void OnSceneLoad(Scene scene, LoadSceneMode sceneMode)
    {
        // Verifique a cena atual e configure a música conforme necessário
        if (scene.buildIndex == 1)
        {
            audioSliderVolume = FindObjectOfType<Slider>();
            audioSliderVolume.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
            audioSliderVolume.value = audioSourceBG.volume;
        }
        else if (scene.buildIndex == 2)
        {
            // Troque a música da cena
            ChangeSceneMusic(1);
        }
        else if (scene.buildIndex == 3)
        {
            // Troque a música da cena
            ChangeSceneMusic(2);
        }
    }

    void OnSliderValueChanged()
    {
        print("Mudou o valor do slider!");
        audioSourceBG.volume = audioSliderVolume.value;
        PlayerPrefs.SetFloat("Volume", audioSliderVolume.value);
    }

    // Adicione um método para trocar a música da cena
    public void ChangeSceneMusic(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < sceneMusic.Length)
        {
            audioSourceBG.clip = sceneMusic[sceneIndex];
            audioSourceBG.Play();
        }
        else
        {
            Debug.LogError("Índice de cena inválido para troca de música!");
        }
    }
}