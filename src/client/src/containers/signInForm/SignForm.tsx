import React from 'react';
import "./SignInForm.css";
import { NavLink, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { getToken } from '../../actions/getToken';
import { Field, reduxForm } from 'redux-form';
import { CommonField } from '../../components/CommonField'
import { required as r} from 'redux-form-validators';

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
                <LoginReduxForm onSubmit={this.onSubmitHandle.bind(this)} />
                <NavLink to="/registration">Регистрация
                </NavLink>
                {this.props.accounts.accessToken !== "" && <Redirect to="/account"></Redirect>}
            </div>
        );
    }

    onSubmitHandle(formData: any) {
        const {login, password} = formData;
        this.props.getToken(login, password);
    }
}

const LoginForm = (props: any) => {
    return (
        <form onSubmit={props.handleSubmit} method='POST'>
            <Field
                name='login'
                component={CommonField}
                type='text'
                placeholder='login'
                validate={r({msg: 'Введите логин'})}
            />
            <Field
                name='password'
                component={CommonField}
                type='password'
                placeholder='password'
                validate={r({msg: 'Введите пароль'})}
            />
            <button>Войти</button>
        </form>
    );
}

const LoginReduxForm = reduxForm<{}, {}>({ form: 'signInForm' })(LoginForm);

const mapDispatchToProps = (dispatch:any) => bindActionCreators({getToken}, dispatch)
const mapStateToProps = (state: any) => ({ ...state });

export default connect(mapStateToProps, mapDispatchToProps)(SignInForm);



