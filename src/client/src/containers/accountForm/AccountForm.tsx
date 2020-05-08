import React, { SyntheticEvent } from 'react';
import { connect } from 'react-redux';
import { store } from '../..';
import { bindActionCreators } from 'redux';
import {fetchThemes, fetchFullInfo} from '../../actions/fetchTheme';
import './AccountForm.css';

class AccountForm extends React.Component<any> {
    themes: any;
    constructor(props : any){
            super(props);
            this.state = {
                listThemes: []
            }
    }
    componentWillMount(){
        this.props.fetchThemes();
    }

     NoteList() {
        const notesList = this.props.fullInfoAboutTheme.notes.map((note: {title: React.ReactNode; content: React.ReactNode;}) =>
        <ul>
            <li>
                title - {note.title}
            </li>
            <li>
                content - {note.content}
            </li>
        </ul>
        )
        if(notesList.length != 0){
            return notesList;
        }
        else{
            return(<div>No notes found</div>);
        }
      }

      CardList(){
        const cardsList = this.props.fullInfoAboutTheme.cards.map((card: {question: React.ReactNode;}) =>
        <ul>
            <li>
                title - {card.question}
            </li>
        </ul>
        )
        if(cardsList.length != 0){
            return cardsList;
        }
        else{
            return(<div>No cards found</div>);
        }
      }

      SubThemesList(){
        const subThemeList = this.props.fullInfoAboutTheme.subThemes.map((subTheme: 
                            {name: React.ReactNode; desсription: React.ReactNode; id: React.ReactNode;}) =>
        <ul>
            <li>
                Name - {subTheme.name}
            </li>
            <li>
                Desсription - {subTheme.desсription}
            </li>
            <button onClick={() => this.getFullInfoAboutTheme(subTheme.id)}>Full info</button>
        </ul>
        )
        if(subThemeList.length != 0){
            return subThemeList;
        }
        else{
            return(<div>No cards found</div>);
        }
      }

      FormListForNotRootTheme(){
            return (
            <div>
            <div>Current theme name - {this.props.fullInfoAboutTheme.name}</div>
            Notes - {this.NoteList()}
            Cards - {this.CardList()}
            Sub Themes - {this.SubThemesList()}
            </div>);
      }

      FormListForRootTheme(){
        return(
            this.props.themes.themes.map((theme: { name: React.ReactNode; id: any; }) =>
        <li>
            Name - {theme.name}<br></br>
            <button onClick={() => this.getFullInfoAboutTheme(theme.id)}>Get full info</button>
        </li>));
      }
      
    render() {
        if(this.props.fullInfoAboutTheme.isRoot == false){
            this.themes = this.FormListForNotRootTheme();
        }
        else{
            this.themes = this.FormListForRootTheme();
        }
        return (
            <div>
                Hello {store.getState().accounts.userName} Your themes:
                <ul>{this.themes}</ul>
            </div>
        );
    }

    getFullInfoAboutTheme(id : any){
        this.props.fetchFullInfo(id);
    }
}

const mapDispatchToProps = (dispatch: any) => bindActionCreators({ fetchThemes, fetchFullInfo }, dispatch)
const mapStateToProps = (state: any) => ({ ...state });

export default connect(mapStateToProps, mapDispatchToProps)(AccountForm);
