resource "keycloak_realm" "customer" {
  realm             = var.customer_realm
  enabled           = true

  access_code_lifespan = "1h"

  ssl_required    = "none"
  password_policy = "upperCase(1) and length(8) and forceExpiredPasswordChange(365) and notUsername"

  internationalization {
    supported_locales = [
      "en",
      "ru"
    ]
    default_locale    = "ru"
  }

  security_defenses {
    headers {
      x_frame_options                     = "DENY"
      content_security_policy             = "frame-src 'self'; frame-ancestors 'self'; object-src 'none';"
      content_security_policy_report_only = ""
      x_content_type_options              = "nosniff"
      x_robots_tag                        = "none"
      x_xss_protection                    = "1; mode=block"
      strict_transport_security           = "max-age=31536000; includeSubDomains"
    }
    brute_force_detection {
      permanent_lockout                 = false
      max_login_failures                = 30
      wait_increment_seconds            = 60
      quick_login_check_milli_seconds   = 1000
      minimum_quick_login_wait_seconds  = 60
      max_failure_wait_seconds          = 900
      failure_reset_time_seconds        = 43200
    }
  }

  web_authn_policy {
    relying_party_entity_name = "Example"
    relying_party_id          = "keycloak.example.com"
    signature_algorithms      = ["ES256", "RS256"]
  }
}

resource "keycloak_openid_client" "customer_client" {
    realm_id  = keycloak_realm.customer.id
    client_id = var.customer_client_id

    enabled = true

    client_secret_wo = var.customer_client_secret
    client_secret_wo_version = 1

    access_type           = "CONFIDENTIAL"
    standard_flow_enabled = false
    direct_access_grants_enabled = true
    service_accounts_enabled = true
}