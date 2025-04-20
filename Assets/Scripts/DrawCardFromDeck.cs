using System;
using UnityEngine;

public class DrawCard : MonoBehaviour {
	public GameObject GameManager;
	public NetworkManager NetworkManager;

	public Boolean dealCards = true;

	public void OnClick() {
		if (dealCards) {
			GameManager.GetComponent<GameManager>().Clear();
			NetworkManager.GetComponent<NetworkManager>().SendMessage("deal");
			dealCards = false;
		} else {
			NetworkManager.GetComponent<NetworkManager>().SendMessage("drawFromDeck");
		}
	}
}
