import {combineEpics} from 'redux-observable';
import getTokenEpic from './getUserTokenEpic';

import 'rxjs';
import registrationAccountEpic from './registrationAccountEpic';

export const rootEpic = combineEpics(getTokenEpic, registrationAccountEpic);