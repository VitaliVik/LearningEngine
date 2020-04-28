import { RegistrationResponse } from './../actions/regitstration';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/switchMap';
import axios, { AxiosResponse } from 'axios';
import {
    registration,
    registrationFail,
    registrationSuccess,
} from '../actions/regitstration';

const url = process.env.REACT_APP_SERVER_URL + 'api/account/register';

export default function registrationAccountEpic(action$: any) {
    return action$
        .ofType(registration.type)
        .switchMap(async (action:any) => {
            let formData: FormData = new FormData();
            formData.set('password', action.payload.password);
            formData.set('username', action.payload.username);
            formData.set('email', action.payload.email)
            return await axios.post(url, formData);
        })
        .map((res: AxiosResponse<RegistrationResponse>) => { 
            return registrationSuccess(res.data);
        })
        .catch((error:any) => {
            Observable.of(registrationFail(error ?? "Ошибка регистрации"))
        });
}