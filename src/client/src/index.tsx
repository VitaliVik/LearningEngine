import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import { BrowserRouter } from 'react-router-dom';
import { createStore, applyMiddleware } from 'redux';
import reducer from "./reducers/index";
import thunk from "redux-thunk";
import {composeWithDevTools} from 'redux-devtools-extension';
import {Provider} from 'react-redux';


export const store = createStore(reducer, composeWithDevTools(applyMiddleware(thunk)));
ReactDOM.render(
(
<Provider store={store}>
    <BrowserRouter>
        <App />
    </BrowserRouter>
</Provider>
), document.getElementById('root'));
