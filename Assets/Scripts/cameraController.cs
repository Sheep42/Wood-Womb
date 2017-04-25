using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {
	private float shakeTime = 0f;
	private float shakeAmt = 0.7f;
	private float shakeDamp = 0.1f;

	private Vector3 initPos;

	void Awake() {
		initPos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if(shakeTime > 0) {
			Vector2 shakePos = Random.insideUnitCircle * shakeDamp;

			transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);

			shakeTime -= Time.deltaTime;
		} else {
			shakeTime = 0f;
			transform.position = initPos;
		}
	}

	public void shakeCam(float intensity, float duration, float dampener) {
		shakeAmt = intensity;
		shakeTime = duration;
		shakeDamp = dampener;
	}
}
