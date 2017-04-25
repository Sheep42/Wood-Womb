using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	[HideInInspector]
	public static int destructionPoints = 0;

	[HideInInspector]
	public static bool won = false;

	[HideInInspector]
	public static bool catRun = false;

	//Dialogue
	public Text dialogue, destructionMeter;
	public string[] startDialogue, endDialogue;

	public Light roomLight;

	private float 	dialogueTimer = 5f, 
					destPercent = 0f, 
					calcPercent = 0f;

	private int dialogueIndex = 0;
	private bool dialogueEnd = false, 
				 triggerDialogue = false,
				 thirtyPercent = false;

	// Update is called once per frame
	void Update () {
		if(!won) {
			if(dialogueIndex < startDialogue.Length) {
				dialogueTimer += Time.deltaTime;

				if(dialogueTimer >= 5f) {
					dialogue.text = startDialogue[dialogueIndex];

					dialogueIndex++;
					dialogueTimer = 0;
				}
			} else if(!dialogueEnd) {
				dialogueTimer += Time.deltaTime;

				if(dialogueTimer >= 5f) {
					dialogue.text = "";

					dialogueEnd = true;
					dialogueTimer = 0f;
				}
			} else if(triggerDialogue && !dialogue.text.Equals("")) {
				dialogueTimer += Time.deltaTime;

				if(dialogueTimer >= 5f) {
					dialogue.text = "";

					dialogueTimer = 0f;
					triggerDialogue = false;
				}
			}
		}

		if(won) {
			if(dialogueIndex < endDialogue.Length) {
				dialogueTimer += Time.deltaTime;

				if(dialogueTimer >= 5f) {
					dialogue.text = endDialogue[dialogueIndex];

					dialogueIndex++;
					dialogueTimer = 0;
				}
			} else if(!dialogueEnd) {
				dialogueTimer += Time.deltaTime;

				if(dialogueTimer >= 5f) {
					dialogue.text = "";

					dialogueEnd = true;
					dialogueTimer = 0f;

					SoundManager.instance.musicSource.Stop();
					SceneManager.LoadScene("Credits");
					SoundManager.instance.musicSource2.Play();
				}
			}
		}

		destPercent = destructionPoints / 1000f;
		calcPercent = destPercent * 100f;


		if(calcPercent >= 100f) {
			destructionMeter.text = "Destruction: 100%";

			if(!won) {
				won = true;
				roomLight.enabled = true;
				dialogueIndex = 0;
				dialogueTimer = 5f;
				dialogueEnd = false;
			}
		} else {
			destructionMeter.text = "Destruction: " + calcPercent + "%";
		}

		if(!thirtyPercent && calcPercent >= 30f) {
			triggerDialogue = true;
			thirtyPercent = true;
			dialogue.text = "Schrodinger: Hey! Knock it off!";
		}
	}
}
