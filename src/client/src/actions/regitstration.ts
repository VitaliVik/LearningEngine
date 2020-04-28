import { createAction } from ".";

export const registration = createAction<RegistrationPayload>("ACCOUNT_REGISTRATION");

export const registrationFail = createAction<any>("ACCOUNT_REGISTRATION_FAIL");

export const registrationSuccess = createAction("ACCOUNT_REGISTRATION_SUCCESS");

export interface RegistrationPayload {
    username: string,
    email: string,
    password: string
}

export interface RegistrationResponse {
    accessToken: string,
    username: string
}