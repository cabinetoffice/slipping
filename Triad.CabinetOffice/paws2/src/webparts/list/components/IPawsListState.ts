import { IListItem } from '../../../types/ListItem';

export interface IPawsListState {
    filterText?: string;
    items?: IListItem[];
}
