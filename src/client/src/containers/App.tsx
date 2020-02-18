import React from 'react';
import SignInForm from './SignForm';
import { Switch, Route } from 'react-router-dom';
import Header from './Header';

export default class App extends React.Component<{}, any>{
  // constructor(props:any) {
  //   super(props);
    
  // }

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
}

// export default connect(state => ({
//   track: []
// }),  
// dispatch => ({
//   onSignIn(username:String, password:String){
    
//   },
//   onCheck: () => {
//     dispatch({type: "CHECK", payload: {}});
//   }
// }))(App);
