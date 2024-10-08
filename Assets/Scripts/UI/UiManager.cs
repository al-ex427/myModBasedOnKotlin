using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Data")]

    [Header("Notebook")]

    [SerializeField] private AudioClip aud_CollectNotebook;

    [SerializeField] private TMP_Text notebookCount;

    [SerializeField] private Animator notebookImg;

    [Header("Reticle")]

    [SerializeField] private Image reticle;

    [SerializeField] private Sprite rectOn;

    [SerializeField] private Sprite rectOff;

    [Header("Animators")]

    [SerializeField] private Animator animator;


    public void UpdateNotebookCount(int notebooks, int maxNotebooks)
    {
        this.notebookImg.Play("NotebookSpin", -1, 0f);
        Singleton<GameControllerScript>.Instance.audioDevice.PlayOneShot(this.aud_CollectNotebook);
        this.notebookCount.text = notebooks.ToString() + $"/{maxNotebooks}";
       
    }

    public void UpdateReticle(bool clickable)
    {
        if (clickable)
        {
            reticle.sprite = rectOn;
        }
        else if (!clickable)
        {
            reticle.sprite = rectOff;
        }
    }

    public void ActivateBaldicator(bool heard)
    {
        if (heard)
        {
            animator.Play("Baldicator_Look", -1, 0f);
        }
        else
        {
            animator.Play("Baldicator_Think", -1, 0f);
        }
    }

}
