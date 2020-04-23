import React, { SyntheticEvent } from 'react';
import { connect } from 'react-redux';
import { store } from '../..';
import { bindActionCreators } from 'redux';
import {fetchThemes} from '../../actions/fetchTheme';

class AccountForm extends React.Component<any> {
    constructor(props : any){
            super(props);
            this.state = {
                themes : []
            }
    }
    render() {
        return (
            <div>
                Hello {store.getState().accounts.userName}
                <button onClick={this.getThemes.bind(this)}>Get themes</button>
                <button onClick={this.checkHyita.bind(this)}>Get hyita</button>
            </div>
        );
    }

    getThemes(event: SyntheticEvent){
        event.preventDefault();
        this.props.fetchThemes();
    }
    checkHyita(event: SyntheticEvent){
        const a = this.props.themes;
        const b = store.getState().themes;
    }
}

const mapDispatchToProps = (dispatch: any) => bindActionCreators({ fetchThemes }, dispatch)
const mapStateToProps = (state: any) => ({ ...state });

export default connect(mapStateToProps, mapDispatchToProps)(AccountForm);
