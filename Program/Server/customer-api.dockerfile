FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /program

COPY ./src/ ./src/

RUN dotnet restore ./src/Presentations/CustomerWeb/

RUN dotnet publish ./src/Presentations/CustomerWeb/ -o ./output

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

FROM runtime

WORKDIR /app

RUN addgroup -S app-group && adduser -S app-user -G app-group

COPY --from=build /program/output ./

RUN chown -R app-user:app-group /app

USER app-user

ENTRYPOINT ["dotnet", "./CustomerWeb.dll", "--", "--environment", "Development"]