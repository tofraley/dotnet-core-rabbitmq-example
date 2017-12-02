#!/bin/bash

dotnet run --project ./rabbitmq-example-api
dotnet run --project ./AccountsAuditConsumer
dotnet run --project ./DirectPaymentCardConsumer
dotnet run --project ./PaymentCardConsumer
dotnet run --project ./PurchaseOrderConsumer
