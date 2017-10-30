import * as React from 'react';

import styles from './PawsList.module.scss';
import { IPawsListProps } from './IPawsListProps';
import { IPawsListState } from './IPawsListState';
import { PawsListService } from './PawsListService';

import { escape } from '@microsoft/sp-lodash-subset';
import { HttpClient } from '@microsoft/sp-http';

import { TextField } from 'office-ui-fabric-react/lib/TextField';
import { Icon } from 'office-ui-fabric-react/lib/Icon';
import { List } from 'office-ui-fabric-react/lib/List';
import { PrimaryButton, IButtonProps } from 'office-ui-fabric-react/lib/Button';
import { autobind } from 'office-ui-fabric-react/lib/Utilities';

import * as types from '../../../types/index';

export default class PawsList extends React.Component<IPawsListProps, IPawsListState> {
  private allItems: types.IListItem[];
  private dataService: PawsListService;

  constructor(props: IPawsListProps) {
    super(props);

    this.dataService = new PawsListService(this.props.httpClient, this.props.itemsUrl, this.props.nameProperty, this.props.descProperty);

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
    window.addEventListener('hashchange', (e) => {
      this.highlightSelectedItem();
    });
  }

  public render(): React.ReactElement<IPawsListProps> {
    let originalItems = this.allItems || [];
    let { items } = this.state;
    let resultCountText = items.length === originalItems.length ? '' : ` (${items.length} of ${originalItems.length} shown)`;

    return (
      <div className={styles.pawsList} >
        <h1>{this.props.title}</h1>
        <PrimaryButton text={`Create a new ${this.props.createNewText || '...'}`} onClick={this.handleNew} />
        <TextField label={'Filter by title' + resultCountText} onBeforeChange={this.onFilterChanged} />
        <List items={items} onRenderCell={this.onRenderCell} className={styles.pawsListItemContainer} />
      </div>
    );
  }

  public componentDidMount(): void {
    if (this.settingsConfigured()) {
      this.dataService.getItems().then((items: types.IListItem[]): void => {
        this.allItems = items;
        this.setState({ items: items });
        this.highlightSelectedItem();
      });
    }
  }

  private settingsConfigured(): boolean {
    return this.props.itemsUrl !== undefined && this.props.nameProperty !== undefined;
  }

  private highlightSelectedItem(): void {
    var id = parseInt(location.hash.substring(1));
    if (!isNaN(id)) {
      var items = document.getElementsByClassName(styles.msListBasicExampleItemCell);
      for (var i = 0; i < items.length; i++) {
        items[i].classList.remove(styles.msListBasicExampleItemCellSelected);
      }
      var item = document.getElementById(`pawsItem${id}`);
      if (item) item.classList.add(styles.msListBasicExampleItemCellSelected);
    }
  }

  @autobind
  private onFilterChanged(text: string) {
    let items = this.allItems;

    this.setState({
      filterText: text,
      items: text ?
        items.filter(item => item.name.toLowerCase().indexOf(text.toLowerCase()) >= 0 || item.description.toLowerCase().indexOf(text.toLowerCase()) >= 0) :
        items
    });
  }

  private onRenderCell(item: types.IListItem, index: number | undefined): JSX.Element {
    return (
      <a href={'#' + item.id} className={styles.noLinkDecoration}>
        <div className={styles.msListBasicExampleItemCell} data-is-focusable={true} id={`pawsItem${item.id}`}>
          <div className={styles.msListBasicExampleItemContent}>
            <div className={styles.msListBasicExampleItemName}>{item.name}</div>
            <div>{item.description}</div>
          </div>
          <Icon
            className={styles.msListBasicExampleChevron}
            iconName={'ChevronRight'}
          />
        </div>
      </a>
    );
  }

  @autobind
  private handleNew(e): void {
    e.preventDefault();
    location.hash = '';
  }

  private indexOfItem(arr: Array<types.IListItem>, id: number): number {
    for (var i = 0; i < arr.length; i++) {
      if (arr[i].id === id)
        return i;
    }
  }
}
