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
    componentWillMount(){
        this.props.fetchThemes();
    }
    render() {
        const listThemes = this.props.themes.themes.map((theme: { id: React.SyntheticEvent<Element, Event>; 
            name: React.ReactNode; desription: React.ReactNode; }) =>
            <body>
                <tr>
                    <td>{theme.name}</td>
                    <td>{theme.desription}</td>
                </tr>
            </body>
        )
        return (
            <div>
                Hello {store.getState().accounts.username}
                <table>
                     <header>
                        <tr>
                            <td>Тема</td>
                            <td>Описание</td>
                        </tr>
                     </header>
                    {listThemes}
                 </table>
            </div>
        );
    }
}

const mapDispatchToProps = (dispatch: any) => bindActionCreators({ fetchThemes }, dispatch)
const mapStateToProps = (state: any) => ({ ...state });

export default connect(mapStateToProps, mapDispatchToProps)(AccountForm);
