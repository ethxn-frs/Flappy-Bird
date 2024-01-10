    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public enum AudioType
    {
        Jump,
        Die,
        Point,
        Hit,
        Swoosh
    }

    public enum AudioSourceType
    {
        Game,
        Player
    }

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        public AudioSource gameSource;
        public AudioSource playerSource;

        [System.Serializable]
        public struct AudioData
        {
            public AudioClip clip;
            public AudioType type;
        }

        public AudioData[] audioData;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            gameSource.volume = PlayerPrefs.GetFloat("SoundState", 1);
            playerSource.volume = PlayerPrefs.GetFloat("SoundState", 1);
        }

        public void PLaySound(AudioType type, AudioSourceType sourceType)
        {

            AudioClip clip = getClip(type);

            if (sourceType == AudioSourceType.Game)
            {
                gameSource.PlayOneShot(clip);
            } else if (sourceType == AudioSourceType.Player)
            {
                playerSource.PlayOneShot(clip);
            }

        }

        AudioClip getClip(AudioType type)
        {
            foreach ( AudioData data in audioData)
            {

                if (data.type == type)
                {
                    return data.clip;
                }
            }

            Debug.LogError("AudioManager : No sound found for : " + type);
            return null;
        }

        public void ToggleSound()
        {
            if (PlayerPrefs.GetFloat("SoundState") > 0f)
            {
                PlayerPrefs.SetFloat("SoundState", 0f);
                UIController.Instance.AlternSoundImage(0);
            }
            else
            {
            PlayerPrefs.SetFloat("SoundState", 1f);
            UIController.Instance.AlternSoundImage(1);
            }

            PlayerPrefs.Save();

        UpdateVolume();
        }

        void UpdateVolume()
        {
            gameSource.volume = PlayerPrefs.GetFloat("SoundState");
            playerSource.volume = PlayerPrefs.GetFloat("SoundState");
        }

    }
