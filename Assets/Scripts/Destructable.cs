using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {
	//public int hitPoints = 100;
	public int selfDmg = 5;
	public int otherDmg = 5;
	public int destroyPts = 10;
	public AudioClip[] hitSounds;
	
	// Update is called once per frame
	void Update () {
		/*if(hitPoints <= 0) {
			GameManager.destructionPoints += destroyPts;
			hitPoints = 100;
			Destroy(gameObject);
		}*/
	}

	public void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.layer == 13) {
			SoundManager.instance.randomizeSfx(hitSounds, 0.3f);
			GameManager.destructionPoints += otherDmg;
		} else if(coll.gameObject.name == "Floor") {
			SoundManager.instance.randomizeSfx(hitSounds, 0.3f);
		}
	}
}
