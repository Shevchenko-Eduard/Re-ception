resource "keycloak_realm" "customer" {
  realm        = var.customer_realm
  display_name = var.customer_realm_display_name

  enabled = true

  login_with_email_allowed       = true
  duplicate_emails_allowed       = false
  edit_username_allowed          = false
  registration_allowed           = true
  registration_email_as_username = true

  access_token_lifespan        = "5m"
  sso_session_idle_timeout     = "30m"
  sso_session_max_lifespan     = "8h"
  offline_session_idle_timeout = "720h"

  ssl_required = "external"

  login_theme   = "keycloak.v2"
  account_theme = "keycloak.v3"
  admin_theme   = "keycloak.v2"
  email_theme   = "keycloak"

  password_policy = "length(8) and digits(1) and upperCase(1) and lowerCase(1)"

  smtp_server {
    host     = var.smtp_host
    from     = var.smtp_from
    port     = var.smtp_port
    starttls = true
    ssl      = false
    auth {
      username = var.smtp_username
      password = var.smtp_password
    }
  }

  internationalization {
    supported_locales = ["en", "ru"]
    default_locale    = "ru"
  }

  security_defenses {
    headers {
      x_frame_options           = "DENY"
      x_content_type_options    = "nosniff"
      x_robots_tag              = "none"
      x_xss_protection          = "1; mode=block"
      strict_transport_security = "max-age=31536000; includeSubDomains"
    }
    brute_force_detection {
      permanent_lockout                = false
      max_login_failures               = 5
      wait_increment_seconds           = 60
      minimum_quick_login_wait_seconds = 60
    }
  }
}

resource "keycloak_openid_client" "customer_backend_client" {
  realm_id  = keycloak_realm.customer.id
  client_id = var.customer_backend_client_id
  name      = var.customer_backend_client_name

  enabled = true

  client_secret_wo         = var.customer_backend_client_secret
  client_secret_wo_version = 1

  access_type                  = "CONFIDENTIAL"
  standard_flow_enabled        = true
  implicit_flow_enabled        = false
  direct_access_grants_enabled = true
  service_accounts_enabled     = true

  valid_redirect_uris = [
    "https://*",
    "http://*"
  ]

  web_origins = ["*"]
}

resource "keycloak_openid_client" "customer_frontend_client" {
  realm_id  = keycloak_realm.customer.id
  client_id = var.customer_frontend_client_id
  name      = var.customer_frontend_client_name

  access_type           = "PUBLIC"
  standard_flow_enabled = true
  implicit_flow_enabled = false

  valid_redirect_uris = [
    "https://*",
    "http://*"
  ]

  web_origins = ["*"]

  pkce_code_challenge_method = "S256"

  access_token_lifespan = "300"

  frontchannel_logout_enabled = true
}
