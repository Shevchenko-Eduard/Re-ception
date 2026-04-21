FROM hashicorp/terraform:1.15.0-rc2

WORKDIR /terraform

COPY ./Terraform/ ./ 

RUN terraform init