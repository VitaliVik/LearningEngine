import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { registration } from '../../actions/regitstration'
import { Link, Redirect } from 'react-router-dom';
import { store } from '../..';
import { accounts } from '../../reducers/accounts';
import { reduxForm, Field, FormDecorator, Fields } from 'redux-form';
import { maxLengthCreator, minLengthCreator, emailValidation } from '../../utils/validators/validators';
import { required } from '../../utils/validators/validators';
import { required as r, email, length, confirmation } from 'redux-form-validators';
import './style.css';
import { StateObservable } from 'redux-observable';

interface RegistrationProps {
    error: string,
    isLoading: string,
    accessToken: string,
    registration: any
}

interface RegistrationData {
    email: string,
    userName: string,
    password: string
}

const minLength6 = minLengthCreator(6);
const maxLength16 = maxLengthCreator(16);

class RegistrationForm extends React.Component<any, any, any> {
    render() {
        const { error, isLoading, accessToken } = this.props.accounts;
        if (accessToken != '') {
            return (<Redirect to="account" />)
        }
        if (error != null) {
            return (
                <div>
                    <p>{error}</p>
                    <Link to="/signIn"></Link>
                </div>
            );
        }
        else {
            return (
                <ReduxForm onSubmit={this.handleFormSubmit.bind(this)}></ReduxForm>
            );
        }
    }

    handleFormSubmit(formData: any) {
        const { userName, email, password } = formData;
        this.props.registration({
            userName,
            email,
            password
        })
    }
}

const renderField = (props: any) => {
    const { meta, label, type, input, placeholder } = props;
    const errors = meta.touched && meta.error;
    return (
        <div>
            <label>{label}</label>
            <div className={errors ? 'errorField' : ''}>
                <input {...input} type={type} placeholder={placeholder}></input>
                {errors && <span className={'errorText'}>{meta.error}</span>}
            </div>
        </div>
    )
}

const Form = (props: any) => {
    return (
        <form onSubmit={props.handleSubmit} method='POST'>
            <Field
                name='userName'
                component={renderField}
                type='text'
                placeholder='login'
                validate={r()}
            />
            <Field
                name='email'
                component={renderField}
                type='email'
                placeholder='email'
                validate={[r(), email()]}
            />
            <Field
                name='password'
                component={renderField}
                type='password'
                placeholder='password'
                validate={[r(), length({in: [6, 16], msg: 'Длина пароля должна быть от 6 до 16 символов!'}), maxLength16]}
            />
            <Field
                name='passwordConfirmation'
                component={renderField}
                type='password'
                placeholder='confirm your password'
                validate={[r(), confirmation({field: 'password', msg: 'Пароли не совпадают!'}), maxLength16]}
            />
            <button>Регестрация</button>
        </form>
    )
}

const ReduxForm = reduxForm<{}, {}>({ form: 'registrationForm' })(Form)

const mapDispatchToProps = (dispatch: any) => bindActionCreators({ registration }, dispatch)
const mapStateToProps = (state: any) => ({ ...state });

export default connect(mapStateToProps, mapDispatchToProps)(RegistrationForm);
