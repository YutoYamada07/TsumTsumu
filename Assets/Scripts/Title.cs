using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    //�{�^������������Main�ɑJ��

    private void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM.Title);
    }
    public void OnStartButton()
    {
        SceneManager.LoadScene("Main");
    }
}
