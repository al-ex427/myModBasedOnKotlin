﻿using KOTLIN.Interactions;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using KOTLIN.Subtitles;
using Pixelplacement;
using KOTLIN.Translation;
using UnityEngine.Events;
using System.Collections.Generic;
using KOTLIN.Items;
using FluidMidi;
public class GameControllerScript : Singleton<GameControllerScript>
{

    [SerializeField] private ItemManager itemManager;
    public int MaxNotebooks;

    [Space()]
    public PlayerScript player;

    public Transform playerTransform;

    public Transform cameraTransform;

    public new Camera camera;

    private int cullingMask;

    public GameObject baldiTutor;

    public GameObject baldi;

    public BaldiScript baldiScrpt;

    public AudioClip aud_Prize;

    public AudioClip aud_PrizeMobile;

    public AudioClip aud_AllNotebooks;

    public GameObject principal;

    public GameObject crafters;

    public GameObject playtime;

    public PlaytimeScript playtimeScript;

    public GameObject gottaSweep;

    public GameObject bully;

    public GameObject firstPrize;

    public GameObject TestEnemy;

    public FirstPrizeScript firstPrizeScript;

    public GameObject quarter;

    public AudioSource tutorBaldi;

    public RectTransform boots;

    public string mode;

    public int notebooks;

    public GameObject[] notebookPickups;

    public int failedNotebooks;

    public bool spoopMode, finaleMode;

    public bool debugMode;

    public bool mouseLocked;

    public int exitsReached;

    public int itemSelected;

    public GameObject bsodaSpray;

    public GameObject alarmClock;

    public GameObject pauseMenu;

    public GameObject highScoreText;

    [HideInInspector] public bool gamePaused;

    [HideInInspector] public bool learningActive;

    private float gameOverDelay;

    [HideInInspector] public AudioSource audioDevice;

    public AudioClip aud_Soda;

    public AudioClip aud_Spray;

    public AudioClip aud_buzz;

    public AudioClip aud_Hang;

    [SerializeField] private AudioClip aud_MachineQuiet;
    [SerializeField] private AudioClip aud_MachineStart;
    [SerializeField] private AudioClip aud_MachineRev;
	[SerializeField] private AudioClip aud_MachineLoop;

    public AudioClip aud_Switch;

    public SongPlayer schoolMusic;

	public SongPlayer endMusic;

  //  public SongPlayer learnMusic;

	[Header("Extra")]

	[SerializeField] private Cubemap Sky;

    [SerializeField] private Color SkyCol;

    //private Player playerInput;
    private List<EntranceScript> entrances = new List<EntranceScript>(); //
    [Obsolete]
	private void Start()
	{
        Shader.SetGlobalTexture("_Skybox", Sky);
        Shader.SetGlobalColor("_SkyboxColor", SkyCol);
        this.cullingMask = this.camera.cullingMask; // Changes cullingMask in the Camera
		this.audioDevice = base.GetComponent<AudioSource>(); //Get the Audio Source
		this.mode = PlayerPrefs.GetString("CurrentMode"); //Get the current mode
		if (this.mode == "endless") //If it is endless mode
		{
			this.baldiScrpt.endless = true; //Set Baldi use his slightly changed endless anger system
		}
		this.schoolMusic.Play(); //Play the school music
		this.LockMouse(); //Prevent the mouse from moving
                          //Update the notebook count
            Singleton<UIManager>.Instance.UpdateNotebookCount(notebooks, MaxNotebooks);
        this.itemSelected = 0; //Set selection to item slot 0(the first item slot)
		this.gameOverDelay = 0.5f;

		foreach (EntranceScript entrance in FindObjectsOfTypeAll(typeof(EntranceScript))) //typeall for 2019 support (ew)
		{
			entrances.Add(entrance);
		}
    }

	private void Update()
	{
		if (!this.learningActive)
		{
			if (Input.GetButtonDown("Pause"))
			{
				if (!this.gamePaused)
				{
					this.PauseGame();
				}
				else
				{
					this.UnpauseGame();
				}
			}
			if (Input.GetKeyDown(KeyCode.Y) & this.gamePaused)
			{
				this.ExitGame();
			}
			else if (Input.GetKeyDown(KeyCode.N) & this.gamePaused)
			{
				this.UnpauseGame();
			}
			if (!this.gamePaused & Time.timeScale != 1f)
			{
				Time.timeScale = 1f;
			}

			if (Time.timeScale != 0f)
			{
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))  //remember to make an input manager
                {
                    Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
                    RaycastHit raycastHit;
                    if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity)) //infinity because interatc distance
                    {
                        Interactable interaciton = raycastHit.collider.gameObject.GetComponent<Interactable>();
						if (interaciton != null && interaciton.isActiveAndEnabled && Vector3.Distance(playerTransform.position, interaciton.transform.position) < interaciton.InteractDistance)
							interaciton.Interact();
                    }
                }

			}
		}
		else
		{
			if (Time.timeScale != 0f)
			{
				Time.timeScale = 0f;
			}
		}
		if (this.player.gameOver)
		{
			if (this.mode == "endless" && this.notebooks > PlayerPrefs.GetInt("HighBooks") && !this.highScoreText.activeSelf)
			{
				this.highScoreText.SetActive(true);
			}
			Time.timeScale = 0f;
			this.gameOverDelay -= Time.unscaledDeltaTime * 0.5f;
			this.camera.farClipPlane = this.gameOverDelay * 400f; //Set camera farClip 
			this.audioDevice.PlayOneShot(this.aud_buzz);
			if (PlayerPrefs.GetInt("Rumble") == 1)
			{

			}
			if (this.gameOverDelay <= 0f)
			{
				if (this.mode == "endless")
				{
					if (this.notebooks > PlayerPrefs.GetInt("HighBooks"))
					{
						PlayerPrefs.SetInt("HighBooks", this.notebooks);
					}
					PlayerPrefs.SetInt("CurrentBooks", this.notebooks);
				}
				Time.timeScale = 1f;
				SceneManager.LoadScene("MainMenu");
			}
		}
		if (this.finaleMode && !this.audioDevice.isPlaying && this.exitsReached == 3)
		{
			this.audioDevice.clip = this.aud_MachineLoop;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}

        if (this.notebooks == MaxNotebooks & this.mode == "story")
        {
            this.ActivateFinaleMode();
        }
    }

	

	public void CollectNotebook(int notes)
	{
		this.notebooks += notes;
		Singleton<UIManager>.Instance.UpdateNotebookCount(notebooks, Mathf.Max(MaxNotebooks, notebooks));

    }

	public void LockMouse()
	{
		if (!this.learningActive)
		{
			Singleton<CursorControllerScript>.Instance.LockCursor(); //Prevent the cursor from moving
			this.mouseLocked = true;
		}
	}

	public void UnlockMouse()
	{
        Singleton<CursorControllerScript>.Instance.UnlockCursor(); //Allow the cursor to move
		this.mouseLocked = false;
	}

	public void PauseGame()
	{
		if (!this.learningActive)
		{
			{
				this.UnlockMouse();
			}
			SongPlayer.PauseAll();
			Time.timeScale = 0f;
			this.gamePaused = true;
			this.pauseMenu.SetActive(true);
			
		}
	}
	public void ExitGame()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void UnpauseGame()
	{
		Time.timeScale = 1f;
		this.gamePaused = false;
        SongPlayer.ResumeAll();
        this.pauseMenu.SetActive(false);
		this.LockMouse();
	}

	public void ActivateSpoopMode()
	{
		this.spoopMode = true; //Tells the game its time for spooky
        foreach (EntranceScript entrance in entrances)
        {
			entrance.Lower(this); 
        }
        this.baldiTutor.SetActive(false); //Turns off Baldi(The one that you see at the start of the game)
		this.baldi.SetActive(true); //Turns on Baldi
        this.principal.SetActive(true); //Turns on Principal
        this.crafters.SetActive(true); //Turns on Crafters
        this.playtime.SetActive(true); //Turns on Playtime
        this.gottaSweep.SetActive(true); //Turns on Gotta Sweep
        this.bully.SetActive(true); //Turns on Bully
        this.firstPrize.SetActive(true); //Turns on First-Prize
		//this.TestEnemy.SetActive(true); //Turns on Test-Enemy
		this.audioDevice.PlayOneShot(this.aud_Hang); //Plays the hang sound
		//this.learnMusic.Stop(); //Stop all the music
		this.schoolMusic.Stop();
	}

	private void ActivateFinaleMode()
	{
		this.finaleMode = true;
        foreach (EntranceScript entrance in entrances)
        {
            entrance.Raise();
        }
    }

	public void GetAngry(float value) //Make Baldi get angry
	{
		if (!this.spoopMode)
		{
			this.ActivateSpoopMode();
		}
		this.baldiScrpt.GetAngry(value);
	}

	public void ActivateLearningGame()
	{
		//this.camera.cullingMask = 0; //Sets the cullingMask to nothing
		this.learningActive = true;
		this.UnlockMouse(); //Unlock the mouse
		this.tutorBaldi.Stop(); //Make tutor Baldi stop talking
		if (!this.spoopMode) //If the player hasn't gotten a question wrong
		{
			this.schoolMusic.Stop(); //Start playing the learn music
			//this.learnMusic.Play();
		}
	}

	public void DeactivateLearningGame(GameObject subject)
	{
		this.camera.cullingMask = this.cullingMask; //Sets the cullingMask to Everything
		this.learningActive = false;
		UnityEngine.Object.Destroy(subject);
		this.LockMouse(); //Prevent the mouse from moving
		if (this.player.stamina < 100f) //Reset Stamina
		{
			this.player.stamina = 100f;
		}
		if (!this.spoopMode) //If it isn't spoop mode, play the school music
		{
			this.schoolMusic.Play();
			//this.learnMusic.Stop();
		}
		if (this.notebooks == 1 & !this.spoopMode) // If this is the players first notebook and they didn't get any questions wrong, reward them with a quarter
		{
			this.quarter.SetActive(true);
			this.tutorBaldi.PlayOneShot(this.aud_Prize);
		}
		else if (this.notebooks == MaxNotebooks & this.mode == "story") // Plays the all 7 notebook sound
		{
			this.audioDevice.PlayOneShot(this.aud_AllNotebooks, 0.8f);
			this.endMusic.Play();
		}
	}

	public IEnumerator BootAnimation()
	{
		float time = 15f;
		float height = 375f;
		Vector3 position = default;
		this.boots.gameObject.SetActive(true);
		while (height > -375f)
		{
			height -= 375f * Time.deltaTime;
			time -= Time.deltaTime;
			position = this.boots.localPosition;
			position.y = height;
			this.boots.localPosition = position;
			yield return null;
		}
		position = this.boots.localPosition;
		position.y = -375f;
		this.boots.localPosition = position;
		this.boots.gameObject.SetActive(false);
		while (time > 0f)
		{
			time -= Time.deltaTime;
			yield return null;
		}
		this.boots.gameObject.SetActive(true);
		while (height < 375f)
		{
			height += 375f * Time.deltaTime;
			position = this.boots.localPosition;
			position.y = height;
			this.boots.localPosition = position;
			yield return null;
		}
		position = this.boots.localPosition;
		position.y = 375f;
		this.boots.localPosition = position;
		this.boots.gameObject.SetActive(false);
		yield break;
	}


	public void ExitReached()
	{
		this.exitsReached++;
		if (this.exitsReached == 1)
		{
			RenderSettings.ambientLight = Color.red; //Make everything red and start player the weird sound
			//RenderSettings.fog = true;
			this.audioDevice.PlayOneShot(this.aud_Switch, 0.8f);
			endMusic.Tempo = 0.1f;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}
		if (this.exitsReached == 2) //Play a sound
		{
			this.audioDevice.volume = 0.8f;
			this.audioDevice.clip = this.aud_MachineStart;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}
		if (this.exitsReached == 3) //Play a even louder sound
		{
			this.audioDevice.clip = this.aud_MachineRev;
			this.audioDevice.loop = false;
			this.audioDevice.Play();
		}
	}

	public void DespawnCrafters()
	{
		this.crafters.SetActive(false); //Make Arts And Crafters Inactive
	}

	public void Fliparoo()
	{
		this.player.height = 6f;
		this.player.fliparoo = 180f;
		this.player.flipaturn = -1f;
		Camera.main.GetComponent<CameraScript>().offset = new Vector3(0f, -1f, 0f);
	}

}
