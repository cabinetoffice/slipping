import * as React from 'react';
import * as ReactDom from 'react-dom';
import { Version } from '@microsoft/sp-core-library';
import {
  BaseClientSideWebPart,
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-webpart-base';

import * as strings from 'PartyFormWebPartStrings';
import PartyForm from './components/PartyForm';
import { IPartyFormProps } from './components/IPartyFormProps';
import { IPartyFormWebPartProps } from './IPartyFormWebPartProps';

export default class PartyFormWebPart extends BaseClientSideWebPart<IPartyFormWebPartProps> {
  private apiLoaded:boolean = false;
  private apiUrl:string = "https://paws-api-dev.azurewebsites.net";

  public render(): void {
    const element: React.ReactElement<IPartyFormProps > = React.createElement(
      PartyForm,
      {
        apiUrl: this.properties.apiUrl,
        httpClient: this. context.httpClient
      }
    );

    this.domElement.innerHTML += `
      <iframe src="${this.apiUrl}" style="display:none;"></iframe>
      <div id="seshForm"></div>
    `;

    this.domElement.querySelector('iframe').addEventListener('load', ():void => {
      this.apiLoaded = true;
    });

    this.executeOrDelayUntilRemoteApiLoaded(():void=>{
      ReactDom.render(element, this.domElement.querySelector('#seshForm'));
    });
  }

  private executeOrDelayUntilRemoteApiLoaded(func:Function):void{
    if(this.apiLoaded){
      func();
    }else{
      setTimeout(():void=> {this.executeOrDelayUntilRemoteApiLoaded(func);}, 100);
    }
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
                PropertyPaneTextField('apiUrl', {
                  label: strings.ApiUrlFieldLabel
                })
              ]
            }
          ]
        }
      ]
    };
  }
}
