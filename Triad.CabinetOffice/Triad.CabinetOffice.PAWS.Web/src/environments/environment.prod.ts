export const environment = {
  production: true,
  adalConfig:{
    tenant:'zephyrgroup.onmicrosoft.com',
    clientId:'155e5220-4443-4aa3-8fd7-e064ebd1a8cd',
    postLogoutRedirectUri: 'https://zephyrpaws2.azurewebsites.net/logout',
    endpoints: {
      'https://zephyrpaws2api.azurewebsites.net': 'https://zephyrpaws2api.azurewebsites.net',
      'https://graph.microsoft.com': 'https://graph.microsoft.com'
    },
  },
  apiUrl: 'https://zephyrpaws2api.azurewebsites.net/odata/',
  graphUrl: 'https://graph.microsoft.com/v1.0/'
};
