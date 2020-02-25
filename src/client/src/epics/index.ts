import {combineEpics} from 'redux-observable';
import getTokenEpic from './getUserTokenEpic';
import 'rxjs';

export const rootEpic = combineEpics(getTokenEpic);