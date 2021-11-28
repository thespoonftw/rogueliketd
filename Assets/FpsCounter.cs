using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FpsCounter : MonoBehaviour {

	[SerializeField] TextMeshProUGUI text;
	
	private float count;

	IEnumerator Start() {
		GUI.depth = 2;
		while (true) {
			if (Time.timeScale == 1) {
				yield return new WaitForSeconds(0.1f);
				count = (1 / Time.deltaTime);
				text.text = (Mathf.Round(count)).ToString();
			}
			yield return new WaitForSeconds(0.5f);
		}
	}
}
