export enum LaidDownResponses {
	NotValid,
	ValidRunEnd,
	ValidRunFront,
	ValidSet
}

export enum ObjectiveOrder {
	RunOne = 0,
	RunTwo = 1,
	RunThree = 2,
	SetOne = 3,
	SetTwo = 4,
	SetThree = 5
}

export class LaidDownResponse {
	private response: LaidDownResponses;

	public constructor(response: LaidDownResponses) {
		this.response = response;
	}

	public isValid(): boolean {
		return this.response == LaidDownResponses.ValidRunEnd
			|| this.response == LaidDownResponses.ValidRunFront
			|| this.response == LaidDownResponses.ValidSet;
	}
}