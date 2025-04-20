// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 3.0.13
// 

using Colyseus.Schema;
#if UNITY_5_3_OR_NEWER
using UnityEngine.Scripting;
#endif

public partial class ArthritisRoomState : Schema {
#if UNITY_5_3_OR_NEWER
[Preserve]
#endif
public ArthritisRoomState() { }
	[Type(0, "map", typeof(MapSchema<Player>))]
	public MapSchema<Player> players = null;

	[Type(1, "array", typeof(ArraySchema<Card>))]
	public ArraySchema<Card> deck = null;

	[Type(2, "ref", typeof(Round))]
	public Round round = null;

	[Type(3, "string")]
	public string currentPlayerId = default(string);

	[Type(4, "array", typeof(ArraySchema<Card>))]
	public ArraySchema<Card> discardPile = null;

	[Type(5, "array", typeof(ArraySchema<string>), "string")]
	public ArraySchema<string> playerOrder = null;
}

