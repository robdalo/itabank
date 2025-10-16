# itabank

## Introduction

Basic project illustrating ARM resource management and continuous integration in Azure.

Includes a simple WebApi for managing bank accounts written using C# .NET. This runs as an Azure app service. Additionally, an Azure function app runs on a timer for periodic account updates.

An SDK library is also provided to facilitate straight forward integration with the API.

## Azure resource management

The WebApi and Function are both intended to run in Azure and as such each include a YML pipeline and associated ARM template. The SDK is intended to be built in Azure and published as a consumable nuget package.

### itabank.WebApi

- webapi.arm.json
    - ARM template for the azure app service resource
- webapi.arm.yml
    - YML pipeline for validation and deployment of app service resource
- webapi.yml
    - YML pipeline for build, test and deployment of the WebApi

### itabank.Function

- function.arm.json
    - ARM template for the azure function app resource
- function.arm.yml
    - YML pipeline for deployment of the function app resource
- function.yml
    - YML pipeline for build, test and deployment of the Function

### itabank.SDK

- package.yml
    - YML pipeline for build, test and publish of SDK as nuget package