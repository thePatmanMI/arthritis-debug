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

public partial class Card : Schema {
#if UNITY_5_3_OR_NEWER
[Preserve]
#endif
public Card() { }
	[Type(0, "int8")]
	public sbyte cardId = default(sbyte);

	[Type(1, "string")]
	public string suit = default(string);

	[Type(2, "int8")]
	public sbyte faceNumberValue = default(sbyte);
}

