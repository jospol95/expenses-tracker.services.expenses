FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

#copy project and restore layers
#WORKDIR /src
COPY BudgetMS.sln .
COPY Expenses.API/*.csproj ./Expenses.API/
COPY Expenses.Domain/*.csproj ./Expenses.Domain/
COPY Expenses.Infrastructure/*.csproj ./Expenses.Infrastructure/

RUN dotnet restore

#copy everything else and build app
COPY Expenses.API/. ./Expenses.API/
COPY Expenses.Domain/. ./Expenses.Domain/
COPY Expenses.Infrastructure/. ./Expenses.Infrastructure/

WORKDIR /app/Expenses.API/
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS RUNTIME
WORKDIR /app

COPY --from=build /app/Expenses.API/out ./
ENTRYPOINT ["dotnet", "Expenses.API.dll"]
