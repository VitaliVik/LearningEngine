import React, { SyntheticEvent, FormEvent } from 'react';
import "./SignInForm.css";
import { NavLink, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import { dispatch } from 'rxjs/internal/observable/pairs';
import { bindActionCreators } from 'redux';
import { getToken } from '../../actions/getToken';

interface SignInState {
    login: string,
    password: string
}

class SignInForm extends React.Component<any, SignInState> {
    constructor(props: any) {
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
                    {this.props.accounts.accessToken != "" && <Redirect to="/account"></Redirect>}
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
        this.props.getToken(this.state.login, this.state.password);
    }
}

const mapDispatchToProps = (dispatch:any) => bindActionCreators({getToken}, dispatch)
const mapStateToProps = (state: any) => ({ ...state });

export default connect(mapStateToProps, mapDispatchToProps)(SignInForm);



