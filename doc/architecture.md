## Logging

* For frequent use of logging service is the best to simply inject the service by constructor injection and use it internaly.
* For couple of debuging messages, the methods on static Provider class can be used. They are marked with Conditional attribute for save use only on debug builds.
* For totaly independent log record, the message hub can be used when logging service is registered with it.

## Providers

Providers are designated to be used as Facades for services, view models, eventually stores. 
They are forced to use only one type of items to force developers to strict to rule one type per provider.
A provider should serve important items by strong typed properties if will be used as a facade.
If a facade is not needed only default providers for services and view models can be used for message bus registration.

 ## View model locator

 TO be able to use view model locator is necessary to use view model provider as well.