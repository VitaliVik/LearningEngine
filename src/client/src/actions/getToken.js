export const GET_TOKEN = 'GET_TOKEN';
export const GET_TOKEN_SUCCESS = 'GET_TOKEN_SUCCESS';
export const GET_TOKEN_FAIL = 'GET_TOKEN_FAIL';

export const getToken = (username, password) => ({
    type: GET_TOKEN,
    payload: {username: username, password: password}
});

export const getTokenFail = (message) => ({
    type: GET_TOKEN_FAIL,
    payload: message
});

export const getTokenSuccess = (res) => ({
    type: GET_TOKEN_SUCCESS,
    payload: {access_token: res.access_token, username: res.username}
});