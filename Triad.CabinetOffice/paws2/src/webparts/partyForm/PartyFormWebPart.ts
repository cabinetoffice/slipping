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

export interface IPartyFormWebPartProps {
  description: string;
}

export default class PartyFormWebPart extends BaseClientSideWebPart<IPartyFormWebPartProps> {

  public render(): void {
    const element: React.ReactElement<IPartyFormProps > = React.createElement(
      PartyForm,
      {
        description: this.properties.description
      }
    );

    ReactDom.render(element, this.domElement);
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
                PropertyPaneTextField('description', {
                  label: strings.DescriptionFieldLabel
                })
              ]
            }
          ]
        }
      ]
    };
  }
}
