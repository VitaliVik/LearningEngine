import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { registration } from '../../actions/regitstration'
import { Link } from 'react-router-dom';

class RegistrationForm extends React.Component<any, any, any> {
    constructor(props: any) {
        super(props);
        this.state = {
            login: "",
            password: "",
            email: ""
        }
    }

    render() {
        const { error, isLoading } = this.props.registration;
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
                <div>
                    <form onSubmit={this.handleSubmit.bind(this)}>
                        <input type="text"
                            placeholder="login"
                            onChange={this.handleLoginChange.bind(this)}
                        />
                        <br />
                        <input type="email"
                            placeholder="email"
                            onChange={this.handleEmailChange.bind(this)}
                        />
                        <br />
                        <input type="password"
                            placeholder="password"
                            onChange={this.handlePasswordChange.bind(this)}
                        />
                        <br />
                        <button>Регестрация</button>
                    </form>

                </div>
            );
        }
    }

    handleLoginChange(event: any) {
        this.setState({
            ...this.state,
            login: event.target.value
        });
    }

    handlePasswordChange(event: any) {
        this.setState({
            ...this.state,
            password: event.target.value,
        });
    }

    handleEmailChange(event: any) {
        this.setState({
            ...this.state,
            email: event.target.value
        });
    }

    handleSubmit(event: any) {
        event.preventDefault();
        const { username, email, password } = this.state;
        registration({
            username,
            email,
            password
        })
    }
}

const mapDispatchToProps = (dispatch: any) => bindActionCreators({ registration }, dispatch)
const mapStateToProps = (state: any) => ({ ...state });

export default connect(mapStateToProps, mapDispatchToProps)(RegistrationForm);


