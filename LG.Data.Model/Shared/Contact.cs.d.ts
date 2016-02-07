declare module server {
	interface Contact extends BaseModel {
		rID: number;
		eventAction: any;
		isActive: boolean;
		personInfo: {
			fName: string;
			mName: string;
			lName: string;
			dob: Date;
			gender: number;
		};
		contactForRID: number;
		phones: any[];
		emailAddresses: any[];
		newPhones: any[];
		newEmailAddresses: any[];
		contactUsages: any[];
	}
	interface BaseModel {
		corporationRID: number;
		message: string;
		isError: boolean;
	}
}
