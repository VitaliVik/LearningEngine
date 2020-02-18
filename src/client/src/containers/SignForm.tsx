import React, { SyntheticEvent, FormEvent } from 'react';
import { store } from "./index";
import "./SignInForm.css";
import { EventType } from '@testing-library/react';


export default class SignInForm extends React.Component<{}, {login: string, password: string}> {
    constructor(props: any) {
        super(props);
        this.state = {login: "", password: ""};
        
    }

    render() {
        return (
            <div className="container">
                <form onSubmit={this.onSubmitHandle.bind(this)}>
                    <input type="text" id="login"  placeholder="Логин" onChange={this.loginChangeHandle.bind(this)}/>
                    <br />
                    <input type="password" id="password" placeholder="Пароль" onChange={this.passwordChangeHandle.bind(this)}/>
                    <br />
                    <button>Войти</button>
                </form>
            </div>
        );
    }

    passwordChangeHandle(event: FormEvent<HTMLInputElement>) {
        this.setState({login: this.state.login, password: event.currentTarget.value});
    }

    loginChangeHandle(event: FormEvent<HTMLInputElement>) {
        this.setState({login: event.currentTarget.value, password: this.state.password});
    }

    onSubmitHandle(event: SyntheticEvent) {
        event.preventDefault();
        store.dispatch({ 
            type: "USER_SIGNIN", 
            payload: { login: this.state.login, password: this.state.password } 
        });
    }
}



