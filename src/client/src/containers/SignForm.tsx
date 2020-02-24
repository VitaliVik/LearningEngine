import React, { SyntheticEvent, FormEvent } from 'react';
import "./SignInForm.css";
import { NavLink } from 'react-router-dom';

interface SignInProps {
   onLogin(login:string, password:string): any;
}

interface SignInState {
    login: string,
    password: string
}

export default class SignInForm extends React.Component<SignInProps, SignInState> {
    constructor(props: SignInProps) {
        super(props);
        this.state = {
            login: "",
            password: ""
        };
        
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
                    <NavLink to="/registration">Регистрация
                    </NavLink>
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
        this.props.onLogin(this.state.login, this.state.password);
    }
}



