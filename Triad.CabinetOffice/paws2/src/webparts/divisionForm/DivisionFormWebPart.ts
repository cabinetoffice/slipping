import * as React from 'react';
import * as ReactDom from 'react-dom';
import { Version } from '@microsoft/sp-core-library';
import {
  BaseClientSideWebPart,
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-webpart-base';

import * as strings from 'DivisionFormWebPartStrings';
import DivisionForm from './components/DivisionForm';
import { IDivisionFormProps } from './components/IDivisionFormProps';

export interface IDivisionFormWebPartProps {
  description: string;
}

export default class DivisionFormWebPart extends BaseClientSideWebPart<IDivisionFormWebPartProps> {

  public render(): void {
    const element: React.ReactElement<IDivisionFormProps > = React.createElement(
      DivisionForm,
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
