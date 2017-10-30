import * as React from 'react';
import * as ReactDom from 'react-dom';
import { Version } from '@microsoft/sp-core-library';
import {
  BaseClientSideWebPart,
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-webpart-base';

import * as strings from 'AbsenceRequestFormWebPartStrings';
import AbsenceRequestForm from './components/AbsenceRequestForm';
import { IAbsenceRequestFormProps } from './components/IAbsenceRequestFormProps';

export interface IAbsenceRequestFormWebPartProps {
  description: string;
}

export default class AbsenceRequestFormWebPart extends BaseClientSideWebPart<IAbsenceRequestFormWebPartProps> {

  public render(): void {
    const element: React.ReactElement<IAbsenceRequestFormProps > = React.createElement(
      AbsenceRequestForm,
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
