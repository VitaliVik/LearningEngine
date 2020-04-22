import React from 'react';
import { connect } from 'react-redux';
import { store } from '../..';


class AccountForm extends React.Component {
    render() {
        return (
            <div>
                {store.getState().accounts.userName}
            </div>
        );
    }
}

export default connect()(AccountForm);
