import { Observable } from 'rxjs';
import { switchMap, catchError } from 'rxjs/operators';
import { map } from 'rxjs/operators';
import 'rxjs/add/observable/of';
import axios from 'axios';

import {
    GET_TOKEN,
    getTokenSuccess,
    getTokenFail
} from "../actions/getToken";

const url = "https://localhost:5001/api/account/token";

export default function getUserTokenEpic(action$: any) {
    return action$
        .ofType(GET_TOKEN)
        .switchMap(async (action:any) => {
            let formData: FormData = new FormData();
            formData.set('password', action.payload.password);
            formData.set('username', action.payload.username);
            let res = await axios.post("https://localhost:5001/api/account/token", formData)
                .then(res => {
            console.log(res.data);
            return res.data;
          })
        })
        .map((res:any) => getTokenSuccess(res.access_token))
        .catch((error:any) => Observable.of(getTokenFail(error)));
}