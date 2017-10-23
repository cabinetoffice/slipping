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

export interface IMemberOfParliamentFormWebPartProps {
  description: string;
}

export default class MemberOfParliamentFormWebPart extends BaseClientSideWebPart<IMemberOfParliamentFormWebPartProps> {

  public render(): void {
    const element: React.ReactElement<IMemberOfParliamentFormProps > = React.createElement(
      MemberOfParliamentForm,
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
