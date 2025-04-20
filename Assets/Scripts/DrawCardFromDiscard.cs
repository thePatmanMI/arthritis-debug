using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardFromDiscard : MonoBehaviour {
	public GameObject GameManager;
	public NetworkManager NetworkManager;

	public void OnClick() {
		NetworkManager.GetComponent<NetworkManager>().SendMessage("drawFromDiscard");
	}
}
