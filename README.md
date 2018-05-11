# AzureStorageAccessPolicySample

A SAS token can be generated in C# (see code sample - method GetSasToken()). However this is not ideal and it cannot be revoked. Ideally you need to generate a Policy in Azure, C# code will read the policy and generate a SAS token from the Policy -  you can control the Policy in Azure.

See the method GetSasTokenFromPolicy() - you need to create an Access Policy in the blob’s ‘images’ container and grant Read access.
