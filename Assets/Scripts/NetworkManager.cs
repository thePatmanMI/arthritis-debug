using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colyseus;
using System.Threading.Tasks;
using System;

public class NetworkManager : MonoBehaviour {
	public GameManager gameManager;
	private static ColyseusClient _client = null;
	private static ColyseusRoom<ArthritisRoomState> _room = null;

	public void Initialize() {
		_client = new ColyseusClient($"ws://localhost:2567");
	}

	public List<Card> getDiscardPile() {
		List<Card> discardPile = _room.State.discardPile.items;
		return discardPile;
	}

	private async void Start() {
		Initialize();
		await JoinOrCreateGame();
		_room.OnMessage<string>("server-message", (message) => {
			Debug.Log("Server message: " + message);
		});

		_room.OnMessage<string>("game-message", (message) => {
			string messageType = message.Substring(0, message.IndexOf(" "));
			Debug.Log("messageType:'" + messageType + "'");
			Debug.Log("Game message: " + message);
			//if (messageType == "draw") {
			//	Debug.Log("opponent draw");
			//	gameManager.GetComponent<GameManager>().Draw("OpponentCards");
			if (messageType == "drop") {
				Debug.Log("drop");
				gameManager.GetComponent<GameManager>().Drop();
			} else if (messageType == "dealt") {
				Debug.Log("dealt");
			}
		});
	}

	public async Task JoinOrCreateGame() {
		_room = await _client.JoinOrCreate<ArthritisRoomState>("game");
		var callbacks = Colyseus.Schema.Callbacks.Get(_room);

		callbacks.OnAdd(state => state.discardPile, (index, card) => {
			gameManager.GetComponent<GameManager>().DrawDiscard();
		});

		int counter = 0;
		callbacks.OnAdd(state => state.players, (id, player) => {
			callbacks.OnAdd(player, player => player.cards, (index, card) => {
				counter++;
				Debug.Log("onAdd called " + counter + " times.");
				if (player.playerId == _room.SessionId) {
					gameManager.GetComponent<GameManager>().Draw(player.playerId, card);
				}
			});
		});

		// FIXME: find replacement for this 0.15 compliant code
		// _room.State.players.OnAdd((String key, Player player) =>
		// {
		// 	player.cards.OnAdd((int cardKey, Card card) =>
		// 	{
		// 		if (player.playerId == _room.SessionId)
		// 		{
		// 			gameManager.GetComponent<GameManager>().Draw(player.playerId, card);
		// 		}
		// 		else
		// 		{
		// 			Debug.Log("drawing opponent card:" + player.playerId);
		// 			gameManager.GetComponent<GameManager>().DrawOpponentCards(player.playerId);
		// 		}
		// 	});
		// });
	}

	public new void SendMessage(string type) {
		Debug.Log("sending message:" + type);
		_room.Send("game-message", type);
	}
}
