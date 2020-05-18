import { Switch, Route } from 'react-router-dom';
import SignInForm from '../signInForm/SignForm';
import Header from './Header';
import React from 'react';
import RegistrationForm from '../registrationForm/RegistrationForm';
import { connect } from 'react-redux';
import AccountForm from '../accountForm/AccountForm';


class App extends React.Component<any, any>{
  render() {
    return (
      <div>
        <Header></Header>
        <Switch>
          <Route path="/registration">
            <RegistrationForm />
          </Route>
          <Route path="/signIn">
            <SignInForm />
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

const mapStateToProps = (state: any) => ({ ...state });

export default connect(mapStateToProps)(App);


