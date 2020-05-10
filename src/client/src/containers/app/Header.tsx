import React from 'react';
import './Header.css';
import { connect } from 'react-redux';
import { NavLink } from 'react-router-dom';

class Header extends React.Component<any> {
    render() {
        const {accessToken, username} = this.props
        const signInBar = accessToken != ''
        ?   <div>
                <button onClick={ e => this.buttonHandle() }>Выйти</button>
            </div>
        :   <div>
                <NavLink to='/registration'>Регистрация</NavLink>
                <NavLink to='/signIn'>Войти</NavLink>
            </div>
        return (
            <div className="header">
                <h1>Learning Engine</h1>
                {signInBar}
            </div>
        );
    }
    
    buttonHandle() : void {

    }
}

const mapStateToProps = (state: any) => state.accounts;

export default connect(mapStateToProps)(Header);