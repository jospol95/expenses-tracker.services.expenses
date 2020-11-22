FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS BUILD
WORKDIR /app

#copy project and restore layers
COPY ../BudgetMS.sln .
COPY Expenses.API.csproj ./Expenses/
COPY Expenses.Domain/*.csproj ./Expenses.Domain/
COPY Expenses.Infrastructure/*.csproj ./Expenses.Infraestructure/

RUN dotnet restore

#copy everything else and build app
COPY Expenses.API.csproj ./Expenses/
COPY Expenses.Domain/ ./Expenses.Domain/
COPY Expenses.Infrastructure/ ./Expenses.Infraestructure/

WORKDIR /app/Expenses/
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS RUNTIME
WORKDIR /app

COPY --from=build /app/Expenses/out ./
ENTRYPOINT ["dotnet", "Expenses.dll"]
