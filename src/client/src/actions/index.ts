import { Action, ActionFunction4, ActionFunction3, ActionFunction2, ActionFunction1, ActionFunction0 } from "redux-actions";
import { createAction as reduxCreateAction } from "redux-actions";

interface ActionType { type: string; }

export interface ActionFunc0<R> extends ActionType { (): R; };
export interface ActionFunc1<T1, R> extends ActionType { (t1: T1): R; };
export interface ActionFunc2<T1, T2, R> extends ActionType { (t1: T1, t2: T2): R; };
export interface ActionFunc3<T1, T2, T3, R> extends ActionType { (t1: T1, t2: T2, t3: T3): R; };
export interface ActionFunc4<T1, T2, T3, T4, R> extends ActionType { (t1: T1, t2: T2, t3: T3, t4: T4): R; };
export interface ActionFuncAny<R> extends ActionType { (...args: any[]): R; };

export function createAction(actionType: string): ActionFuncAny<Action<any>>;

export function createAction<Payload>(
    actionType: string
): ActionFunc1<Payload, Action<Payload>>;

export function createAction<Payload>(
    actionType: string,
    payloadCreator: ActionFunction0<Payload>
): ActionFunc0<Action<Payload>>;

export function createAction<Payload, Arg1>(
    actionType: string,
    payloadCreator: ActionFunction1<Arg1, Payload>
): ActionFunc1<Arg1, Action<Payload>>;

export function createAction<Payload, Arg1, Arg2>(
    actionType: string,
    payloadCreator: ActionFunction2<Arg1, Arg2, Payload>
): ActionFunc2<Arg1, Arg2, Action<Payload>>;

export function createAction<Payload, Arg1, Arg2, Arg3>(
    actionType: string,
    payloadCreator: ActionFunction3<Arg1, Arg2, Arg3, Payload>
): ActionFunc3<Arg1, Arg2, Arg3, Action<Payload>>;

export function createAction<Payload, Arg1, Arg2, Arg3, Arg4>(
    actionType: string,
    payloadCreator?: ActionFunction4<Arg1, Arg2, Arg3, Arg4, Payload>
): ActionFunc4<Arg1, Arg2, Arg3, Arg4, Action<Payload>> {
    return Object.assign(
        payloadCreator ? reduxCreateAction(actionType, payloadCreator) : reduxCreateAction(actionType),
        { type: actionType }
    );
}