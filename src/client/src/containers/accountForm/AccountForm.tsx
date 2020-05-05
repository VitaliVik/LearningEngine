import React, { SyntheticEvent } from 'react';
import { connect } from 'react-redux';
import { store } from '../..';
import { bindActionCreators } from 'redux';
import {fetchThemes, fetchSubThemes} from '../../actions/fetchTheme';
import { themes } from '../../reducers/themes';

class AccountForm extends React.Component<any> {
    constructor(props : any){
            super(props);
            this.state = {
                listThemes: []
            }
    }
    componentWillMount(){
        this.props.fetchThemes();
    }
     NoteList(props: any) {
        if(props){
            const listItems = props.map((note: { title: React.ReactNode; content: React.ReactNode; }) =>    
              <li>Название - {note.title}<br></br> Описание - {note.content} </li> );  return (
              <ul>{listItems}</ul>  ); 
        }
        return(<div>No notes found</div>);
      }
      CardList(props : any){
          if(props){
            const listItems = props.map((card: { question: React.ReactNode }) =>    
            <li>Вопрос - {card.question}</li> );  return (
              <ul>{listItems}</ul>  ); 
        }
      }
    render() {
        let listThemes;
        if(this.props.themes.isRoot == false)
        {
            listThemes = this.props.themes.themes.map((theme: { name: React.ReactNode; desсription: React.ReactNode; 
                                                        notes: [{ title: React.ReactNode; content: React.ReactNode; }]; 
                                                        cards: [{ question: React.ReactNode }]; id: any; }) =>
            <div>
            <ul style = {{display: 'inline-block', width : '30%', height : 150}}>
                <li>
                Name - 
                {theme.name}<br></br>
                Description - 
                {theme.desсription}<br></br>
                <button onClick={() => this.getSubThemes(theme.id)}>Get subthemes</button>
                </li>
            </ul>
            <ul style = {{display: 'inline-block', width : '30%', height : 150}}>
                <li>
                    Notes - 
                    {this.NoteList(theme.notes)}
                </li>
            </ul>
            <ul style = {{display: 'inline-block', width : '30%', height : 150}}>
                <li>
                    Cards - 
                    {this.CardList(theme.cards)}
                </li>
            </ul>
            </div>

        )
        return (
            <div>
                Hello {store.getState().accounts.userName} Your themes:
                <ul>{listThemes}</ul>
            </div>
        );
        }
        else
        {
             listThemes = this.props.themes.themes.map((theme: { name: React.ReactNode; id: any; }) =>
            <li>
                Name - {theme.name}<br></br>
                <button onClick={() => this.getSubThemes(theme.id)}>Get subthemes</button>
             </li>
        )
        return (
            <div>
                Hello {store.getState().accounts.userName} Your themes:
                <ul>{listThemes}</ul>
            </div>
        );
        }

    }

    getSubThemes(id : any){
        this.props.fetchSubThemes(id);
    }
}

const mapDispatchToProps = (dispatch: any) => bindActionCreators({ fetchThemes, fetchSubThemes }, dispatch)
const mapStateToProps = (state: any) => ({ ...state });

export default connect(mapStateToProps, mapDispatchToProps)(AccountForm);
