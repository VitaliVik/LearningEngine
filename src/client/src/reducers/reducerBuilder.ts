import { ReducerMap, Action, ReducerMapValue } from "redux-actions";
import { ActionFuncAny } from "../actions";

export class ReducerBuilder<TState> {
    reducers: ReducerMap<TState, any> = {};

    handle<TPayload>(action: ActionFuncAny<Action<TPayload>>, map: ReducerMapValue<TState, TPayload>): this {
        this.reducers[action.type] = map;
        return this;
    }

    build(): ReducerMap<TState, any> {
        return this.reducers;
    }
}