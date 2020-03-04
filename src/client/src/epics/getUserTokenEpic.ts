// import 'rxjs/add/observable/of';
// import 'rxjs/add/operator/switchMap';
// import { Observable } from 'rxjs';
// import { catchError } from 'rxjs/operators';
// import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/switchMap';
import axios from 'axios';

import {
    getToken,
    getTokenSuccess,
    getTokenFail
} from "../actions/getToken";

const url = "https://localhost:5000/api/account/token";

export default function getTokenEpic(action$: any) {
    action$.subscribe((act:any)=>console.log(act));
    return action$
        .ofType(getToken.type)
        .switchMap(async (action:any) => {
            let formData: FormData = new FormData();
            formData.set('password', action.payload.password);
            formData.set('username', action.payload.username);
            let res = await axios.post(url, formData);
            return res.data;
                
        })
        .map((res:any) => { 
            return getTokenSuccess(res)
        })
        .catch((error:any) => Observable.of(getTokenFail(error)));
}