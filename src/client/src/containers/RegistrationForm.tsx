import React, { SyntheticEvent } from 'react';
export default class RegistrationForm extends React.Component<any, any, any> {
    constructor(props:any) {
        super(props);
        this.state = {
            login: "",
            password: "",
            email: ""
        }
    }

    render() {
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

    handleLoginChange(event: any) {
        this.setState({login: event.target.value, password: this.state.password, email: this.state.email});
    }

    handlePasswordChange(event:any) {
        this.setState({login: this.state.login, password: event.target.value, email: this.state.email});
    }

    handleEmailChange(event:any) {
        this.setState({login: event.target.value, password: this.state.password, email: this.state.email})
    }
    handleSubmit(){

    }
}