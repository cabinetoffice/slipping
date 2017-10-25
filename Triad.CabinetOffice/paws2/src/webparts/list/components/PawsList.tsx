import * as React from 'react';
import './PawsList.module.scss';
import { IPawsListProps } from './IPawsListProps';
import { IPawsListState } from './IPawsListState';
import { escape } from '@microsoft/sp-lodash-subset';
import { TextField } from 'office-ui-fabric-react/lib/TextField';
import { Icon } from 'office-ui-fabric-react/lib/Icon';
import { List } from 'office-ui-fabric-react/lib/List';
import { PrimaryButton, IButtonProps } from 'office-ui-fabric-react/lib/Button';
import { HttpClient } from '@microsoft/sp-http';
import { ListItem, IListItem } from '../../../types/ListItem';
import { PawsListService } from './PawsListService';

export default class PawsList extends React.Component<IPawsListProps, IPawsListState> {
  private allItems: IListItem[];
  private dataService: PawsListService;

  constructor(props: IPawsListProps) {
    super(props);

    this.dataService = new PawsListService(this.props.httpClient, this.props.itemsUrl, this.props.nameProperty, this.props.descProperty);

    this.onFilterChanged = this.onFilterChanged.bind(this);
    this.handleNew = this.handleNew.bind(this);

    this.state = { items: [] };

    window.addEventListener('pawsReload', (e: CustomEvent) => {
      if (e.detail && e.detail.change) {
        this.componentDidMount();
        // TODO: Reload only changed item
        // if (e.detail.change === 'add') {
        //   this.dataService.getItem(e.detail.id).then((item: IListItem): void => {
        //     this.allItems.push(item);
        //   });
        // }
        // if (e.detail.change === 'edit') {
        //   this.dataService.getItem(e.detail.id).then((item: IListItem): void => {
        //     this.allItems[this.indexOfItem(this.allItems, e.detail.id)] = item;
        //   });
        // }
        // if (e.detail.change === 'delete') {
        //   this.allItems.splice(this.indexOfItem(this.allItems, e.detail.id), 1);
        // }
        // this.onFilterChanged(this.state.filterText);
      }
    });
  }

  public render(): React.ReactElement<IPawsListProps> {
    let originalItems = this.allItems || [];
    let { items } = this.state;
    let resultCountText = items.length === originalItems.length ? '' : ` (${items.length} of ${originalItems.length} shown)`;

    return (
      <div>
        <h2>{this.props.title}</h2>
        <PrimaryButton text="Add new session" onClick={this.handleNew} />
        <TextField label={'Filter by title' + resultCountText} onBeforeChange={this.onFilterChanged} />
        <List items={items} onRenderCell={this.onRenderCell} className="pawsList" />
      </div>
    );
  }

  public componentDidMount(): void {
    if (this.settingsConfigured()) {
      this.dataService.getItems().then((items: IListItem[]): void => {
        this.allItems = items;
        this.setState({ items: items });
      });
    }
  }

  private settingsConfigured(): boolean {
    return this.props.itemsUrl !== undefined && this.props.nameProperty !== undefined;
  }

  private onFilterChanged(text: string) {
    let items = this.allItems;

    this.setState({
      filterText: text,
      items: text ?
        items.filter(item => item.name.toLowerCase().indexOf(text.toLowerCase()) >= 0) :
        items
    });
  }

  private onRenderCell(item: IListItem, index: number | undefined): JSX.Element {
    return (
      <a href={'#' + item.id} className="noLinkDecoration">
        <div className='ms-ListBasicExample-itemCell' data-is-focusable={true}>
          <div className='ms-ListBasicExample-itemContent'>
            <div className='ms-ListBasicExample-itemName'>{item.name}</div>
            <div className='ms-ListBasicExample-itemDesc'>{item.description}</div>
          </div>
          <Icon
            className='ms-ListBasicExample-chevron'
            iconName={'ChevronRight'}
          />
        </div>
      </a>
    );
  }

  private handleNew(e): void {
    e.preventDefault();
    location.hash = '';
  }

  private indexOfItem(arr: Array<IListItem>, id: number): number {
    for (var i = 0; i < arr.length; i++) {
      if (arr[i].id === id)
        return i;
    }
  }
}
