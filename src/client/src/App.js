import React from 'react';
import {connect} from "react-redux"; 
import SignInForm from './SignForm';
import { Switch, Route } from 'react-router-dom';
import Header from './Header';

class App extends React.Component{

  render() {
    return (
      <div>
      <Header></Header>
      <Switch>
        <Route to="signIn">
          <SignInForm></SignInForm>
        </Route>
      </Switch>
        
      </div>
    );
    

  }
  clickHandler() {
    this.props.onCheck();
  }
}

export default connect(state => ({
  track: []
}),  
dispatch => ({
  onSignIn(user){
    
  },
  onCheck: () => {
    dispatch({type: "CHECK", payload: {}});
  }
}))(App);
