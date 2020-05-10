import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { registration } from '../../actions/regitstration'
import { Link, Redirect } from 'react-router-dom';
import { store } from '../..';
import { accounts } from '../../reducers/accounts';
import { reduxForm, Field, FormDecorator, Fields } from 'redux-form';
import { required, maxLengthCreator, minLengthCreator, emailValidation } from '../../utils/validators/validators';
import './style.css';
import { render } from '@testing-library/react';
import { StateObservable } from 'redux-observable';

interface RegistrationProps {
    error: string,
    isLoading: string,
    accessToken: string,
    registration: any
}

interface RegistrationData {
    email: string,
    username: string,
    password: string
}

const minLength6 = minLengthCreator(6);
const maxLength16 = maxLengthCreator(16);
const passwordConfirm = (value: string) => {
    
}

class RegistrationForm extends React.Component<any, any, any> {
    render() {
        const { error, isLoading, accessToken } = this.props.accounts;
        const onSubmit = (props: any) => {
            console.log(props);
        }
        if (accessToken != '') {
            return (
                <>
                    <Redirect to="account" />
                </>
            )
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
        const { username, email, password } = formData;
        this.props.registration({
            username,
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
                name='username'
                component={renderField}
                type='text'
                placeholder='login'
                validate={required}
            />
            <Field
                name='email'
                component={renderField}
                type='email'
                placeholder='email'
                validate={[required, emailValidation]}
            />
            <Field
                name='password'
                component={renderField}
                type='password'
                placeholder='password'
                validate={[required, minLength6, maxLength16]}
            />
            <button>Регестрация</button>
        </form>
    )
}

const ReduxForm = reduxForm<{}, {}>({ form: 'registrationForm' })(Form)

const mapDispatchToProps = (dispatch: any) => bindActionCreators({ registration }, dispatch)
const mapStateToProps = (state: any) => ({ ...state });

export default connect(mapStateToProps, mapDispatchToProps)(RegistrationForm);
