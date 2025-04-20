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

public partial class Round : Schema {
#if UNITY_5_3_OR_NEWER
[Preserve]
#endif
public Round() { }
	[Type(0, "string")]
	public string currentRound = default(string);

	[Type(1, "array", typeof(ArraySchema<string>), "string")]
	public ArraySchema<string> objectives = null;

	[Type(2, "number")]
	public float startingHandSize = default(float);
}

