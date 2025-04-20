using System.Text;
using UnityEngine;

public class PlayingCard : MonoBehaviour {

	private int cardId;
	private GameObject card;
	private int faceNumberValue;
	private string suit;

	public PlayingCard(int i)
	{
		cardId = i;
		if (cardId < 13) {
			suit = Suit.Clubs;
			faceNumberValue = determineFaceNumberValue(cardId);
        } else if (cardId < 26) {
			suit = Suit.Diamonds;
			faceNumberValue = determineFaceNumberValue(cardId - 13);
		} else if (cardId < 39) {
			suit = Suit.Hearts;
            faceNumberValue = determineFaceNumberValue(cardId - 26);
        } else if (cardId < 52) {
			suit = Suit.Spades;
            faceNumberValue = determineFaceNumberValue(cardId - 39);
        } else {
			suit = Suit.Wild;
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public static string getCardName(Card card)
	{
		StringBuilder sb = new StringBuilder("card");
		switch (card.suit)
		{
			case Suit.Clubs:
				sb.Append("Clubs");
                break;
            case Suit.Diamonds:
                sb.Append("Diamonds");
                break;
            case Suit.Hearts:
                sb.Append("Hearts");
                break;
            case Suit.Spades:
                sb.Append("Spades");
                break;
            case Suit.Wild:
                return "cardJoker";
        }

		switch (card.faceNumberValue)
		{
            case FaceNumberValue.Ace:
                sb.Append("A");
                break;
            case FaceNumberValue.Two:
                sb.Append("2");
                break;
            case FaceNumberValue.Three:
                sb.Append("3");
                break;
			case FaceNumberValue.Four:
                sb.Append("4");
                break;
            case FaceNumberValue.Five:
                sb.Append("5");
                break;
            case FaceNumberValue.Six:
                sb.Append("6");
                break;
            case FaceNumberValue.Seven:
                sb.Append("7");
                break;
            case FaceNumberValue.Eight:
                sb.Append("8");
                break;
            case FaceNumberValue.Nine:
                sb.Append("9");
                break;
            case FaceNumberValue.Ten:
                sb.Append("10");
                break;
            case FaceNumberValue.Jack:
                sb.Append("J");
                break;
            case FaceNumberValue.Queen:
                sb.Append("Q");
                break;
            case FaceNumberValue.King:
                sb.Append("K");
                break;
        }
		return sb.ToString();
	}

	int determineFaceNumberValue(int number)
	{
		switch (number)
		{
			case 0:
				return FaceNumberValue.Ace;
			case 1:
				return FaceNumberValue.Two;
			case 2:
				return FaceNumberValue.Three;
			case 3:
				return FaceNumberValue.Four;
			case 4:
				return FaceNumberValue.Five;
			case 5:
				return FaceNumberValue.Six;
			case 6:
				return FaceNumberValue.Seven;
			case 7:
				return FaceNumberValue.Eight;
			case 8:
				return FaceNumberValue.Nine;
			case 9:
				return FaceNumberValue.Ten;
			case 10:
				return FaceNumberValue.Jack;
			case 11:
				return FaceNumberValue.Queen;
			default:
				return FaceNumberValue.King;
		}
	}
}
