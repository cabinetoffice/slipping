// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  adalConfig:{
    tenant:'zephyrgroup.onmicrosoft.com',
    clientId:'155e5220-4443-4aa3-8fd7-e064ebd1a8cd',
    postLogoutRedirectUri: 'http://localhost:4200/logout',
    endpoints: {
      'https://zephyrpaws2api.azurewebsites.net': 'https://zephyrpaws2api.azurewebsites.net',
      'https://graph.microsoft.com': 'https://graph.microsoft.com'
    },
  },
  apiUrl: 'https://zephyrpaws2api.azurewebsites.net/odata/',
  graphUrl: 'https://graph.microsoft.com/v1.0/'
};
