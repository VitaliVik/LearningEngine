import React from 'react';
import './Header.css';
import { connect } from 'react-redux';
import { NavLink } from 'react-router-dom';

class Header extends React.Component<any> {
    render() {
        const {access_token, username} = this.props
        return (
            <div className="header">
                <h1>Learning Engine</h1>
                <div>{access_token != "" && <NavLink to="signIn">Войти</NavLink>} </div>
            </div>
        );
    }
}

const mapStateToProps = (state: any) => state.accounts;

export default connect(mapStateToProps)(Header);