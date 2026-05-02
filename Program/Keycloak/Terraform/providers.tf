terraform {
  required_providers {
    keycloak = {
      source  = "keycloak/keycloak"
      version = "5.7.0" # Рекомендуется использовать свежую версию
    }
  }
}

provider "keycloak" {
  client_id = "admin-cli"          # Указываем публичный клиент
  url       = var.kc_url
  username  = var.admin_username   # Имя пользователя с правами администратора
  password  = var.admin_password   # Пароль этого пользователя
  realm     = "master"             # Обычно администратор находится в realm "master"
}