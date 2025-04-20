import { Schema, ArraySchema, CollectionSchema, MapSchema, type } from "@colyseus/schema";

export enum Objective {
	DISCARD = "discard",
	NO_DISCARD = "no discard",
	SET_OF_3 = "set of 3",
	RUN_OF_4 = "run of 4",
	RUN_OF_7 = "run of 7"
}

export class Round extends Schema {
	@type("string") readonly currentRound: string;
	@type(["string"]) readonly objectives = new ArraySchema<string>();
	@type("number") readonly startingHandSize: number;

	constructor(round = "1") {
		super();
		this.currentRound = round;
		switch (round) {
			case "1":
				this.objectives.push(Objective.SET_OF_3);
				this.objectives.push(Objective.SET_OF_3);
				this.objectives.push(Objective.DISCARD);
				this.startingHandSize = 7;
				break;
			case "2":
				this.objectives.push(Objective.SET_OF_3);
				this.objectives.push(Objective.RUN_OF_4);
				this.objectives.push(Objective.DISCARD);
				this.startingHandSize = 8;
				break;
			case "3":
				this.objectives.push(Objective.RUN_OF_4);
				this.objectives.push(Objective.RUN_OF_4);
				this.objectives.push(Objective.DISCARD);
				this.startingHandSize = 9;
				break;
			case "4":
				this.objectives.push(Objective.SET_OF_3);
				this.objectives.push(Objective.SET_OF_3);
				this.objectives.push(Objective.SET_OF_3);
				this.objectives.push(Objective.DISCARD);
				this.startingHandSize = 10;
				break;
			case "5":
				this.objectives.push(Objective.SET_OF_3);
				this.objectives.push(Objective.SET_OF_3);
				this.objectives.push(Objective.RUN_OF_4);
				this.objectives.push(Objective.DISCARD);
				this.startingHandSize = 11;
				break;
			case "6":
				this.objectives.push(Objective.SET_OF_3);
				this.objectives.push(Objective.RUN_OF_4);
				this.objectives.push(Objective.RUN_OF_4);
				this.objectives.push(Objective.DISCARD);
				this.startingHandSize = 12;
				break;
			case "7":
				this.objectives.push(Objective.RUN_OF_4);
				this.objectives.push(Objective.RUN_OF_4);
				this.objectives.push(Objective.RUN_OF_4);
				this.objectives.push(Objective.DISCARD);
				this.startingHandSize = 13;
				break;
			case "8":
				this.objectives.push(Objective.SET_OF_3);
				this.objectives.push(Objective.SET_OF_3);
				this.objectives.push(Objective.RUN_OF_7);
				this.objectives.push(Objective.NO_DISCARD);
				this.startingHandSize = 13;
				break;
			default:
				break;
		}
	}
}

export enum FaceNumberValue {
	Two = 0,
	Three = 1,
	Four = 2,
	Five = 3,
	Six = 4,
	Seven = 5,
	Eight = 6,
	Nine = 7,
	Ten = 8,
	Jack = 9,
	Queen = 10,
	King = 11,
	Ace = 12
}
export enum Suit {
	Clubs = "Clubs",
	Diamonds = "Diamonds",
	Hearts = "Hearts",
	Spades = "Spades",
	Wild = "Wild"
};

export class Card extends Schema {
	@type("int8") readonly cardId: number;
	@type("string") readonly suit: string;
	@type("int8") readonly faceNumberValue: number = -1;

	constructor(cardId = -1) {
		super();
		this.cardId = cardId;
		if (cardId < 13) {
			this.suit = Suit.Clubs;
			this.faceNumberValue = cardId;
		} else if (cardId < 26) {
			this.suit = Suit.Diamonds;
			this.faceNumberValue = cardId - 13;
		} else if (cardId < 39) {
			this.suit = Suit.Hearts;
			this.faceNumberValue = cardId - 26;
		} else if (cardId < 52) {
			this.suit = Suit.Spades;
			this.faceNumberValue = cardId - 39;
		} else {
			this.suit = Suit.Wild;
		}
	}
}

export class Player extends Schema {
	@type("string") readonly playerId: string;
	@type([Card]) cards = new ArraySchema<Card>();

	constructor(playerId = "") {
		super();
		this.playerId = playerId;
	}
}

export class ArthritisRoomState extends Schema {
	@type({ map: Player }) players = new MapSchema<Player>();
	@type([Card]) deck = new ArraySchema<Card>();
	@type(Round) round = new Round("1");
	@type("string") currentPlayerId: string;
	@type([Card]) discardPile = new ArraySchema<Card>();
	@type(["string"]) playerOrder = new ArraySchema<string>();
}
