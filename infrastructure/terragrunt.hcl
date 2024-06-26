locals {
  aws_region   = "eu-west-1"
  state_bucket_region = "eu-west-1"
  aws_account_id = "978251882572"
}

# Configure Terragrunt to automatically store tfstate files in an S3 bucket
remote_state {
  backend = "s3"
  config = {
    encrypt        = true
    bucket         = "booboobanisher-infrastructure-terraform-state-bucket"
    key            = "${path_relative_to_include()}/terraform.tfstate"
    region         = "${local.state_bucket_region}"
    dynamodb_table = "booboobanisher-infrastructure-terraform-locks-table"

    s3_bucket_tags = {
      "created-using" = "terraform"
      "owner" = "alexander.kruger@bbd.co.za"
    }

    dynamodb_table_tags = {
      "created-using" = "terraform"
      "owner" = "alexander.kruger@bbd.co.za"
    }

  }
  generate = {
    path      = "backend.tf"
    if_exists = "overwrite_terragrunt"
  }
}

# Generate an AWS provider block
# Use a profile rather than assume role
generate "provider" {
  path      = "provider_override.tf"
  if_exists = "overwrite_terragrunt"
  contents  = <<EOF
terraform {
  required_version = "~> 1.5"

  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
}

provider "aws" {
  region = "${local.aws_region}"
  default_tags {
        tags = {
        "created-using" = "terraform"
        "owner" = "alexander.kruger@bbd.co.za"
        "level up" = "CSharp"
        "project" = "BooBooBanisher"
    }
  }
}
EOF
}