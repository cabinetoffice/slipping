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
import { IListWebPartProps } from './IListWebPartProps';

export default class ListWebPart extends BaseClientSideWebPart<IListWebPartProps> {
  private apiLoaded:boolean = false;

  public render(): void {
    const element: React.ReactElement<IPawsListProps > = React.createElement(
      PawsList,
      {
        title: this.properties.title,
        itemsUrl: this.properties.itemsUrl,
        nameProperty: this.properties.nameProperty,
        descProperty: this.properties.descriptionProperty,
        httpClient: this.context.httpClient
      }
    );

    this.domElement.innerHTML += `
      <div class="loading"></div>
      <iframe src="${this.properties.itemsUrl}" style="display:none;"></iframe>
      <div id="paws-list"></div>
    `;

    this.context.statusRenderer.displayLoadingIndicator(this.domElement.querySelector(".loading"), "items");

    this.domElement.querySelector('iframe').addEventListener('load', ():void => {
      this.apiLoaded = true;
    });

    this.executeOrDelayUntilRemoteApiLoaded(():void=>{
      ReactDom.render(element, this.domElement.querySelector('#paws-list'));
      this.context.statusRenderer.clearLoadingIndicator(this.domElement.querySelector(".loading"));
    });
  }

  protected get dataVersion(): Version {
    return Version.parse('1.0');
  }
  
  protected get disableReactivePropertyChanges(): boolean { return true; }

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
                  label: strings.ItemsUrlFieldLabel
                }),
                PropertyPaneTextField('nameProperty', {
                  label: strings.NamePropertyFieldLabel
                }),
                PropertyPaneTextField('descriptionProperty', {
                  label: strings.DescriptionPropertyFieldLabel
                })
              ]
            }
          ]
        }
      ]
    };
  }

  private executeOrDelayUntilRemoteApiLoaded(func:Function):void {
    if(this.apiLoaded) {
      func();
    } else {
      setTimeout(():void => {this.executeOrDelayUntilRemoteApiLoaded(func);}, 100);
    }
  }
}
