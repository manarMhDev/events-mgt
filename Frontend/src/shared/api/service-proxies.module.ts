import { NgModule } from '@angular/core';
// import { AbpHttpInterceptor } from 'abp-ng2-module';

import * as ApiServiceProxies from './service-proxies';

@NgModule({
    providers: [
        ApiServiceProxies.AuthClient,
        ApiServiceProxies.FirstTitleClient,
        ApiServiceProxies.SecondTitleClient,
        ApiServiceProxies.SeatsTypesClient,
        ApiServiceProxies.PersonTypeClient,
        ApiServiceProxies.EventPlaceClient,
        ApiServiceProxies.EventsClient,
        ApiServiceProxies.InvitationClient,
        ApiServiceProxies.SeatsClient,
    ]
})
export class ServiceProxyModule { }
