import { getToken } from '../../actions/getToken';
import { fetchTheme } from '../../actions/fetchTheme';
import { Switch, Route } from 'react-router-dom';
import SignInForm from '../SignForm';
import Header from '../Header';
import React from 'react';
import RegistrationForm from '../RegistrationForm';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';


class App extends React.Component<any, any>{
  render() {
    const { getToken, fetchTheme } = this.props;
    return (
      <div>
        <Header></Header>
        <button onClick={fetchTheme}></button>
        <Switch>
          <Route path="/registration">
            <RegistrationForm />
          </Route>
          <Route path="/signIn">
            <SignInForm onLogin={getToken} />>
            <br />
          </Route>
          <Route path="/account">
            <p>вошел подлец</p>
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

let mapToStateProps = (state: any) => ({ ...state });


let mapToDispatchProps = (dispatch: any) =>
  bindActionCreators({
    getToken,
    fetchTheme
  }, dispatch);



export default connect(mapToStateProps, mapToDispatchProps)(App);

