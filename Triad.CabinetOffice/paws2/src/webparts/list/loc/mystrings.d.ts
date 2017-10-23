declare interface IListWebPartStrings {
  PropertyPaneDescription: string;
  BasicGroupName: string;
  TitleFieldLabel: string;
}

declare module 'ListWebPartStrings' {
  const strings: IListWebPartStrings;
  export = strings;
}
