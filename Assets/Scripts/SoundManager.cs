using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //AudioSource:スピーカー
    //AudioClip:CD

    [SerializeField] AudioSource audioSourceBGM = default;
    [SerializeField] AudioClip[] audioClips = default;

    [SerializeField] AudioSource audioSourceSE = default;
    [SerializeField] AudioClip[] seClips = default;

    public enum BGM
    { 
        Title,
        Main
    }

    public enum SE
    {
        Touch,
        Destroy
    }
    //シングルトンにしてやる；ゲーム内にただ一つだけのもの；しーんが変わっても破壊されない；どのコードからもアクセスしやすい

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void PlayBGM(BGM bgm)
    {
        audioSourceBGM.clip = audioClips[(int)bgm];
        audioSourceBGM.Play();
    }

    public void PlaySE(SE se)
    {
        audioSourceSE.PlayOneShot(seClips[(int)se]);
    }

}
