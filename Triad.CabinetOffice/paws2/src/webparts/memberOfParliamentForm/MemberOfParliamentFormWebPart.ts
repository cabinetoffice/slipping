import * as React from 'react';
import * as ReactDom from 'react-dom';
import { Version } from '@microsoft/sp-core-library';
import {
  BaseClientSideWebPart,
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-webpart-base';

import * as strings from 'MemberOfParliamentFormWebPartStrings';
import MemberOfParliamentForm from './components/MemberOfParliamentForm';
import { IMemberOfParliamentFormProps } from './components/IMemberOfParliamentFormProps';
import { IMemberOfParliamentFormWebPartProps } from './IMemberOfParliamentFormWebPartProps';

export default class MemberOfParliamentFormWebPart extends BaseClientSideWebPart<IMemberOfParliamentFormWebPartProps> {
  private apiLoaded:boolean = false;
  private apiUrl:string = "https://paws-api-dev.azurewebsites.net";

  public render(): void {
    const element: React.ReactElement<IMemberOfParliamentFormProps> = React.createElement(
      MemberOfParliamentForm,
      {
        apiUrl: this.properties.apiUrl,
        httpClient: this.context.httpClient
      }
    );
    this.domElement.innerHTML += `
      <iframe src="${this.apiUrl}" style="display:none;"></iframe>
      <div id="mpForm"></div>
    `;

    this.domElement.querySelector('iframe').addEventListener('load', ():void => {
      this.apiLoaded = true;
    });

    this.executeOrDelayUntilRemoteApiLoaded(():void=>{
      ReactDom.render(element, this.domElement.querySelector('#mpForm'));
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
