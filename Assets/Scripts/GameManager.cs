using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public GameObject PlayerCard;
	public GameObject OpponentCard;
	public GameObject DiscardCard;
	public GameObject OpponentCardBack;
	public GameObject PlayerArea;
	public GameObject OpponentArea;
	public GameObject DropZone;
	public GameObject DiscardPile;

	private List<GameObject> discardPileCards = new List<GameObject>();
	private List<GameObject> opponentCards = new List<GameObject>();
	private List<GameObject> playerCards = new List<GameObject>();
	private List<Image> images = new List<Image>();

	public NetworkManager networkManager;

	public void Clear() {
        foreach (var item in playerCards) {
			Destroy(item);
        }
		foreach (var item in opponentCards)
		{
			Destroy(item);
		}
		foreach (var item in discardPileCards) {
			Destroy(item);
		}
		playerCards.Clear();
		opponentCards.Clear();
		discardPileCards.Clear();
	}

	public void Draw(String playerId, Card card) {
		string cardName = PlayingCard.getCardName(card);
        Image image = PlayerCard.GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>(cardName);
        GameObject gameCard = Instantiate(PlayerCard, new Vector2(0, 0), Quaternion.identity);
        gameCard.transform.SetParent(PlayerArea.transform, false);
		playerCards.Add(gameCard);
    }

	public void DrawDiscard() {
		string topCard = PlayingCard.getCardName(networkManager.getDiscardPile().ElementAt(0));
		Image image = DiscardCard.GetComponent<Image>();
		image.sprite = Resources.Load<Sprite>(topCard);

		GameObject discardCard = Instantiate(DiscardCard, new Vector2(0, 0), Quaternion.identity);
		discardCard.transform.SetParent(DiscardPile.transform, false);
		discardPileCards.Add(discardCard);
	}

	public void DrawOpponentCards(String playerId) {
        GameObject card = Instantiate(OpponentCardBack, new Vector2(0, 0), Quaternion.identity);
        card.transform.SetParent(OpponentArea.transform, false);
        opponentCards.Add(card);
    }

	public void Drop() {
		GameObject card = Instantiate(OpponentCard, new Vector2(0,0), Quaternion.identity);
		card.transform.SetParent(DiscardPile.transform, false);
		if (opponentCards.Count > 0) {
			Destroy(opponentCards[0]);
			opponentCards.RemoveAt(0);
		}
	}
}
