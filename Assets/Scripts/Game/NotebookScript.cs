//using Rewired;
using KOTLIN.Interactions;
using UnityEngine;

public class NotebookScript : Interactable
{

    public GameControllerScript gc;

    public BaldiScript bsc;

    public float respawnTime;

    public bool up;

    public Transform player;

    public GameObject learningGame;

    public AudioSource audioDevice;

    public bool noMath;
    private void Start()
    {
        //this.playerInput = ReInput.players.GetPlayer(0);
        this.up = true;
    }

    private void Update()
    {
        if (this.gc.mode == "endless")
        {
            if (this.respawnTime > 0f)
            {
                if ((base.transform.position - this.player.position).magnitude > 60f)
                {
                    this.respawnTime -= Time.deltaTime;
                }
            }
            else if (!this.up)
            {
                base.transform.position = new Vector3(base.transform.position.x, 4f, base.transform.position.z);
                this.up = true;
                this.audioDevice.Play();
            }
        }
    }

    public override void Interact()
    {
        // Debug.LogError("Interacted"); Originally used for testing -al_ex427
        this.bsc.Hear(base.transform.position, 1f);
        base.transform.position = new Vector3(base.transform.position.x, -20f, base.transform.position.z);
        this.up = false;
        this.respawnTime = 120f;
        this.gc.CollectNotebook(1);
        if (this.noMath)
        {
            this.gc.player.stamina = 100;

            if (this.gc.notebooks == 1 & !this.gc.spoopMode)
            {
                this.gc.quarter.SetActive(true);
                this.gc.tutorBaldi.PlayOneShot(this.gc.aud_Prize);
            }

            if (this.gc.notebooks == 2)
            {
                this.gc.ActivateSpoopMode();
            }

            if (this.gc.spoopMode)
            {
                this.bsc.GetAngry(1f);
            }

            if (this.gc.notebooks == this.gc.MaxNotebooks & this.gc.mode == "story")
            {

                this.audioDevice.PlayOneShot(this.gc.aud_AllNotebooks, 0.8f);
            }
        }
        else
        {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.learningGame);
            gameObject.GetComponent<MathGameScript>().gc = this.gc;
            gameObject.GetComponent<MathGameScript>().baldiScript = this.bsc;
            gameObject.GetComponent<MathGameScript>().playerPosition = this.player.position;
        }


    }


    //private Player playerInput;
}