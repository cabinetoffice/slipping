import * as React from 'react';
import * as ReactDom from 'react-dom';
import { Version } from '@microsoft/sp-core-library';
import {
  BaseClientSideWebPart,
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-webpart-base';

import * as strings from 'SessionFormWebPartStrings';
import SessionForm from './components/SessionForm';
import { ISessionFormProps } from './components/ISessionFormProps';
import { ISessionFormWebPartProps } from './ISessionFormWebPartProps';

export default class SessionFormWebPart extends BaseClientSideWebPart<ISessionFormWebPartProps> {
  private apiLoaded: boolean = false;

  public render(): void {
    const element: React.ReactElement<ISessionFormProps> = React.createElement(
      SessionForm,
      {
        apiUrl: this.properties.apiUrl,
        httpClient: this.context.httpClient
      }
    );

    this.domElement.innerHTML += `
      <iframe src="${this.properties.apiUrl}" style="display:none;"></iframe>
      <div id="seshForm"></div>
    `;

    this.domElement.querySelector('iframe').addEventListener('load', (): void => {
      this.apiLoaded = true;
    });

    this.executeOrDelayUntilRemoteApiLoaded((): void => {
      ReactDom.render(element, this.domElement.querySelector('#seshForm'));
    });
  }

  private executeOrDelayUntilRemoteApiLoaded(func: Function): void {
    if (this.apiLoaded) {
      func();
    } else {
      setTimeout((): void => { this.executeOrDelayUntilRemoteApiLoaded(func); }, 100);
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
