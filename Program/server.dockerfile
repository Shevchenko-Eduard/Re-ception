FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY Server/ ./
COPY ./build-server.sh ./

RUN chmod +x build-server.sh
RUN ./build-server.sh

FROM mcr.microsoft.com/dotnet/runtime-deps:10.0 AS runtime-10

RUN apt update && \
    apt install -y curl

FROM runtime-10

WORKDIR /app

COPY --from=build /src/output ./

RUN chown -R app:app /app

USER app

EXPOSE 8000

ENTRYPOINT ["./WebApi", "--environment", "Development"]