#!/bin/bash

touch ./Program/.env
ln -s ../.env ./Program/Backend/.env
ln -s ../.env ./Program/Nginx/.env
ln -s ../.env ./Program/Keycloak/.env

cat << EOF > ./Program/.env
SERVER_HOSTNAME="docker.local"

# --- keycloak

# keycloak db
KC_POSTGRES_DB="keycloak"
KC_POSTGRES_USER="keycloak"
KC_POSTGRES_PASSWORD="keycloak"
KC_POSTGRES_PORT=5435

# keycloak
KEYCLOAK_HTTP_PORT=7080
KEYCLOAK_HOST="auth.\${SERVER_HOSTNAME}"
ADMIN_NAME="admin"
ADMIN_PASSWORD="admin"

# terraform
KC_EMPLOYEE_REALM="employee"
KC_EMPLOYEE_REALM_DISPLAY_NAME="employee"

KC_EMPLOYEE_BACKEND_CLIENT_ID="employee-api"
KC_EMPLOYEE_BACKEND_CLIENT_NAME="employee api"
KC_EMPLOYEE_BACKEND_CLIENT_SECRET="employee-secret"

KC_EMPLOYEE_FRONTEND_CLIENT_ID="employee-frontend"
KC_EMPLOYEE_FRONTEND_CLIENT_NAME="employee-frontend"
KC_EMPLOYEE_FRONTEND_CLIENT_SECRET="employee-frontend-secret"

KC_CUSTOMER_REALM="customer"
KC_CUSTOMER_REALM_DISPLAY_NAME="customer"

KC_CUSTOMER_BACKEND_CLIENT_ID="customer-api"
KC_CUSTOMER_BACKEND_CLIENT_NAME="customer api"
KC_CUSTOMER_BACKEND_CLIENT_SECRET="customer-secret"

KC_CUSTOMER_FRONTEND_CLIENT_ID="customer-frontend"
KC_CUSTOMER_FRONTEND_CLIENT_NAME="customer-frontend"
KC_CUSTOMER_FRONTEND_CLIENT_SECRET="customer-frontend-secret"

KC_SMPT_HOST="smtp.gmail.com"
KC_SMPT_PORT=587
KC_SMPT_USERNAME="your@email.com"
KC_SMPT_FROM=\${KC_SMPT_USERNAME}
KC_SMPT_PASSWORD="your-password"

# --- server

# server
SERVER_POSTGRES_DB="hotel"
SERVER_POSTGRES_USER="hotel"
SERVER_POSTGRES_PASSWORD="hotel"
SERVER_POSTGRES_PORT=5433

# auth with keycloak
KEYCLOAK_AUTH_SERVER_URL=https://\${KEYCLOAK_HOST}/

# employee api
EMPLOYEE_API_PORT=5001

# customer api
CUSTOMER_API_PORT=5002

# asp.net core (customer + employee)
REDIS_INSTANCE_NAME="server_"
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_SCHEMA=http
ASPNETCORE_HOST=*
ASPNETCORE_PORT=8000
ASPNETCORE_URLS=\${ASPNETCORE_SCHEMA}://\${ASPNETCORE_HOST}:\${ASPNETCORE_PORT}

MINIO_HOST=minio

# minio
S3_PORT=9000
S3_PORT_UI=9010
S3_HOST=0.0.0.0
MINIO_ROOT_PASSWORD=admin
MINIO_ROOT_USER=admin

# redis
CACHE_PORT=6378
CACHE_PASSWORD=-cache-password

# --- Nginx

# nginx
NGINX_HTTP_PORT=80
NGINX_HTTPS_PORT=443
NGINX_DOMAIN=\${SERVER_HOSTNAME}

EOF