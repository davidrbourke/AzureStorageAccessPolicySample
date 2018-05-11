# AzureStorageAccessPolicySample

A SAS token can be generated in C# (see code sample - method GetSasToken()). However this is not ideal and it cannot be revoked. Ideally you need to generate a Policy in Azure, C# code will read the policy and generate a SAS token from the Policy -  you can control the Policy in Azure.

See the method GetSasTokenFromPolicy() - you need to create an Access Policy in the blob’s ‘images’ container and grant Read access.

## Setup
1. Set the StorageConnection setting in the web.config to a Azure Storage Account Connection String
2. Create a blob container in Azure called: images
3. Create and Access Policy in the container called MySAP
4. Upload images in Azure