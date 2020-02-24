import React from 'react';
import SignInForm from './SignForm';
import { Switch, Route } from 'react-router-dom';
import Header from './Header';
import { connect } from 'react-redux';
import axios from 'axios';
import RegistrationForm from './RegistrationForm';
import { bindActionCreators } from 'redux';
import { getToken } from '../actions/getToken';

class App extends React.Component<any, any>{
  constructor(props: any) {
    super(props);
    console.log(props);
  }

  render() {
    return (
      <div>
        <Header></Header>
        <Switch>
        <Route path="/registration">
            <RegistrationForm />
          </Route>
          <Route path="/signIn">
            <SignInForm onLogin={getToken}/>>
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

let mapToStateProps = (state:any) => ({ ...state});

let mapToDispatchProps = (dispatch: any) => 
  bindActionCreators({
    getToken: getToken
  }, dispatch);



export default connect(mapToStateProps, mapToDispatchProps)(App);

