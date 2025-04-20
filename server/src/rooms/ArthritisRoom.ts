import { Room, Client } from "colyseus";
import { Card, Player, ArthritisRoomState } from "./schema/ArthritisRoomState";

export class ArthritisRoom extends Room<ArthritisRoomState> {

	//support only 4 clients connected

	onCreate (options: any) {
		console.log("Arthritis Room being created", options);
		this.state = new ArthritisRoomState();
		this.maxClients = 4;
	}

	onJoin (client: Client, options: any) {
		console.log(client.sessionId, "joined!");

		this.state.players.set(client.sessionId, new Player(client.sessionId));
		this.state.playerOrder.push(client.sessionId);

		//when a message is received of type "message," broadcast it with the type "server-message" to all clients
		this.onMessage("message", (client, message) => {
			if (message == "draw") {
				const player = this.state.players.get(client.sessionId);
				console.log("draw cards clicked, dealing out cards");
				var cards = "";
				for (let i = 0; i < 7; i++) {
					const card = new Card(Math.round(Math.random() * 53));
					player.cards.push(card);
					cards += card.cardId + ",";
				}
			}
			console.log(cards);
			console.log("Game Room received message from", client.sessionId, ":", message);
			this.broadcast("server-message", `(${client.sessionId} ${message}`);
		});
		
		//when a message is received of type "game-message," broadcast it with the type "game-message" to all clients except for the one that sent it
		this.onMessage("game-message", (client, message) => {
			console.log("game-message received from", client.sessionId, ":", message);
			if (message == "deal") {
				this.state.deck.clear();
				this.state.players.forEach(function (player) {
					player.cards.clear();
				});
				this.addDeck(2);
				this.shuffleDeck();
				this.dealCards();
			} else if (message == "drawFromDeck") {
				let card = this.state.deck.shift();
				this.state.players.get(client.sessionId).cards.push(card);
				// this.state.discardPile.push(card);
				this.state.discardPile.splice(0, 0, card);
			} else if (message == "drawFromDiscard") {
				// FIXME: add out of turn logic here for the card swiping
				let card = this.state.discardPile.shift();
				this.state.players.get(client.sessionId).cards.push(card);
			} else if (message == "drop") {
				this.broadcast("game-message", message + " ", { except: client });
			} else if (message == "dropOnDiscard") {
				// FIXME: add logic here to add to the discard pile, maybe it just gets automatically added by sync and we just update the player here?
				
			}
		});

		this.autoDispose = true;
	}

	onLeave (client: Client, consented: boolean) {
		console.log(client.sessionId, "left!");
		this.state.players.delete(client.sessionId);
		let index = this.state.playerOrder.indexOf(client.sessionId);
		this.state.playerOrder.splice(index);
	}

	onDispose() {
		console.log("room", this.roomId, "disposing...");
	}

	addDeck(count: number = 1) {
		for (let i = 0; i < count; i++) {
			for (let j = 0; j < 54; j++) {
				const card = new Card(j);
				this.state.deck.push(card);
			}
		}
	}

	dealCards() {
		for (let i = 0; i < this.state.playerOrder.length; i++) {
			console.log("dealing cards to " + this.state.playerOrder[i]);
			for (let j = 0; j < this.state.round.startingHandSize; j++) {
				let card = this.state.deck.shift();
				this.state.players.get(this.state.playerOrder[i]).cards.push(card);
			}
		}
		this.state.discardPile.push(this.state.deck.shift());
	}

	shuffleDeck(count: number = 1000) {
		for (let i = 0; i < count; i++) {
			let index = Math.round(Math.random() * (this.state.deck.length - 1));
			let card = this.state.deck.at(index);
			this.state.deck.splice(index, 1);
			this.state.deck.push(card);
		}
		console.log("shuffle complete");
	}
}
