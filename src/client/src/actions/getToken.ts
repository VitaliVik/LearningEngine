import {createAction} from 'redux-action';
export const GET_TOKEN = 'GET_TOKEN';
export const GET_TOKEN_SUCCESS = 'GET_TOKEN_SUCCESS';
export const GET_TOKEN_FAIL = 'GET_TOKEN_FAIL';


export const getToken = createAction("GET_TOKEN", (username: string, password: string) => {
    return {
        username: username,
        password: password
    }
});

export const getTokenFail = createAction((message: string) => ({
    type: GET_TOKEN_FAIL,
    payload: message
}));

export const getTokenSuccess = createAction((res: TokenResponse) => ({
    type: GET_TOKEN_SUCCESS,
    payload: {access_token: res.access_token, username: res.username}
}));

interface TokenResponse {
    access_token: string,
    username: string
}



// export const getToken = (username: string, password: string) => ({
//     type: GET_TOKEN,
//     payload: {username: username, password: password}
// });

// export const getTokenFail = (message: string) => ({
//     type: GET_TOKEN_FAIL,
//     payload: message
// });
// export const getTokenSuccess = (res: TokenResponse) => ({
//     type: GET_TOKEN_SUCCESS,
//     payload: {access_token: res.access_token, username: res.username}
// });