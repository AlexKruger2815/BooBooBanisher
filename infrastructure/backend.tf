terraform {
  backend "s3" {
    bucket = "booboobanisher-infrastructure-terraform-state-bucket"
    key = "booboobanisher-infrastructure-terraform-state-bucket/terraform.tfstate"  # Specify the path/key for your state file
    region = "eu-west-1"
  }
}