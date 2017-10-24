import * as React from 'react';
import './PawsList.module.scss';
import { IPawsListProps } from './IPawsListProps';
import { IPawsListState } from './IPawsListState';
import { escape } from '@microsoft/sp-lodash-subset';
import { TextField } from 'office-ui-fabric-react/lib/TextField';
import { Icon } from 'office-ui-fabric-react/lib/Icon';
import { List } from 'office-ui-fabric-react/lib/List';
import { DefaultButton, IButtonProps } from 'office-ui-fabric-react/lib/Button';
import { HttpClient } from '@microsoft/sp-http';
import { ListItem, IListItem } from '../../../types/ListItem';

export default class PawsList extends React.Component<IPawsListProps, IPawsListState> {
  private allItems: IListItem[];

  constructor(props:IPawsListProps){
    super(props);

    this._onFilterChanged = this._onFilterChanged.bind(this);
    this._handleNew = this._handleNew.bind(this);

    this.state = {items:[]};

    window.addEventListener('pawsReload', (e) => {
        this.componentDidMount();
    });
  }

  public render(): React.ReactElement<IPawsListProps> {
    let originalItems = this.allItems || [];
    let { items } = this.state;
    let resultCountText = items.length === originalItems.length ? '' : ` (${items.length} of ${originalItems.length} shown)`;
    
    return (
      <div>
        <h2>{this.props.title}</h2>
        <DefaultButton text="Add new session" onClick={ this._handleNew } />
        <TextField label={'Filter by title' + resultCountText } onBeforeChange={ this._onFilterChanged } />
        <List items={items} onRenderCell={this._onRenderCell} className="pawsList" />
      </div>
    );
  }

  public componentDidMount():void {
    this.getItems().then((items:IListItem[]):void=>{
      this.allItems = items;
      this.setState({ items: items });
    });
  }

  private _onFilterChanged(text: string) {
    let items = this.allItems;

    this.setState({
      filterText: text,
      items: text ?
        items.filter(item => item.name.toLowerCase().indexOf(text.toLowerCase()) >= 0) :
        items
    });
  }

  private _onRenderCell(item: IListItem, index: number | undefined): JSX.Element {
    return (
        <a href={'#' + item.id} className="noLinkDecoration">
        <div className='ms-ListBasicExample-itemCell' data-is-focusable={ true }>
          <div className='ms-ListBasicExample-itemContent'>
            <div className='ms-ListBasicExample-itemName'>{ item.name }</div>
            <div className='ms-ListBasicExample-itemDesc'>{ item.description }</div>
          </div>
          <Icon
            className='ms-ListBasicExample-chevron'
            iconName={ 'ChevronRight' }
          />
        </div>
        </a>
    );
  }

  private _handleNew(e):void{
    e.preventDefault();
    location.hash='';
  }

  //#region Service Functions
  private getItems(): Promise<IListItem[]>{
    return new Promise<IListItem[]>((resolve: (items:IListItem[])=>void, reject:(error:any)=>void):void=>{
      this.props.httpClient.get(`${this.props.itemsUrl}?$select=ID,${this.props.nameProperty},${this.props.descProperty}`, HttpClient.configurations.v1, { credentials:'include' })
      .then((response:Response):Promise<IListItem[]> => {
        return response.json();
      })
      .then((data:any):void=>{
        var items = [];
        data.value.forEach(element => {
          var item = {
            id: element['ID'],
            name: element[this.props.nameProperty],
            description: element[this.props.descProperty]
          };
          items.push(item);
        });
          resolve(items);
      },(error:any):void=>{
          reject(error);
      });
    });
  }
  //#endregion
}
