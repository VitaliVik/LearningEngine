import { createAction } from ".";


export const getToken = createAction('GET_TOKEN', (userName:string, password:string) => ({
    userName,
    password
}));
 
export const getTokenFail = createAction('GET_TOKEN_FAIL', (message: string) => message);

export const getTokenSuccess = createAction<GetTokenSuccessPayload, TokenResponse>('GET_TOKEN_SUCCESS', (res) => ({
    accessToken: res.accessToken,
    userName: res.userName
}));

export interface GetTokenSuccessPayload { accessToken: string, userName: string }

interface TokenResponse {
    accessToken: string,
    userName: string
}

