import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { registration } from '../../actions/regitstration'
import { Link, Redirect } from 'react-router-dom';
import { reduxForm, Field } from 'redux-form';
import { required as r, email, length, confirmation } from 'redux-form-validators';
import './style.css';
import { CommonField } from '../../components/CommonField';

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

class RegistrationForm extends React.Component<any, any, any> {
    render() {
        const { error, accessToken } = this.props.accounts;
        if (accessToken !== '') {
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



const Form = (props: any) => {
    return (
        <form onSubmit={props.handleSubmit} method='POST'>
            <Field
                name='userName'
                component={CommonField}
                type='text'
                placeholder='login'
                validate={r()}
            />
            <Field
                name='email'
                component={CommonField}
                type='email'
                placeholder='email'
                validate={[r(), email()]}
            />
            <Field
                name='password'
                component={CommonField}
                type='password'
                placeholder='password'
                validate={[r(), length({in: [6, 16], msg: 'Длина пароля должна быть от 6 до 16 символов!'})]}
            />
            <Field
                name='passwordConfirmation'
                component={CommonField}
                type='password'
                placeholder='confirm your password'
                validate={[r(), confirmation({field: 'password', msg: 'Пароли не совпадают!'})]}
            />
            <button>Регестрация</button>
        </form>
    )
}

const ReduxForm = reduxForm<{}, {}>({ form: 'registrationForm' })(Form)

const mapDispatchToProps = (dispatch: any) => bindActionCreators({ registration }, dispatch)
const mapStateToProps = (state: any) => ({ ...state });

export default connect(mapStateToProps, mapDispatchToProps)(RegistrationForm);
