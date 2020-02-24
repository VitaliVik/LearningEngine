import {combineEpics} from 'redux-observable';
import getUserToken from './getUserTokenEpic';

export const rootEpic = combineEpics(getUserToken);