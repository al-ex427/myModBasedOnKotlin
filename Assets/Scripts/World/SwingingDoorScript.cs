using NaughtyAttributes;
using UnityEngine;

public class SwingingDoorScript : MonoBehaviour
{
    public bool passable = true; 
    private void Start()
    {
        this.myAudio = base.GetComponent<AudioSource>();
        this.bDoorLocked = true;
        gc = GameControllerScript.Instance;
        baldi = gc.baldiScrpt;
    }

    private void Update()
    {
        if (!this.requirementMet & this.gc.notebooks >= 2)
        {
            this.requirementMet = true;
            this.UnlockDoor();
        }
        if (this.openTime > 0f)
        {
            this.openTime -= 1f * Time.deltaTime;
        }
        if (this.lockTime > 0f)
        {
            this.lockTime -= Time.deltaTime;
        }
        else if (this.bDoorLocked & this.requirementMet)
        {
            this.UnlockDoor();
        }
        if (this.openTime <= 0f & this.bDoorOpen & !this.bDoorLocked)
        {
            this.bDoorOpen = false;
            if (DifferentSides)
            {
                this.inside.material = this.inClosed;
                this.outside.material = this.outClosed;
                return;
            }

            this.inside.material = this.closed;
            this.outside.material = this.closed;
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (!this.bDoorLocked && passable)
        {
            this.bDoorOpen = true;
            if (DifferentSides)
            {
                this.inside.material = this.inOpen;
                this.outside.material = this.outOpen;
            }
            else
            {
                this.inside.material = this.open;
                this.outside.material = this.open;
            }

            this.openTime = 2f;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!(this.gc.notebooks < 2 & other.tag == "Player"))
        {
            if (!this.bDoorLocked && passable)
            {
                this.myAudio.PlayOneShot(this.doorOpen, 1f);
                if (other.tag == "Player" && this.baldi.isActiveAndEnabled)
                {
                    this.baldi.Hear(base.transform.position, 1f);
                }
            }
        }
    }

    public void LockDoor(float time)
    {
        this.barrier.enabled = true;
        this.obstacle.SetActive(true);
        this.bDoorLocked = true;
        this.lockTime = time;

        if (DifferentSides)
        {
            this.inside.material = this.inLocked;
            this.outside.material = this.outLocked;
        }
        else
        {
            this.inside.material = this.locked;
            this.outside.material = this.locked;
        }
    }

    private void UnlockDoor()
    {
        this.barrier.enabled = false;
        this.obstacle.SetActive(false);
        this.bDoorLocked = false;
        if (DifferentSides)
        {
            this.inside.material = this.inClosed;
            this.outside.material = this.outClosed;
        }
        else
        {
            this.inside.material = this.closed;
            this.outside.material = this.closed;
        }
    }

    private GameControllerScript gc;

    private BaldiScript baldi;

    public MeshCollider barrier;

    public GameObject obstacle;

    public MeshCollider trigger;

    public MeshRenderer inside;

    public MeshRenderer outside;

    [SerializeField] private bool DifferentSides;

    [Header("Materials In")]
    [ShowIf("DifferentSides")]
    public Material inClosed;

    [ShowIf("DifferentSides")]
    public Material inOpen;

    [ShowIf("DifferentSides")]
    public Material inLocked;

    [Header("Out")]
    [ShowIf("DifferentSides")]
    public Material outClosed;

    [ShowIf("DifferentSides")]
    public Material outOpen;

    [ShowIf("DifferentSides")]
    public Material outLocked;

    [HideIf("DifferentSides")]
    public Material closed;

    [HideIf("DifferentSides")]
    public Material open;

    [HideIf("DifferentSides")]
    public Material locked;

    [Space()]
    public AudioClip doorOpen;

    public AudioClip baldiDoor;

    private float openTime;

    private float lockTime;

    public bool bDoorOpen;

    public bool bDoorLocked;

    private bool requirementMet;

    protected AudioSource myAudio;
}