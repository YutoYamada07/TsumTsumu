using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //AudioSource:�X�s�[�J�[
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
    //�V���O���g���ɂ��Ă��G�Q�[�����ɂ���������̂��́G���[�񂪕ς���Ă��j�󂳂�Ȃ��G�ǂ̃R�[�h������A�N�Z�X���₷��

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
