FROM mcr.microsoft.com/dotnet/aspnet:10.0.7-alpine3.23 AS runtime

RUN apk add --no-cache \
    curl \
    libc6-compat \
    icu-libs \
    krb5-libs \
    libgcc \
    libintl \
    libssl3 \
    libstdc++ \
    zlib \
    libldap \
    icu-data-full

WORKDIR /app

RUN addgroup -S app-group && adduser -S app-user -G app-group

RUN chown -R app-user:app-group /app

USER app-user

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS base-build

WORKDIR /program

COPY --parents ./src/**/*.csproj .
COPY --parents ./src/**/*.props .
COPY --parents ./src/**/*.slnx .

RUN dotnet restore ./src/

COPY ./src/ ./src/

FROM base-build AS customer-build

RUN dotnet publish ./src/Presentations/CustomerWeb/ -o ./output

FROM base-build AS employee-build

RUN dotnet publish ./src/Presentations/EmployeeWeb/ -o ./output

FROM runtime AS customer-api

COPY --from=customer-build /program/output ./

ENTRYPOINT ["dotnet", "./CustomerWeb.dll"]

FROM runtime AS employee-api

COPY --from=employee-build /program/output ./

ENTRYPOINT ["dotnet", "./EmployeeWeb.dll"]