declare module server {
	interface StartClickToCall extends BaseModel {
		pinCode: string;
		callSid: string;
		consultationID: number;
		medicalPractionerRID: number;
		medicalPractionerPhoneNumber: string;
	}
	interface BaseModel {
		corporationRID: number;
		message: string;
		isError: boolean;
	}
	interface CallOutcome extends BaseModel {
		consultationID: number;
		medicalPractionerRID: number;
		outcome: any;
	}
	interface ClickToCallLight {
		phoneNumber: string;
	}
	interface CallInstance extends BaseModel {
		callSid: string;
		pinCode: string;
		consultationID: number;
		callStatus: any;
	}
}
