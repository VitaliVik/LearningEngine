import { getToken } from '../../actions/getToken';
import { fetchTheme } from '../../actions/fetchTheme';
import { Switch, Route, NavLink, Redirect } from 'react-router-dom';
import SignInForm from '../signInForm/SignForm';
import Header from './Header';
import React from 'react';
import RegistrationForm from '../registrationForm/RegistrationForm';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import AccountForm from '../accountForm/AccountForm';


class App extends React.Component<any, any>{
  render() {
    const { accessToken } = this.props;
    return (
      <div>
        <Header></Header>
        <Switch>
          <Route path="/registration">
            <RegistrationForm />
          </Route>
          <Route path="/signIn">
            <SignInForm />
            <br />
          </Route>
          <Route path="/account">
            <AccountForm/>
          </Route>
          <Route path="/">
          </Route>
        </Switch>
      </div>
    );
  }
}

interface Theme {
  id: number,
  name: string,
  description: string,
  isPublic: boolean,
  parentTheme: Theme,
  subThemes: Array<Theme>
  notes: Array<Note>
}

interface Note {
  title: string;
  content: string;
}

let mapStateToProps = (state: any) => ({ ...state });


let mapToDispatchProps = (dispatch: any) =>
  bindActionCreators({
    getToken,
    fetchTheme
  }, dispatch);



export default connect(mapStateToProps)(App);


