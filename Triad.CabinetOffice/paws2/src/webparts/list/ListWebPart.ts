import * as React from 'react';
import * as ReactDom from 'react-dom';
import { Version } from '@microsoft/sp-core-library';
import {
  BaseClientSideWebPart,
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-webpart-base';

import * as strings from 'ListWebPartStrings';
import PawsList from './components/PawsList';
import { IPawsListProps } from './components/IPawsListProps';

export interface IListWebPartProps {
  title: string;
  itemsUrl: string;
}

export default class ListWebPart extends BaseClientSideWebPart<IListWebPartProps> {
  private remoteApiLoaded:boolean = false;
  private apiUrl:string = "https://paws-api-dev.azurewebsites.net"; // TODO: get this from web part props

  public render(): void {
    this.properties.title='Sessions';
    const element: React.ReactElement<IPawsListProps > = React.createElement(
      PawsList,
      {
        itemsUrl: this.apiUrl + '/odata/Sessions', // TODO: get this from web part props
        httpClient: this.context.httpClient
      }
    );

    this.domElement.innerHTML += `
      <h2>${this.properties.title}</h2>
      <iframe src="${this.apiUrl}/odata/Sessions" style="display:none;"></iframe>
      <div id="paws-list"></div>
    `;

    this.domElement.querySelector('iframe').addEventListener('load', ():void => {
      this.remoteApiLoaded = true;
    });
    this.executeOrDelayUntilRemoteApiLoaded(():void=>{
      ReactDom.render(element, this.domElement.querySelector('#paws-list'));
    });
  }

  protected get dataVersion(): Version {
    return Version.parse('1.0');
  }

  protected getPropertyPaneConfiguration(): IPropertyPaneConfiguration {
    return {
      pages: [
        {
          header: {
            description: strings.PropertyPaneDescription
          },
          groups: [
            {
              groupName: strings.BasicGroupName,
              groupFields: [
                PropertyPaneTextField('title', {
                  label: strings.TitleFieldLabel
                }),
                PropertyPaneTextField('itemsUrl', {
                  label: 'Items URL'
                })
              ]
            }
          ]
        }
      ]
    };
  }

  private executeOrDelayUntilRemoteApiLoaded(func:Function):void{
    if(this.remoteApiLoaded){
      func();
    }else{
      setTimeout(():void=> {this.executeOrDelayUntilRemoteApiLoaded(func);}, 100);
    }
  }
}
